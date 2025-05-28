// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

namespace FivePD.Gamemode.Client.Utilities
{
    /// <summary>
    /// Contains the identifiers of all FivePD keybindings.
    /// </summary>
    public abstract class KeybindIdentifiers
    {
        /// <summary>
        /// Initiates or cancels a client's traffic stop.
        /// </summary>
        public const string TrafficStopToggle = "fivepd:trafficStopToggle";

        /// <summary>
        /// Stops the closest or the targeted ped.
        /// </summary>
        public const string StopPed = "fivepd:stopPed";

        /// <summary>
        /// Closes any opened menu.
        /// </summary>
        public const string MenuClose = "fivepd:menuClose";

        /// <summary>
        /// Moves up by one item in menus.
        /// </summary>
        public const string MenuMoveUp = "fivepd:menuMoveUp";

        /// <summary>
        /// Moves down by one item in menus.
        /// </summary>
        public const string MenuMoveDown = "fivepd:menuMoveDown";

        /// <summary>
        /// Moves left by one item in menus.
        /// </summary>
        public const string MenuMoveLeft = "fivepd:menuMoveLeft";

        /// <summary>
        /// Moves right by one item in menus.
        /// </summary>
        public const string MenuMoveRight = "fivepd:menuMoveRight";

        /// <summary>
        /// Invokes the action of the currently selected menu item.
        /// </summary>
        public const string MenuItemSelect = "fivepd:menuItemSelect";

        /// <summary>
        /// Opens the MDT.
        /// </summary>
        public const string OpenMDT = "fivepd:openMdt";
    }
}