// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using FivePD.Common.Models;

namespace FivePD.Gamemode.Server.Interfaces
{
    /// <summary>
    /// Responsible for <see cref="FVehicle"/> related tasks.
    /// </summary>
    public interface IVehicleService
    {
        /// <summary>
        /// Registers all ped related events at startup.
        /// </summary>
        public void RegisterEvents();
    }
}