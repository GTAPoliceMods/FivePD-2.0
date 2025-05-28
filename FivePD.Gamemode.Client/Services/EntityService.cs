// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using FivePD.Common;
using FivePD.Gamemode.Client.Interfaces;
using Cfx = CitizenFX.Core.Native;

namespace FivePD.Gamemode.Client.Services
{
    /// <inheritdoc />
    public class EntityService : IEntityService
    {
        /// <inheritdoc />
        public Task<Ped> SpawnPed(uint model, Vector3 location, float heading)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public async Task<Vehicle> SpawnVehicle(uint model, Vector3 location, float heading)
        {
            bool hasServerResponded = false;
            int vehicleNetworkId = 0;

            BaseScript.TriggerServerEvent(Events.EntityManagement.RequestVehicle, model, location, heading, new Action<int>(networkId =>
            {
                hasServerResponded = true;
                vehicleNetworkId = networkId;
            }));

            while (!hasServerResponded)
            {
                await BaseScript.Delay(100);
            }

            return (Vehicle)Entity.FromNetworkId(vehicleNetworkId);
        }

        /// <inheritdoc />
        public Task<Prop> SpawnProp(int model, Vector3 location)
        {
            throw new System.NotImplementedException();
        }
    }
}