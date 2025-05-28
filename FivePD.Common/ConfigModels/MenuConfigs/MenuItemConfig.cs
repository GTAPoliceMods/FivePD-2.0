// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

namespace FivePD.Common.ConfigModels.MenuConfigs
{
    /// <summary>
    /// Default config of menu items.
    /// </summary>
    public class MenuItemConfig
    {
        /// <summary>
        /// Gets or sets a value indicating whether the given menu item should be displayed.
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether the given menu item should be displayed only for admins.
        /// </summary>
        public bool AdminOnly { get; set; } = false;
    }
}