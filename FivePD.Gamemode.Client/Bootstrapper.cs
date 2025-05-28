// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using FivePD.Gamemode.Client.Extensions;
using FivePD.Gamemode.Client.Injection;
using FivePD.Gamemode.Client.Interfaces;
using FivePD.Gamemode.Client.Services;
using Cfx = CitizenFX.Core.Native;

#pragma warning disable S3241
#pragma warning disable CS4014
namespace FivePD.Gamemode.Client
{
    /**
     * <summary>The gamemode client entrypoint. Handles the creation and management of all tasks on the client.</summary>
     */
    // ReSharper disable once UnusedType.Global
    public class Bootstrapper : BaseScript
    {
        /**
         * <summary>
         *     Initializes a new instance of the <see cref="Bootstrapper" /> class. Used as the entrypoint for the Cfx
         *     runtime.
         * </summary>
         */
        public Bootstrapper()
        {
            var services = new ServiceProviderBuilder();

            services
                .Register(() => this.EventHandlers)
                .As<EventHandlerDictionary>();

            services
                .Register<AddTickHandler>(() => this.OnTickAdd);
            services
                .Register<RemoveTickHandler>(() => this.OnTickRemove);

            services
                .RegisterType<FileHandlerService>()
                .As<IFileHandlerService>();

            services
                .RegisterType<LoggerService>()
                .As<ILoggerService>();

            services
                .RegisterType<LocalizationService>()
                .As<ILocalizationService>()
                .IsSingleton();

            services
                .RegisterType<ConfigService>()
                .As<IConfigService>();

            services
                .RegisterType<EntityService>()
                .As<IEntityService>();

            services
                .RegisterType<TickService>()
                .As<ITickService>();

            services
                .RegisterType<ScreenDecorService>()
                .As<IScreenDecorService>();

            services
                .RegisterType<NuiService>()
                .As<INuiService>()
                .IsSingleton();

            services
                .RegisterType<SoundService>()
                .As<ISoundService>();

            services
                .RegisterType<KeybindService>()
                .As<IKeybindService>()
                .IsSingleton();

            services
                .RegisterType<RadialMenuService>()
                .As<IRadialMenuService>();

            services
                .RegisterType<MenuService>()
                .As<IMenuService>();

            services
                .RegisterType<PedService>()
                .As<IPedService>();

            services
                .RegisterType<VehicleService>()
                .As<IVehicleService>();

            services
                .RegisterType<NotificationService>()
                .As<INotificationService>();

            services
                .RegisterType<PoliceEventService>()
                .As<IPoliceEventService>()
                .IsSingleton();

            services
                .RegisterType<TrafficStopService>()
                .As<ITrafficStopService>();

            var provider = services.Build();

            var screenDecorService = provider.Resolve<IScreenDecorService>();
            screenDecorService.SetTextToDraw(true, false);
            screenDecorService.BeginTextDraw();

            var keybindService = provider.Resolve<IKeybindService>();
            keybindService.Register(provider);

            var nuiService = provider.Resolve<INuiService>();
            nuiService.RegisterEventHandlers();

            var pedService = provider.Resolve<IPedService>();
            pedService.RegisterEvents();
            pedService.RegisterKeybinds();

            var trafficStopService = provider.Resolve<ITrafficStopService>();
            trafficStopService.RegisterKeybinds();

            var localizationService = provider.Resolve<ILocalizationService>();
            localizationService.RegisterEventHandlers();
            localizationService.LoadDefaultLocalization();

            var radialMenuService = provider.Resolve<IRadialMenuService>();
            radialMenuService.RegisterEvents();
            radialMenuService.CreateMenus();
            radialMenuService.RegisterMenuKeybinds();

            var menuService = provider.Resolve<IMenuService>();
            menuService.RegisterEvents();
            menuService.CreateMenus();
            menuService.RegisterMenuKeybinds();

            screenDecorService.SetTextToDraw(false, false);
        }

        private void OnTickAdd(Func<Task> task)
        {
            this.Tick += task;
        }

        private void OnTickRemove(Func<Task> task)
        {
            this.Tick -= task;
        }
    }
}