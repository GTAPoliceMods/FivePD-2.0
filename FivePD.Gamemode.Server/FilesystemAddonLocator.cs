// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System;
using System.Collections.Generic;
using System.IO;
using FivePD.Gamemode.Server.Models;
using Newtonsoft.Json;
using Serilog;
using Cfx = CitizenFX.Core.Native;

namespace FivePD.Gamemode.Server
{
    /**
     * <summary>Responsible for locating addon definitions. Loading of addons should be.</summary>
     */
    public class FilesystemAddonLocator
    {
        private const string MetadataFileName = "addon.fpd.json";

        private static readonly string ResourcePath = Cfx.API.GetResourcePath(Cfx.API.GetCurrentResourceName());

        private static readonly string[] AddonDirectories =
        {
            Path.Combine(ResourcePath, "Libraries/IA"),
        };

        private readonly ILogger _logger;

        /**
         * <summary>Initializes a new instance of the <see cref="FilesystemAddonLocator"/> class
         * powered by the attached file system.</summary>
         * <param name="logger">A logger instance to use.</param>
         */
        public FilesystemAddonLocator(ILogger logger)
        {
            this._logger = logger;
        }

        /**
         * <summary>Returns all found addon definitions.</summary>
         * <returns>An enumerable of all addon definitions located.</returns>
         */
        public IEnumerable<AddonDefinition> GetAddons()
        {
            var definitions = new List<AddonDefinition>();

            this._logger.Information("Beginning addon locate via file system driver");

            foreach (var directory in AddonDirectories)
            {
                var addonMetadataLocations = FindAddonsInDirectory(new DirectoryInfo(directory));

                foreach (var addonMetadataLocation in addonMetadataLocations)
                {
                    try
                    {
                        var metadata = JsonConvert.DeserializeObject<AddonMetadata>(File.ReadAllText(addonMetadataLocation));

                        this._logger.Debug("Found addon metadata for {@Addon}", metadata.FriendlyName);

                        var definition = new AddonDefinition()
                        {
                            Name = Path.GetFileName(Path.GetDirectoryName(addonMetadataLocation)),
                            FriendlyName = metadata.FriendlyName,
                            Version = metadata.Version,
                            FriendlyVersion = metadata.FriendlyVersion,
                            IsInternal = true,
                            Assemblies = new List<byte[]>(),
                        };

                        var addonDirectory = Directory.GetParent(addonMetadataLocation).ToString();

                        foreach (var assembly in metadata.Assemblies)
                        {
                            // todo: path validation and security
                            var assemblyPath = Path.Combine(addonDirectory, assembly);

                            if (!File.Exists(assemblyPath))
                            {
                                this._logger.Warning("Tried to find an addon assembly that doesn't exist ({Assembly}). Is the {MetadataFileName} up-to-date?", assembly, MetadataFileName);

                                continue;
                            }

                            this._logger.Debug("Located addon assembly {Assembly}", assembly);

                            try
                            {
                                definition.Assemblies.Add(ValidateAssemblyAndReadBytes(assemblyPath));
                            }
                            catch (Exception e)
                            {
                                this._logger.Warning("Failed to read addon assembly file {Assembly}. {Message}", assembly, e.Message);
                            }
                        }

                        definitions.Add(definition);
                    }
                    catch (Exception e)
                    {
                       this._logger.Warning("An exception occurred while loading the addon with metadata path {Path}. {Message}", addonMetadataLocation, e.Message);
                    }
                }
            }

            this._logger.Information("Found {@Count} addons, passing back to bootstrapper", definitions.Count);

            return definitions;
        }

        private static IEnumerable<string> FindAddonsInDirectory(DirectoryInfo directory)
        {
            var addons = new List<string>();
            foreach (var subdirectory in directory.GetDirectories())
            {
                var metadataLocation = Path.Combine(subdirectory.ToString(), MetadataFileName);

                if (!File.Exists(metadataLocation))
                {
                    continue;
                }

                addons.Add(metadataLocation);
            }

            return addons;
        }

        private static byte[] ValidateAssemblyAndReadBytes(string path)
        {
            if (Path.GetExtension(path) != ".dll")
            {
                throw new ArgumentException("The provided file is not a library (DLL)");
            }

            return File.ReadAllBytes(path);
        }
    }
}