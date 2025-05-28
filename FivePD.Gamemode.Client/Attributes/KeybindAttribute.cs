// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System;

namespace FivePD.Gamemode.Client.Attributes
{
    /// <summary>
    /// Used to bind methods to given keys.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class KeybindAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeybindAttribute"/> class.
        /// </summary>
        /// <param name="id">Identifier of the keybind. For example: "fivepd_open_mdt".</param>
        /// <param name="key">The key's name which will be binded to the given method.</param>
        /// <param name="description">Description of the keybind that will be showed in the settings.</param>
        public KeybindAttribute(string id, string key, string description = "")
        {
            this.Id = id;
            this.Key = key;
            this.Description = description;
        }

        /// <summary>
        /// Gets identifier of the binded action.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Gets key's name to bind.
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// Gets key's description.
        /// </summary>
        // TODO: Change description to JSON key for translation.
        public string Description { get; }

        /// <summary>
        /// Gets for how long the key should be pressed to trigger the action.
        /// </summary>
        public int TimeToHold { get; }

        /// <summary>
        /// Gets for how long the key should be pressed to trigger the action in milliseconds.
        /// </summary>
        public int TimeToHoldInMs { get; }
    }
}