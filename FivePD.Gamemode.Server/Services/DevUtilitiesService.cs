// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System;
using System.Collections.Generic;
using CitizenFX.Core;
using FivePD.Gamemode.Server.Interfaces;
using Cfx = CitizenFX.Core.Native;

namespace FivePD.Gamemode.Server.Services
{
    /**
     * <inheritdoc />
     */
    public class DevUtilitiesService : IDevUtilitiesService
    {
        private readonly ICommandService _commandService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DevUtilitiesService"/> class.
        /// </summary>
        /// <param name="commandService">The <see cref="ICommandService"/> to use.</param>
        public DevUtilitiesService(ICommandService commandService)
        {
            this._commandService = commandService;
        }

        /**
         * <inheritdoc />
         */
        public void RegisterDevelopmentCommands()
        {
            this._commandService.RegisterCommand("clear", (source, args) => ClearAllEntities());
        }

        private static void ClearAllEntities()
        {
            foreach (int entity in Cfx.API.GetAllPeds())
            {
                Cfx.API.DeleteEntity(entity);
            }

            foreach (int entity in Cfx.API.GetAllVehicles())
            {
                Cfx.API.DeleteEntity(entity);
            }

            foreach (int entity in Cfx.API.GetAllObjects())
            {
                Cfx.API.DeleteEntity(entity);
            }
        }
    }
}