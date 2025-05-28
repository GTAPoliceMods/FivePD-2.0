// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System.Threading.Tasks;

namespace FivePD.Gamemode.Server.API
{
    /**
     * <summary>Represents a server-sided gamemode addon.</summary>
     */
    public interface IGamemodeAddon
    {
        /**
         * <summary>Executed when the addon is loaded by the GAR.</summary>
         * <returns>A task which should complete when the addon is ready.</returns>
         */
        Task OnStarted();

        /**
         * <summary>Executed when the GAR has requested the addon to stop.</summary>
         * <returns>A task which should complete when the addon is ready.</returns>
         */
        Task OnStopped();
    }
}