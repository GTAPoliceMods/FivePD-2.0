// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

namespace FivePD.Common.ConfigModels.MenuConfigs
{
    /// <summary>
    /// The config of the duty menu.
    /// </summary>
    public class DutyMenuConfig
    {
        /// <summary>
        /// Gets or sets the config of the duty status toggle item.
        /// </summary>
        public MenuItemConfig DutyToggle { get; set; } = new MenuItemConfig();

        /// <summary>
        /// Gets or sets the config of the vehicle spawn item.
        /// </summary>
        public MenuItemConfig SpawnVehicle { get; set; } = new MenuItemConfig();

        /// <summary>
        /// Gets or sets the config of the loadout spawner item.
        /// </summary>
        public MenuItemConfig GetLoadout { get; set; } = new MenuItemConfig();
    }
}