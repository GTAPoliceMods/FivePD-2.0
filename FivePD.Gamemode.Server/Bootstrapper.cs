// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using CitizenFX.Core;
using FivePD.Common.Models;
using FivePD.Gamemode.Server.API;
using FivePD.Gamemode.Server.Database;
using FivePD.Gamemode.Server.Exceptions;
using FivePD.Gamemode.Server.Extensions;
using FivePD.Gamemode.Server.Interfaces;
using FivePD.Gamemode.Server.Models;
using FivePD.Gamemode.Server.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sentry;
using Serilog;
using Cfx = CitizenFX.Core.Native;

namespace FivePD.Gamemode.Server
{
    /// <summary>
    /// The gamemode server entrypoint. Handles the
    /// creation and management of all tasks on the server.
    /// </summary>
    // ReSharper disable once UnusedType.Global
    public class Bootstrapper : BaseScript
    {
        private const string SentryDsn = "https://a304ee75321740359328f5f3b3fbb98c@o983555.ingest.sentry.io/6455831";

        private readonly ServiceProvider _provider;
        private readonly IDisposable _sentryClient;

        private bool _isStopped;

        /// <summary>
        /// Initializes a new instance of the <see cref="Bootstrapper" /> class.
        /// Used as the entrypoint for the Cfx server runtime.
        /// </summary>
        public Bootstrapper()
        {
            #if DEBUG
            ConfigureForDevelopment();
            #endif

            var globalLoggerConfig = new LoggerConfiguration()
                .WriteTo.CfxDebugSink("[{Timestamp:HH:mm:ss} FivePD:{SourceIdentifier} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.File(
                    $"{AppDomain.CurrentDomain.BaseDirectory}/Logs/Server-.txt",
                    rollingInterval: RollingInterval.Day,
                    rollOnFileSizeLimit: true,
                    retainedFileCountLimit: 7)
                .MinimumLevel.Verbose();

            // todo: validate certs for sentry and our in-house api
            #pragma warning disable S4830
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            #pragma warning restore S4830

            // Register Sentry for error logging
            this._sentryClient = SentrySdk.Init(o =>
            {
                o.Dsn = SentryDsn;
                o.TracesSampleRate = 1.0;
                #if DEBUG
                o.Debug = true;
                o.DiagnosticLogger = new SerilogSentryDiagnosticLogger();
                o.Environment = "development";
                #endif
            });

            SentrySdk.ConfigureScope(scope =>
            {
                scope.Contexts["cfx"] = new
                {
                    Version = Cfx.API.GetConvar("version", "Unknown"),
                };
            });

            // Begin startup Sentry transaction
            var transaction = SentrySdk.StartTransaction(
                "bootstrap",
                "bootstrap-gamemode",
                "Gamemode Bootstrapping");

            var addonLoadSpan = transaction.StartChild("bootstrap-addons", "Bootstrapping for addon subsystem");

            /*
             * Gamemode addons have full(ish) access to the complete suite of services
             * made available to core gamemode services. Thus, we need to find our addons
             * prior to actually assembling a service this._provider. The addon locator takes one
             * parameter, a logger, so we create it early.
             */

            Log.Logger = globalLoggerConfig.CreateLogger();

            var preInitLogger = Log.Logger.ForContext("SourceIdentifier", "Bootstrapper");

            preInitLogger.Information(
                "Bootstrapping gamemode server version {@Version}",
                Assembly.GetExecutingAssembly().GetName().Version.ToString());

            var addonLocator = new FilesystemAddonLocator(Log.Logger.ForContext("SourceIdentifier", "AddonLocator"));

            var assembliesWithDefinitions = new Dictionary<Assembly, AddonDefinition>();

            var allAddons = addonLocator.GetAddons().ToList();

            // Load addon assemblies
            foreach (var addon in allAddons)
            {
                preInitLogger.Information(
                    "Registering addon {@AddonName} version {@AddonVersion}",
                    addon.FriendlyName,
                    addon.FriendlyVersion);

                foreach (var assembly in addon.Assemblies)
                {
                    var asm = Assembly.Load(assembly);
                    assembliesWithDefinitions.Add(asm, addon);
                }
            }

            addonLoadSpan.Finish();
            var serviceInitSpan = transaction.StartChild("bootstrap-services", "Bootstrapping for services");

            var services = new ServiceCollection();

            // Find and register core services
            services.AddSingleton(Log.Logger);
            services.AddSingleton(this.EventHandlers);

            services.AddDbContext<Database.DatabaseContext>();

            services.AddSingleton<IFileHandlerService, FileHandlerService>();
            services.AddSingleton<ILocalizationService, LocalizationService>();
            services.AddSingleton<IConfigService, ConfigService>();
            services.AddSingleton<ICommandService, CommandService>();
            services.AddSingleton<IAddonMetadataService, AddonMetadataService>();
            services.AddTransient<IAddonLoggerService, AddonLoggerService>();
            services.AddSingleton<ITickQueueService, ConcurrentTickQueueService>();
            services.AddSingleton<IAddonAssemblyLoader, AddonAssemblyLoader>();
            services.AddSingleton<IEntityService, EntityService>();
            services.AddSingleton<IServerStateService, ServerStateService>();
            services.AddSingleton<IReplicatedStateService, ReplicatedStateService>();
            services.AddSingleton<IDevUtilitiesService, DevUtilitiesService>();
            services.AddSingleton<IPedService, PedService>();
            services.AddSingleton<IVehicleService, VehicleService>();
            services.AddSingleton<ITrafficStopService, TrafficStopService>();
            services.AddSingleton<IPlayerDataService, PlayerDataService>();

            // Find and register addons
            services.Scan(scan => scan
                .FromAssemblies(assembliesWithDefinitions.Keys)
                .AddClasses(classes => classes.AssignableTo<IGamemodeAddon>())
                .AsSelfWithInterfaces()
                .WithSingletonLifetime());

            this._provider = services.BuildServiceProvider();

            var addonMetadataService = this._provider.GetService<IAddonMetadataService>();
            foreach (var assembly in assembliesWithDefinitions)
            {
                addonMetadataService?.RegisterAddonAssemblyWithDefinition(assembly.Key, assembly.Value);
            }

            var playerDataService = this._provider.GetService<IPlayerDataService>();
            playerDataService?.RegisterEvents();

            var entityService = this._provider.GetService<IEntityService>();
            entityService?.RegisterEvents();

            var pedService = this._provider.GetService<IPedService>();
            pedService?.RegisterEvents();

            var vehicleService = this._provider.GetService<IVehicleService>();
            vehicleService?.RegisterEvents();

            var trafficStopService = this._provider.GetService<ITrafficStopService>();
            trafficStopService?.RegisterEvents();

            var localizationService = this._provider.GetService<ILocalizationService>();
            localizationService?.RegisterEventHandlers();
            localizationService?.LoadAvailableLocales();
            localizationService?.ValidateLocalizationFiles();

            var log = this._provider.GetService<ILogger>()?.ForContext("SourceIdentifier", "Bootstrapper");

            this.LogDiagnostics();

            if (!this.IsSafeToRun())
            {
                log?.Fatal("FivePD cannot run in this environment. Verify all prerequisites have been met and try again");
                this.OnStopped();
                return;
            }

            var db = this._provider.GetService<DatabaseContext>();
            try
            {
                if (db is null)
                {
                    throw new DatabaseUsageException("No database context is available for usage");
                }

                if (!db.Database.CanConnect())
                {
                    throw new DatabaseUsageException("The configured database is invalid or offline. Verify the connection settings in the /Config/database.json file and try again");
                }

                var migrations = db.Database.GetPendingMigrations();

                try
                {
                    log?.Information("Upgrading the database to {Version}", migrations.Last());
                    db.Database.Migrate();
                }
                catch (Exception)
                {
                    // Throws when there are no migrations to run. Not an issue. Continue.
                    log?.Information("The database is up to date");
                }
            }
            catch (DatabaseUsageException e)
            {
                log?.Fatal("Database configuration failed: {Error}", e.Message);

                this.StopGamemode();
                return;
            }

            // Check for dev mode. If enabled, register the utilities
            if (Cfx.API.GetConvar("sv_enableFivepdDevUtils", "false") == "true")
            {
                var devUtilsService = this._provider.GetService<IDevUtilitiesService>();

                devUtilsService?.RegisterDevelopmentCommands();
            }

            var tickQueueService = this._provider.GetService<ITickQueueService>();
            this.Tick += tickQueueService!.OnTick;

            // Grab an instance of every addon entrypoint
            foreach (var addon in this._provider.GetServices<IGamemodeAddon>())
            {
                // Lazy load it
                tickQueueService.Enqueue(() => addon.OnStarted());
            }

            serviceInitSpan.Finish();
            transaction.Finish();

            // Being that the thread is about to be free for the first time, enqueued addon starts will run here
            log?.Information("Gamemode server bootstrapping has completed. Preparing addon initialization");

            var demoPlayer = new FPlayer("1");

            playerDataService.DutyStatusChanged += (sender, args) => log.Information("A duty status changed! ({@Player})", args);

            log?.Information("Before duty status is {@Status}", playerDataService.GetDutyStatus(demoPlayer));
            playerDataService.SetDutyStatus(demoPlayer, FDutyStatus.OnDutyAndReceiving);
            log?.Information("After duty status is {@Status}", playerDataService.GetDutyStatus(demoPlayer));
        }

        /// <summary>
        /// Called when the gamemode is being stopped. Handles cleanup.
        /// </summary>
        [EventHandler("onResourceStop")]
        internal void OnStopped()
        {
            if (this._isStopped)
            {
                return;
            }

            var log = this._provider.GetService<ILogger>()?.ForContext("SourceIdentifier", "Bootstrapper");

            log?.Information("Stopping FivePD");

            this._isStopped = true;

            log?.Debug("Running addon shutdown routine");

            // Grab an instance of every addon entrypoint and "stop" it
            foreach (var addon in this._provider.GetServices<IGamemodeAddon>())
            {
                addon.OnStopped();
            }

            log?.Debug("Addon shutdown complete. Running gamemode disposal routine");

            try
            {
                this._provider.Dispose();
            }
            catch (Exception)
            {
                // Doesn't matter at this point
            }
        }

        /// <summary>
        /// Terminates the gamemode.
        /// </summary>
        internal void StopGamemode()
        {
            this.OnStopped();
        }

        #if DEBUG
        /// <summary>
        /// Configures pre-run settings for development mode.
        /// This logic runs before the gamemode has been initialized.
        /// </summary>
        private static void ConfigureForDevelopment()
        {
            // Disables client-side script entity spawning
            Cfx.API.SetRoutingBucketEntityLockdownMode(0, "relaxed");
        }
        #endif

        /// <summary>
        /// Determines if the gamemode can run in the current environment.
        /// <returns>A boolean representing the safety of the environment.</returns>
        /// </summary>
        private bool IsSafeToRun()
        {
            // Verifies OneSync is enabled
            if (Cfx.API.GetConvar("onesync", "off") != "on")
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Logs common diagnostic properties.
        /// </summary>
        private void LogDiagnostics()
        {
            var log = this._provider.GetService<ILogger>()!.ForContext("SourceIdentifier", "Bootstrapper");

            // Logs the current Cfx artifacts version
            log.Debug(
                "Running Cfx version {@Version}",
                Cfx.API.GetConvar("version", "Unknown"));

            // Logs dev mode status
            var devModeEnabled = Cfx.API.GetConvar("sv_enableFivepdDevUtils", "false") == "true";

            log.Debug(
                "Development mode is {@Status}",
                devModeEnabled ? "on" : "off");

            if (devModeEnabled)
            {
                log.Warning(
                    "Do not enable development mode on public servers. Only enable development mode when developing addons for FivePD. Development mode allows bad actors to exploit FivePD. This is VERY dangerous");
            }
        }
    }
}