// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System;
using System.Collections.Concurrent;
using CitizenFX.Core;
using FivePD.Common;
using FivePD.Common.Builders;
using FivePD.Common.Models;
using FivePD.Gamemode.Server.Interfaces;
using Newtonsoft.Json;

namespace FivePD.Gamemode.Server.Services
{
    /// <inheritdoc />
    public class VehicleService : IVehicleService
    {
        private readonly EventHandlerDictionary _eventHandlerDictionary;
        private readonly IConfigService _configService;
        private readonly ConcurrentDictionary<int, FVehicle> _vehicles = new ConcurrentDictionary<int, FVehicle>();

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleService"/> class.
        /// </summary>
        /// <param name="eventHandlerDictionary">The <see cref="EventHandlerDictionary" /> to use.</param>
        /// <param name="configService">The <see cref="IConfigService" /> to use.</param>
        public VehicleService(EventHandlerDictionary eventHandlerDictionary, IConfigService configService)
        {
            this._eventHandlerDictionary = eventHandlerDictionary;
            this._configService = configService;
        }

        /// <inheritdoc />
        public void RegisterEvents()
        {
            this._eventHandlerDictionary[Events.Vehicle.Generate] += new Action<string>(this.GenerateVehicle);
        }

        private void GenerateVehicle(string serializedFVehicleBuilder)
        {
            var fVehicleBuilder = JsonConvert.DeserializeObject<FVehicleBuilder>(serializedFVehicleBuilder);
            if (fVehicleBuilder is null || this._vehicles.ContainsKey(fVehicleBuilder.NetworkId))
            {
                return;
            }

            var fVehicle = fVehicleBuilder
                .WithItems(this._configService.GetItems())
                .Build();

            this._vehicles[fVehicle.NetworkId] = fVehicle;
        }
    }
}