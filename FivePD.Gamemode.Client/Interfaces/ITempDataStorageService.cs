// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using CitizenFX.Core;

namespace FivePD.Gamemode.Client.Interfaces
{
    /// <summary>
    /// Responsible for getting temporary data.
    /// </summary>
    public interface ITempDataStorageService
    {
        /// <summary>
        /// Gets a value based off a key.
        /// </summary>
        /// <param name="entity">Entity to set data on.</param>
        /// <param name="key">Key to use.</param>
        /// <returns>Value associated with key.</returns>
        public dynamic Get(Entity entity, string key);
    }
}