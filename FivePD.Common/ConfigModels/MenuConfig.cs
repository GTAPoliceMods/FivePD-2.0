// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using FivePD.Common.ConfigModels.MenuConfigs;

namespace FivePD.Common.ConfigModels
{
    /// <summary>
    /// Contains the config of all official menus.
    /// </summary>
    public class MenuConfig
    {
        /// <summary>
        /// Gets or sets the duty menu config.
        /// </summary>
        public DutyMenuConfig DutyMenu { get; set; } = new DutyMenuConfig();
    }
}