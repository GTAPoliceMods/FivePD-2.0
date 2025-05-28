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
    /// Handles all ped related features.
    /// </summary>
    public interface IPedService
    {
        /// <summary>
        /// Registers all ped related events.
        /// </summary>
        public void RegisterEvents();

        /// <summary>
        /// Registers all ped related keybinds.
        /// </summary>
        public void RegisterKeybinds();

        /// <summary>
        /// If the player is aiming stops the targeted ped, otherwise the closest one to the player.
        /// </summary>
        public void StopPed();

        /// <summary>
        /// Generates a ped's <see cref="FPed"/> object.
        /// </summary>
        /// <param name="ped">The ped that's data need to be generated and stored on the server.</param>
        public void GeneratePed(Ped ped);
    }
}