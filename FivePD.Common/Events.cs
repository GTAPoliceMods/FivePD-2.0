// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using FivePD.Common.Models;

namespace FivePD.Common
{
    /**
     * <summary>Contains constant definitions for event handler names.</summary>
     */
    public abstract class Events
    {
        // Follows naming convention "{resource}:{area}:{id}" OR "{resource}:{area}:{id}:{type}" where,
        // {resource} is "fivepd"
        // {area} is a camelCased descriptor of the applicable event location
        // {id} is a camelCased descriptor of the event itself
        // {type} is optional, either "server" or "client"

        /**
         * <summary>Contains definitions for events related to entity management.</summary>
         */
        public abstract class EntityManagement
        {
            /**
             * <summary>C->S: Used when a client wants to request that the server creates a ped.</summary>
             */
            public const string RequestPed = "fivepd:entityManagement:requestPed";

            /**
             * <summary>C->S: Used when a client wants to request that the server creates a ped in an existing vehicle.</summary>
             */
            public const string RequestPedInVehicle = "fivepd:entityManagement:requestPedInVehicle";

            /**
             * <summary>C->S: Used when a client wants to request that the server creates a vehicle.</summary>
             */
            public const string RequestVehicle = "fivepd:entityManagement:requestVehicle";

            /**
             * <summary>C->S: Used when a client wants to request that the server creates a prop (object).</summary>
             */
            public const string RequestProp = "fivepd:entityManagement:requestProp";
        }

        /// <summary>
        /// Contains definitions for <see cref="FPed"/> related events.
        /// </summary>
        public abstract class Ped
        {
            /// <summary>
            /// Used to generate a <see cref="FPed"/> on the server.
            /// </summary>
            public const string Generate = "fivepd:ped:generate";

            /// <summary>
            /// Used when a player searches for a ped in the Nui.
            /// </summary>
            public const string Search = "fivepd:ped:search";
        }

        /// <summary>
        /// Contains definitions for <see cref="FVehicle"/> related events.
        /// </summary>
        public abstract class Vehicle
        {
            /// <summary>
            /// Used to generate a <see cref="FVehicle"/> on the server.
            /// </summary>
            public const string Generate = "fivepd:vehicle:generate";
        }

        /// <summary>
        /// Contains definitions for events related to localization.
        /// </summary>
        public abstract class Localization
        {
            /// <summary>
            /// C->S: Used when a client wants to request all available locales.
            /// </summary>
            public const string GetAvailableLocalesFromServer = "fivepd:localization:getAvaibleLocalesFromServer";

            /// <summary>
            /// Used to request localization from Nui.
            /// </summary>
            public const string GetLocalizationFromNui = "fivepd:localization:getLocalizationFromNui";
        }

        /// <summary>
        /// The necessary event types used for communicating with the Nui.
        /// Should contains the same types as the Terminal project has.
        /// </summary>
        public abstract class NuiEventType
        {
            /// <summary>
            /// Invoked from the Nui when it's initialized..
            /// </summary>
            public const string IsNuiReady = "fivepd:nui:isNuiReady";

            /// <summary>Closes the Nui.</summary>
            public const string Close = "close";

            /// <summary>Opens the Nui and displays the MDT view.</summary>
            public const string Mdt = "view:mdt";

            /// <summary>Opens the Nui and displays a menu.</summary>
            public const string MenuView = "view:menu";

            /// <summary>Opens the Nui and displays a radial menu.</summary>
            public const string RadialMenu = "view:radial_menu";

            /// <summary>Opens the Nui and displays a timer bar.</summary>
            public const string TimerBar = "view:timer_bar";

            /// <summary>Moves up by one item in normal menus.</summary>
            public const string MenuUp = "menu:control_up";

            /// <summary>Moves down by one item in normal menus.</summary>
            public const string MenuDown = "menu:control_down";

            /// <summary>Moves left by one item in normal menus.</summary>
            public const string MenuLeft = "menu:control_left";

            /// <summary>Moves right by one item in normal menus.</summary>
            public const string MenuRight = "menu:control_right";

            /// <summary>Selects the current item.</summary>
            public const string MenuSelect = "menu:control_select";

            /// <summary>Updates a menu item's specific value.</summary>
            public const string UpdateMenuItem = "menu:update_item";

            /// <summary>Sends the current localization to the Nui.</summary>
            public const string LocalizationChange = "localization:change";

            /// <summary>Sends all menus to the Nui.</summary>
            public const string SendMenuObjects = "send:menus";

            /// <summary>Sends all radial menus to the Nui.</summary>
            public const string SendRadialMenuObjects = "send:radialMenus";

            /// <summary>Sends radial menu control events (mouse move up, down, select) to the Nui.</summary>
            public const string RadialMenuControl = "radial_menu:control";

            /// <summary>Sends an event to the Nui to play a sound effect.</summary>
            public const string PlaySound = "sound:play";

            /// <summary>Sends an event to the Nui to read out a text.</summary>
            public const string Speak = "sound:speak";
        }

        /// <summary>
        /// Contains definitions for <see cref="BasePoliceEvent"/> related events.
        /// </summary>
        public abstract class TrafficStop
        {
            /// <summary>
            /// Used to store an event on the server.
            /// </summary>
            public const string Initiate = "fivepd:trafficStop:initiate";

            /// <summary>
            /// Used to remove an event from the server and it also detaches all attached players from it.
            /// </summary>
            public const string Cancel = "fivepd:trafficStop:cancel";

            /// <summary>
            /// Used to get if a vehicle is attached to a vehicle or not.
            /// </summary>
            public const string IsVehicleAttached = "fivepd:trafficStop:isVehicleAttached";
        }
    }
}