// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

namespace FivePD.Gamemode.Server.Models
{
    /// <summary>
    /// Represents the 'users' table.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the license key.
        /// </summary>
        public string License { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user is an admin.
        /// </summary>
        public bool Admin { get; set; }

        /// <summary>
        /// Gets or sets the department which the user is joined to.
        /// </summary>
        public Department Department { get; set; }
    }
}