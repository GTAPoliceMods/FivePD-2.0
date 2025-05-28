// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System;
using FivePD.Gamemode.Client.Injection;

namespace FivePD.Gamemode.Client.Interfaces
{
    /// <summary>
    /// Used to register keybindings.
    /// </summary>
    public interface IKeybindService
    {
        /// <summary>
        /// Finds all methods with the Keybind attribute
        /// and register them.
        /// </summary>
        /// <param name="provider">The <see cref="ServiceProvider"/> to be used.</param>>
        public void Register(ServiceProvider provider);

        /// <summary>
        /// Registers a keybind and the given method to be invoked on press.
        /// </summary>
        /// <param name="id">The id of this keybind.</param>
        /// <param name="description">The description of this keybind.</param>
        /// <param name="key">The key of this keybind.</param>
        /// <param name="onPress">The method that'll be invoked on press.</param>
        /// <param name="onRelease">The method that'll be invoked on release.</param>
        public void RegisterKeybind(string id, string description, string key, Action onPress = null, Action onRelease = null);

        /// <summary>
        /// Registers a holdable keybind and the given method to be invoked on press.
        /// </summary>
        /// <param name="id">The id of this keybind.</param>
        /// <param name="description">The description of this keybind.</param>
        /// <param name="key">The key of this keybind.</param>
        /// <param name="timeToHoldInMs">Determines for how long the key should be pressed to trigger the action.</param>
        /// <param name="onPress">The method that'll be invoked on press.</param>
        /// <param name="onRelease">The method that'll be invoked on release.</param>
        /// <param name="canInvokeOnPress">The onPress method will only be invoked if this method returns true.</param>
        public void RegisterHoldableKeybind(string id, string description, string key, int timeToHoldInMs, Action onPress = null, Action onRelease = null, Func<bool> canInvokeOnPress = null);

        /// <summary>
        /// Registers a mouse keybind and the given method to be invoked on press.
        /// </summary>
        /// <param name="id">The id of this keybind.</param>
        /// <param name="description">The description of this keybind.</param>
        /// <param name="key">The key of this keybind.</param>
        /// <param name="onPress">The method that'll be invoked on press.</param>
        /// <param name="onRelease">The method that'll be invoked on release.</param>
        public void RegisterMouseKeybind(string id, string description, string key, Action onPress = null, Action onRelease = null);
    }
}