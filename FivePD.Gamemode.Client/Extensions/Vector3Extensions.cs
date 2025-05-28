// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using CitizenFX.Core;
using Cfx = CitizenFX.Core.Native;

namespace FivePD.Gamemode.Client.Extensions
{
    /// <summary>
    /// Holds extension methods for CitizenFX.Core.Client.Vector3.
    /// </summary>
    public static class Vector3Extensions
    {
        /// <summary>
        /// Gets the nearest street's name to the given position.
        /// </summary>
        /// <param name="position">A <see cref="Vector3"/> position.</param>
        /// <returns>A string representing the street's name.</returns>
        public static string GetStreetName(this Vector3 position)
        {
            uint streetHash = default(uint);
            uint crossingRoadHash = default(uint);
            Cfx.API.GetStreetNameAtCoord(position.X, position.Y, position.Z, ref streetHash, ref crossingRoadHash);

            string streetName = Cfx.API.GetStreetNameFromHashKey(streetHash);

            return streetName;
        }
    }
}