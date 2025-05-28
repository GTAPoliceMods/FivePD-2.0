// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using CitizenFX.Core;

namespace FivePD.Gamemode.Server.Interfaces
{
    /// <summary>
    /// Responsible for setting/getting temporary data.
    /// </summary>
    public interface ITempDataStorageService
    {
        /// <summary>
        /// Sets a value based off a key.
        /// </summary>
        /// <param name="entity">Entity to set data on.</param>
        /// <param name="key">Key to use.</param>
        /// <param name="value">Value to set.</param>
        public void Set(Entity entity, string key, dynamic value);

        /// <summary>
        /// Gets a value based off a key.
        /// </summary>
        /// <param name="entity">Entity to set data on.</param>
        /// <param name="key">Key to use.</param>
        /// <returns>Value associated with key.</returns>
        public dynamic Get(Entity entity, string key);
    }
}