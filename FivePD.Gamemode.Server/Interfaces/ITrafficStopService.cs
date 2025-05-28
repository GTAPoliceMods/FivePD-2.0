// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using FivePD.Common.Models;

namespace FivePD.Gamemode.Server.Interfaces
{
    /// <summary>
    /// Responsible for handling the <see cref="TrafficStop"/> related events.
    /// </summary>
    public interface ITrafficStopService
    {
        /// <summary>
        /// Registers all police event related events.
        /// </summary>
        public void RegisterEvents();
    }
}