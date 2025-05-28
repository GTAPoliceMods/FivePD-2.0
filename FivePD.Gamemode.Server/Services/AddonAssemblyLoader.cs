// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FivePD.Gamemode.Server.API;
using FivePD.Gamemode.Server.Interfaces;
using Serilog;

namespace FivePD.Gamemode.Server.Services
{
    /// <inheritdoc />
    public class AddonAssemblyLoader : IAddonAssemblyLoader
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddonAssemblyLoader"/> class.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to use.</param>
        public AddonAssemblyLoader(ILogger logger)
        {
            this._logger = logger;
        }

        /// <inheritdoc />
        public Assembly LoadAssemblyFromBytes(byte[] assemblyBytes)
        {
            var asm = Assembly.Load(assemblyBytes);

            foreach (var type in asm.GetTypes().Where(x => typeof(IGamemodeAddon).IsAssignableFrom(x)))
            {
                this._logger.Debug("Found class {@Type} that identified as an addon entrypoint", type.FullName);
            }

            return asm;
        }
    }
}