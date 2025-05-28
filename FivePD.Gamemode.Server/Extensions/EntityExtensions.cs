// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using CitizenFX.Core;
using FivePD.Gamemode.Server.Services;
using Cfx = CitizenFX.Core.Native;

namespace FivePD.Gamemode.Server.Extensions
{
    /// <summary>
    /// Holds extension methods for CitizenFX.Core.Entity.
    /// </summary>
    public static class EntityExtensions
    {
        private static readonly TempDataStorageService TempDataStorageService = new TempDataStorageService();

        /// <summary>
        /// Set temporary replicated data on this <see cref="Entity"/>.
        /// </summary>
        /// <param name="entity">Entity to set data for.</param>
        /// <param name="key">Key to set.</param>
        /// <param name="value">Value to set.</param>
        public static void SetTempData(this Entity entity, string key, dynamic value) => TempDataStorageService.Set(entity, key, value);

        /// <summary>
        /// Get temporary replicated data off this <see cref="Entity"/>.
        /// </summary>
        /// <param name="entity">Entity to get data for.</param>
        /// <param name="key">Key to get.</param>
        /// <returns>Temporary replicated data.</returns>
        public static dynamic GetTempData(this Entity entity, string key) => TempDataStorageService.Get(entity, key);
    }
}