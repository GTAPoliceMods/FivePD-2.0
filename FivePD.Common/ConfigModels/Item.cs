// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

namespace FivePD.Common.ConfigModels
{
    /// <summary>
    /// Contains the definition of an item.
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Defines where the item can be found.
        /// </summary>
        public enum ItemLocation
        {
            /// <summary>
            /// The item can spawn both in vehicles and on peds.
            /// </summary>
            Everywhere,

            /// <summary>
            /// The item can only spawn in vehicles.
            /// </summary>
            Vehicle,

            /// <summary>
            /// The item can only spawn on peds.
            /// </summary>
            Ped,
        }

        /// <summary>
        /// Gets or sets the title of this loadout.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the item is illegal.
        /// </summary>
        public bool IsIllegal { get; set; } = false;

        /// <summary>
        /// Gets or sets where can the item spawn.
        /// </summary>
        public ItemLocation Location { get; set; } = ItemLocation.Everywhere;
    }
}