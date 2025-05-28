// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System;
using CitizenFX.Core;
using Cfx = CitizenFX.Core.Native;

namespace FivePD.Gamemode.Client.Extensions
{
    /// <summary>
    /// Holds extension methods for <see cref="EventHandlerDictionary"/>.
    /// </summary>
    public static class EventHandlerDictionaryExtensions
    {
        /// <summary>
        /// Registers a Nui event.
        /// </summary>
        /// <param name="eventHandlerDictionary">The event handler dictionary to add the event for.</param>
        /// <param name="eventName">The event's name.</param>
        /// <param name="action">The action that'll be invoked when the event is sent from the Nui.</param>
        /// <typeparam name="T">The type of the request body.</typeparam>
        public static void AddNuiEvent<T>(this EventHandlerDictionary eventHandlerDictionary, string eventName, Action<T, CallbackDelegate> action)
        {
            Cfx.API.RegisterNuiCallbackType(eventName);
            eventHandlerDictionary[$"__cfx_nui:{eventName}"] += action;
        }
    }
}