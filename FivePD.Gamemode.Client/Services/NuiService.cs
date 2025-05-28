// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System;
using System.Dynamic;
using CitizenFX.Core;
using FivePD.Common;
using FivePD.Gamemode.Client.Attributes;
using FivePD.Gamemode.Client.Extensions;
using FivePD.Gamemode.Client.Interfaces;
using FivePD.Gamemode.Client.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Cfx = CitizenFX.Core.Native;

namespace FivePD.Gamemode.Client.Services
{
    /// <inheritdoc cref="INuiService" />
    public class NuiService : INuiService
    {
        private readonly EventHandlerDictionary _eventHandlerDictionary;
        private bool _isNuiOpened;

        /// <summary>Initializes a new instance of the <see cref="NuiService"/> class.</summary>
        /// <param name="eventHandlerDictionary">The <see cref="EventHandlerDictionary" /> to use.</param>>
        public NuiService(EventHandlerDictionary eventHandlerDictionary)
        {
            this._eventHandlerDictionary = eventHandlerDictionary;
        }

        /// <summary>
        /// Delegate for event <see cref="OnNuiHide"/>.
        /// </summary>
        public delegate void OnHide();

        /// <inheritdoc />
        public event OnHide OnNuiHide;

        /// <inheritdoc />
        public void RegisterEventHandlers()
        {
            this._eventHandlerDictionary.AddNuiEvent(Events.NuiEventType.Close, new Action<ExpandoObject, CallbackDelegate>((body, cb) =>
            {
                this.Hide();
                cb.Invoke(string.Empty);
            }));
        }

        /// <inheritdoc />
        [Keybind(KeybindIdentifiers.MenuClose, "BACK", "Close menu")]
        public void Hide()
        {
            this.SendMessage("{\"type\": \"" + Events.NuiEventType.Close + "\"}");
            Cfx.API.SetNuiFocus(false, false);
            this._isNuiOpened = false;
            this.OnNuiHide?.Invoke();
        }

        /// <inheritdoc />
        [Keybind(KeybindIdentifiers.OpenMDT, "B", "Open MDT")]
        public void OpenMdt()
        {
            this.Show(Events.NuiEventType.Mdt);
        }

        /// <inheritdoc />
        [Keybind(KeybindIdentifiers.MenuMoveUp, "UP", "Move up in menu")]
        public void MenuControlUp()
        {
            if (!this._isNuiOpened)
            {
                return;
            }

            this.SendMessage("{\"type\": \"" + Events.NuiEventType.MenuUp + "\"}");
        }

        /// <inheritdoc />
        [Keybind(KeybindIdentifiers.MenuMoveDown, "DOWN", "Move down in menu")]
        public void MenuControlDown()
        {
            if (!this._isNuiOpened)
            {
                return;
            }

            this.SendMessage("{\"type\": \"" + Events.NuiEventType.MenuDown + "\"}");
        }

        /// <inheritdoc />
        [Keybind(KeybindIdentifiers.MenuMoveLeft, "LEFT", "Move left in menu")]
        public void MenuControlLeft()
        {
            if (!this._isNuiOpened)
            {
                return;
            }

            this.SendMessage("{\"type\": \"" + Events.NuiEventType.MenuLeft + "\"}");
        }

        /// <inheritdoc />
        [Keybind(KeybindIdentifiers.MenuMoveRight, "RIGHT", "Move right in menu")]
        public void MenuControlRight()
        {
            if (!this._isNuiOpened)
            {
                return;
            }

            this.SendMessage("{\"type\": \"" + Events.NuiEventType.MenuRight + "\"}");
        }

        /// <inheritdoc />
        [Keybind(KeybindIdentifiers.MenuItemSelect, "RETURN", "Selects item in menu")]
        public void MenuControlSelect()
        {
            if (!this._isNuiOpened)
            {
                return;
            }

            this.SendMessage("{\"type\": \"" + Events.NuiEventType.MenuSelect + "\"}");
        }

        /// <inheritdoc />
        public void ShowRadialMenu(string menuId)
        {
            // Centers cursor
            Cfx.API.SetCursorLocation(0.5f, 0.5f);

            this.Show(Events.NuiEventType.RadialMenu, menuId, false, false);
        }

        /// <param name="timeToHoldInMs"></param>
        /// <inheritdoc />
        public void ShowTimerBar(int timeToHoldInMs)
        {
            Cfx.API.SetNuiFocus(false, false);
            this.SendMessage(new JObject
            {
                { "showTimerBar", true },
                { "timerBarTitle", "test" },
                { "timerBarTimeToHold", timeToHoldInMs },
            });
        }

        /// <inheritdoc />
        public void SendMessage(string data)
        {
            Cfx.API.SendNuiMessage(data);
        }

        /// <inheritdoc />
        public void SendMessage(JObject data)
        {
            Cfx.API.SendNuiMessage(JsonConvert.SerializeObject(data));
        }

        /// <inheritdoc />
        public void Show(string type, string menuId = "#", bool hasFocus = true, bool hasCursor = true)
        {
            if (this._isNuiOpened && type != Events.NuiEventType.Mdt)
            {
                this.Hide();
                return;
            }

            this._isNuiOpened = true;

            this.SendMessage(new JObject
            {
                { "type", type },
                { "menuId", menuId },
            });
            Cfx.API.SetNuiFocus(hasFocus, hasCursor);
        }
    }
}