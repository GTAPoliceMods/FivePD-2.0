// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

namespace FivePD.Gamemode.Client.Interfaces
{
    /// <summary>
    /// Responsible for registering and triggering all events related to radial menus.
    /// </summary>
    public interface IRadialMenuService
    {
        /// <summary>
        /// Registers the necessary events for the radial menus.
        /// </summary>
        public void RegisterEvents();

        /// <summary>
        /// Registers the official menus.
        /// </summary>
        public void CreateMenus();

        /// <summary>
        /// Registers all menus' keybind and its items' actions.
        /// </summary>
        public void RegisterMenuKeybinds();

        /// <summary>
        /// Sends all radial menus to the Nui.
        /// </summary>
        public void SendToNui();
    }
}