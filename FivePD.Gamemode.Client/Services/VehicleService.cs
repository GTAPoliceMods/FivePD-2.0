// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System.Linq;
using CitizenFX.Core;
using FivePD.Common;
using FivePD.Common.Builders;
using FivePD.Gamemode.Client.Extensions;
using FivePD.Gamemode.Client.Interfaces;
using Newtonsoft.Json;

namespace FivePD.Gamemode.Client.Services
{
    /// <inheritdoc />
    public class VehicleService : IVehicleService
    {
        private readonly IPedService _pedService;

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleService"/> class.
        /// </summary>
        /// <param name="pedService">The <see cref="IPedService"/> to use.</param>
        public VehicleService(IPedService pedService)
        {
            this._pedService = pedService;
        }

        /// <inheritdoc />
        public void GenerateVehicle(Vehicle vehicle)
        {
            // TODO: Is the Occupants contains the Driver as well?
            this._pedService.GeneratePed(vehicle.Driver);
            foreach (var ped in vehicle.Occupants)
            {
                this._pedService.GeneratePed(ped);
            }

            var fVehicleBuilder = new FVehicleBuilder(vehicle.NetworkId)
                .WithOwner(vehicle.Driver.NetworkId)
                .WithPassengers(vehicle.Occupants.Select(ped => ped.NetworkId).ToList())
                .WithColor(vehicle.GetColorName())
                .WithBrand(vehicle.DisplayName);

            BaseScript.TriggerServerEvent(Events.Vehicle.Generate, JsonConvert.SerializeObject(fVehicleBuilder));
        }
    }
}