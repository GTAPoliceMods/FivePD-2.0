// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

namespace FivePD.Gamemode.Server.Interfaces
{
    /// <summary>
    /// Provides access to basic logging
    /// functionality for addon usage.
    /// </summary>
    public interface IAddonLoggerService
    {
        /// <summary>
        /// Logs a message with the information label.
        /// </summary>
        /// <param name="message">The text to write.</param>
        public void Information(string message);
    }
}