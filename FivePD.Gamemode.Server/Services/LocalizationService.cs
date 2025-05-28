// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System;
using System.Collections.Generic;
using System.IO;
using CitizenFX.Core;
using FivePD.Common;
using FivePD.Common.Exceptions;
using FivePD.Common.Extensions;
using FivePD.Common.Localization;
using FivePD.Gamemode.Server.Interfaces;
using Newtonsoft.Json;
using Serilog;
using Cfx = CitizenFX.Core.Native;

namespace FivePD.Gamemode.Server.Services
{
    /// <inheritdoc />
    public class LocalizationService : ILocalizationService
    {
        private readonly Localization _defaultLocalization;
        private readonly EventHandlerDictionary _eventHandlerDictionary;
        private readonly IFileHandlerService _fileHandlerService;
        private readonly ILogger _logger;
        private List<Locale> _locales;

        /// <summary>Initializes a new instance of the <see cref="LocalizationService"/> class.</summary>
        /// <param name="eventHandlerDictionary">The <see cref="EventHandlerDictionary" /> to use.</param>
        /// <param name="fileHandlerService">The <see cref="IFileHandlerService" /> to use.</param>
        /// <param name="logger">The <see cref="ILogger" /> to use.</param>
        public LocalizationService(EventHandlerDictionary eventHandlerDictionary, IFileHandlerService fileHandlerService, ILogger logger)
        {
            this._defaultLocalization = new Localization();
            this._eventHandlerDictionary = eventHandlerDictionary;
            this._fileHandlerService = fileHandlerService;
            this._logger = logger;
        }

        /// <inheritdoc />
        public Localization GetDefaultLocalization()
        {
            return this._defaultLocalization;
        }

        /// <inheritdoc />
        public void RegisterEventHandlers()
        {
            this._eventHandlerDictionary[Events.Localization.GetAvailableLocalesFromServer] += new Action<NetworkCallbackDelegate>(cb =>
            {
                cb.Invoke(JsonConvert.SerializeObject(this._locales));
            });
        }

        /// <inheritdoc />
        public void LoadAvailableLocales()
        {
            try
            {
                this._locales = new List<Locale>();

                var path = AppDomain.CurrentDomain.BaseDirectory + "/Localization/";
                var files = Directory.GetFiles(path, "*.json");
                foreach (var file in files)
                {
                    var fileName = Path.GetFileName(file).Split('.');
                    this._locales.Add(new Locale(fileName[0], string.Empty));
                }

                if (this._locales.Count == 0)
                {
                    this._locales.Add(new Locale("en", "English"));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error loading localization files: " + ex.Message);
            }
        }

        /// <inheritdoc />
        public void ValidateLocalizationFiles()
        {
            foreach (var locale in this._locales)
            {
                var path = $"/Localization/{locale.Key}.json";
                var file = this._fileHandlerService.LoadFile<Localization>(path, false);

                if (file.Item2 is null)
                {
                    if (locale.Key != "en" && file.Item1.Title == "English")
                    {
                        locale.Title = "? " + locale.Key + " ?";
                    }
                    else
                    {
                        locale.Title = file.Item1.Title;
                    }

                    continue;
                }

                var exceptionMessage = file.Item2 switch
                {
                    EmptyConfigException _ => this._defaultLocalization.WarningLocalizations.EmptyConfigException,
                    _ => this._defaultLocalization.WarningLocalizations.ConfigException,
                };

                this._logger.Warning(exceptionMessage.ReplaceParams(
                    new Dictionary<string, string>
                    {
                        { "path", $"\"{path}\"" },
                    }));
            }
        }
    }
}