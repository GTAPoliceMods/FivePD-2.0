// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using CitizenFX.Core;

namespace FivePD.Gamemode.Client.Extensions
{
    /// <summary>
    /// Holds extension methods for CitizenFX.Core.Ped.
    /// </summary>
    public static class PedExtensions
    {
        /// <summary>
        /// Sets the required flags for the ped to ignore default GTA events.
        /// </summary>
        /// <param name="ped">The ped to set the flags for.</param>
        /// <param name="state">True if it should ignore GTA events, otherwise false.</param>
        public static void KeepTask(this Ped ped, bool state)
        {
            ped.AlwaysKeepTask = state;
            ped.BlockPermanentEvents = state;
        }

        /// <summary>
        /// Sets a ped's relationship to the players.
        /// </summary>
        /// <param name="ped">The ped to set the relationship for.</param>
        /// <param name="relationship">The relationship to set.</param>
        public static void SetRelationShip(this Ped ped, Relationship relationship)
        {
            ped.RelationshipGroup.SetRelationshipBetweenGroups(Game.PlayerPed.RelationshipGroup, relationship, true);
        }
    }
}