// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System.Collections.Generic;
using FivePD.Common.ConfigModels;
using FivePD.Common.Exceptions;
using FivePD.Common.Extensions;
using FivePD.Gamemode.Server.Interfaces;
using Serilog;
using Cfx = CitizenFX.Core.Native;

namespace FivePD.Gamemode.Server.Services
{
    /// <inheritdoc />
    public class ConfigService : IConfigService
    {
        private readonly IFileHandlerService _fileHandlerService;
        private readonly ILocalizationService _localizationService;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigService"/> class.
        /// </summary>
        /// <param name="fileHandlerService">The <see cref="IFileHandlerService"/> to use.</param>
        /// <param name="localizationService">The <see cref="ILocalizationService"/> to use.</param>
        /// <param name="logger">The <see cref="ILogger"/> to use.</param>
        public ConfigService(IFileHandlerService fileHandlerService, ILocalizationService localizationService, ILogger logger)
        {
            this._fileHandlerService = fileHandlerService;
            this._localizationService = localizationService;
            this._logger = logger;
        }

        /// <inheritdoc />
        public DatabaseConfig GetDatabaseConfig()
        {
            return this.InternalLoadFile<DatabaseConfig>("/Config/database.json");
        }

        /// <inheritdoc />
        public List<Item> GetItems()
        {
            return this.InternalLoadFile<List<Item>>("/Config/items.json");
        }

        /// <inheritdoc />
        public List<string> GetFemaleFirstnames()
        {
            return this._fileHandlerService.LoadStringList("/Config/FemaleFirstnames.txt");
        }

        /// <inheritdoc />
        public List<string> GetMaleFirstnames()
        {
            return this._fileHandlerService.LoadStringList("/Config/MaleFirstnames.txt");
        }

        /// <inheritdoc />
        public List<string> GetLastnames()
        {
            return this._fileHandlerService.LoadStringList("/Config/Lastnames.txt");
        }

        private T InternalLoadFile<T>(string path, bool useCache = true)
        {
            var file = this._fileHandlerService.LoadFile<T>(path, useCache);

            if (file.Item2 == null)
            {
                return file.Item1;
            }

            var exceptionMessage = file.Item2 switch
            {
                EmptyConfigException _ => this._localizationService.GetDefaultLocalization().WarningLocalizations.EmptyConfigException,
                _ => this._localizationService.GetDefaultLocalization().WarningLocalizations.ConfigException,
            };

            this._logger.Warning(exceptionMessage.ReplaceParams(
                new Dictionary<string, string>
                {
                    { "path", $"\"{path}\"" },
                }));

            return file.Item1;
        }
    }
}