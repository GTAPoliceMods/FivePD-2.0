// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using CitizenFX.Core;
using FivePD.Common;
using FivePD.Common.Exceptions;
using FivePD.Common.Extensions;
using FivePD.Common.Localization;
using FivePD.Gamemode.Client.Extensions;
using FivePD.Gamemode.Client.Interfaces;
using Newtonsoft.Json.Linq;
using Cfx = CitizenFX.Core.Native;

#pragma warning disable S1450

namespace FivePD.Gamemode.Client.Services
{
    /// <inheritdoc />
    public class LocalizationService : ILocalizationService
    {
        private readonly EventHandlerDictionary _eventHandlerDictionary;
        private readonly IFileHandlerService _fileHandlerService;
        private readonly ILoggerService _loggerService;
        private readonly string _defaultLocale = "en";

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizationService"/> class.
        /// </summary>
        /// <param name="eventHandlerDictionary">The <see cref="EventHandlerDictionary" /> to use.</param>
        /// <param name="fileHandlerService">The <see cref="IFileHandlerService" /> to use.</param>
        /// <param name="loggerService">The <see cref="ILoggerService" /> to use.</param>
        public LocalizationService(EventHandlerDictionary eventHandlerDictionary, IFileHandlerService fileHandlerService, ILoggerService loggerService)
        {
            this.Locales = new List<Locale>();
            this._eventHandlerDictionary = eventHandlerDictionary;
            this._fileHandlerService = fileHandlerService;
            this._loggerService = loggerService;
        }

        /// <inheritdoc />
        public List<Locale> Locales { get; }

        /// <inheritdoc />
        public Localization CurrentLocalization { get; private set; }

        /// <inheritdoc />
        public void RegisterEventHandlers()
        {
            this._eventHandlerDictionary.AddNuiEvent(Events.Localization.GetLocalizationFromNui, new Action<ExpandoObject, CallbackDelegate>((body, cb) =>
            {
                cb.Invoke(this.CurrentLocalization);
            }));
        }

        /// <inheritdoc />
        public async Task<List<Locale>> LoadAvailableLocales()
        {
            if (this.Locales.Count > 0)
            {
                return this.Locales;
            }

            int hasServerResponded = 0;
            var now = DateTime.Now;
            var timeUntilFinishWait = now.AddMinutes(1);

            BaseScript.TriggerServerEvent(Events.Localization.GetAvailableLocalesFromServer, new Action<string>(locales =>
            {
                hasServerResponded = 1;
                foreach (var item in JArray.Parse(locales))
                {
                    this.Locales.Add(new Locale(item["Key"].ToString(), item["Title"].ToString()));
                }
            }));

            do
            {
                if (DateTime.Compare(DateTime.Now, timeUntilFinishWait) > 0)
                {
                    return new List<Locale>();
                }

                await BaseScript.Delay(1);
            }
            while (hasServerResponded == 0);

            return this.Locales;
        }

        /// <inheritdoc />
        public void LoadDefaultLocalization()
        {
            this.LoadLocalization(this._defaultLocale);
        }

        /// <inheritdoc />
        public void LoadLocalization(string locale)
        {
            var path = $"/Localization/{locale}.json";
            var file = this._fileHandlerService.LoadFile<Localization>(path, false);

            if (file.Item2 is null)
            {
                this.CurrentLocalization = file.Item1;
                return;
            }

            var exceptionMessage = file.Item2 switch
            {
                EmptyConfigException _ => this.CurrentLocalization.WarningLocalizations.EmptyConfigException,
                _ => this.CurrentLocalization.WarningLocalizations.ConfigException,
            };

            this._loggerService.Warn(exceptionMessage.ReplaceParams(
                new Dictionary<string, string>
                {
                    { "path", $"\"{path}\"" },
                }));
        }
    }
}