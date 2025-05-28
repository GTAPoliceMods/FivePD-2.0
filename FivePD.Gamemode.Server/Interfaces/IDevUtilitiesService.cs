// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

namespace FivePD.Gamemode.Server.Interfaces
{
    /**
     * <summary>Provides utilities for development. These
     * methods should only be called in non-prod servers.</summary>
     */
    public interface IDevUtilitiesService
    {
        /**
         * <summary>Registers commands intended for development
         * usage.</summary>
         */
        public void RegisterDevelopmentCommands();
    }
}