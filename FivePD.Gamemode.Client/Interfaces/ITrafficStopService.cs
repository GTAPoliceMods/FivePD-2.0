// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

namespace FivePD.Gamemode.Client.Interfaces
{
    /// <summary>
    /// Responsible for handling traffic stops.
    /// </summary>
    public interface ITrafficStopService
    {
        /// <summary>
        /// Registers all traffic stop related keybinds.
        /// </summary>
        public void RegisterKeybinds();
    }
}