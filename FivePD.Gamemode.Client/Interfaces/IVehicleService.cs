// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using CitizenFX.Core;
using FivePD.Common.Models;

namespace FivePD.Gamemode.Client.Interfaces
{
    /// <summary>
    /// Handles all vehicle related features.
    /// </summary>
    public interface IVehicleService
    {
        /// <summary>
        /// Generates a vehicle's <see cref="FVehicle"/> object.
        /// </summary>
        /// <param name="vehicle">The vehicle that's data need to be generated and stored on the server.</param>
        public void GenerateVehicle(Vehicle vehicle);
    }
}