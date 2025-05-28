// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System.Collections.Generic;

namespace FivePD.Common.ConfigModels
{
    /// <summary>
    /// Contains the definition of a loadout.
    /// </summary>
    public class Loadout
    {
        /// <summary>
        /// Gets or sets the title of this loadout.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets an array of weapons in this loadout.
        /// </summary>
        public List<Weapon> Weapons { get; set; } = new List<Weapon>();

        /// <summary>
        /// A loadout weapon.
        /// </summary>
        public class Weapon
        {
            /// <summary>
            /// Gets or sets the key of this weapon.
            /// </summary>
            public string Key { get; set; } = string.Empty;

            /// <summary>
            /// Gets or sets the components of this weapon.
            /// </summary>
            public List<string> ComponentKeys { get; set; } = new List<string>();

            /// <summary>
            /// Gets or sets the ammunition count of this weapon.
            /// </summary>
            public int Ammunition { get; set; } = 0;
        }
    }
}