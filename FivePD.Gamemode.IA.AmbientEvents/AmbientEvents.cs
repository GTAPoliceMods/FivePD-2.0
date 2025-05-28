// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System;
using System.Reflection;
using System.Threading.Tasks;
using FivePD.Gamemode.Server.API;
using FivePD.Gamemode.Server.Interfaces;
using Serilog.Core;

namespace FivePD.Gamemode.IA.AmbientEvents
{
    /**
     * <summary>Responsible for creating and management various ambient
     * events around players (e.x.: carjackings or speeders).</summary>
     */
    [AddonMetadata("AmbientEventProvider", "1.0.0", 100)]
    public class AmbientEvents : IGamemodeAddon
    {
        private readonly IAddonLoggerService _logger;

        public AmbientEvents(IAddonLoggerService logger)
        {
            this._logger = logger;
        }

        /**
         * <inheritdoc />
         */
        public Task OnStarted()
        {
            this._logger.Information("I started!");

            return Task.CompletedTask;
        }

        /**
         * <inheritdoc />
         */
        public Task OnStopped()
        {
            this._logger.Information("I stopped!");

            return Task.CompletedTask;
        }
    }
}