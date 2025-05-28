// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

namespace FivePD.Common.ConfigModels
{
    /// <summary>
    /// Contains the database settings.
    /// </summary>
    public class DatabaseConfig
    {
        /// <summary>
        /// Gets or sets the address of the database.
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the port of the database.
        /// </summary>
        public string Port { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the database.
        /// </summary>
        public string Database { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user used for connecting to the database.
        /// </summary>
        public string User { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the password used for connecting to the database.
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <inheritdoc />
        public override string ToString()
        {
            return $"server={this.Address};port={this.Port};database={this.Database};user={this.User};password={this.Password};";
        }
    }
}