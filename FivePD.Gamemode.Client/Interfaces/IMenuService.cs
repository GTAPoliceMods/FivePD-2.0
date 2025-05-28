// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

namespace FivePD.Gamemode.Client.Interfaces
{
    /// <summary>
    /// Handles all Nui menu related events.
    /// </summary>
    public interface IMenuService
    {
        /// <summary>
        /// Registers the necessary events for the menus.
        /// </summary>
        public void RegisterEvents();

        /// <summary>
        /// Creates all menu objects.
        /// </summary>
        public void CreateMenus();

        /// <summary>
        /// Registers all menus' keybind and its items' actions.
        /// </summary>
        public void RegisterMenuKeybinds();
    }
}