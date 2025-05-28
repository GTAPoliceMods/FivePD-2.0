// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using CitizenFX.Core;
using FivePD.Common;
using FivePD.Gamemode.Client.Extensions;
using FivePD.Gamemode.Client.Interfaces;
using FivePD.Gamemode.Client.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Cfx = CitizenFX.Core.Native;

namespace FivePD.Gamemode.Client.Services
{
    /// <inheritdoc />
    public class RadialMenuService : IRadialMenuService
    {
        private readonly EventHandlerDictionary _eventHandlerDictionary;
        private readonly INuiService _nuiService;
        private readonly IKeybindService _keybindService;
        private readonly ITickService _tickService;
        private readonly List<RadialMenu> _menus = new List<RadialMenu>();
        private bool _isAnyMenuVisible;
        private MouseCoordinate _lastMousePosition;

        /// <summary>Initializes a new instance of the <see cref="RadialMenuService"/> class.</summary>
        /// <param name="eventHandlerDictionary">The <see cref="EventHandlerDictionary" /> to use.</param>
        /// <param name="nuiService">The <see cref="INuiService" /> to use.</param>
        /// <param name="keybindService">The <see cref="IKeybindService" /> to use.</param>
        /// <param name="tickService">The <see cref="ITickService" /> to use.</param>
        public RadialMenuService(EventHandlerDictionary eventHandlerDictionary, INuiService nuiService, IKeybindService keybindService, ITickService tickService)
        {
            this._eventHandlerDictionary = eventHandlerDictionary;
            this._nuiService = nuiService;
            this._keybindService = keybindService;
            this._tickService = tickService;
        }

        /// <inheritdoc />
        public void RegisterEvents()
        {
            this._eventHandlerDictionary.AddNuiEvent(Events.NuiEventType.IsNuiReady, new Action<ExpandoObject, CallbackDelegate>((body, cb) =>
            {
                this.SendToNui();
                cb.Invoke(string.Empty);
            }));

            this._nuiService.OnNuiHide += () =>
            {
                this._isAnyMenuVisible = false;
                this._tickService.OnTick -= this.DisableMouseMovement;
                this._tickService.OnTick -= this.OnMouseMove;
            };
        }

        /// <inheritdoc />
        public void SendToNui()
        {
            var radialMenus = new JArray();
            foreach (var menu in this._menus)
            {
                var items = new JArray();
                foreach (var item in menu.Items)
                {
                    items.Add(new JObject
                    {
                        { "Title", item.Title },
                        { "Hashcode", item.GetHashCode().ToString() },
                    });
                }

                radialMenus.Add(new JObject
                {
                    { "Title", menu.Title },
                    { "Id", menu.Id },
                    { "Items", items },
                });
            }

            this._nuiService.SendMessage(new JObject
            {
                { "type", Events.NuiEventType.SendRadialMenuObjects },
                { "data", radialMenus },
            });
        }

        /// <inheritdoc />
        public void RegisterMenuKeybinds()
        {
            foreach (var menu in this._menus)
            {
                void OnPress()
                {
                    this._isAnyMenuVisible = true;
                    this._tickService.OnTick += this.DisableMouseMovement;
                    this._tickService.OnTick += this.OnMouseMove;
                    this._nuiService.ShowRadialMenu(menu.Id);
                }

                void OnRelease()
                {
                    this._nuiService.SendMessage(new JObject
                    {
                        { "type", Events.NuiEventType.RadialMenuControl },
                        { "control", "select" },
                    });
                    this._nuiService.Hide();
                }

                this._keybindService.RegisterKeybind($"fivepd:openRadialMenu_${menu.Id}", "-", menu.Key, OnPress, OnRelease);

                foreach (var item in menu.Items)
                {
                    var eventName = $"fivepd:radialMenu:trigger_item_{item.GetHashCode()}";
                    this._eventHandlerDictionary.AddNuiEvent(eventName, new Action<ExpandoObject, CallbackDelegate>((body, callback) => item.OnPress(callback)));
                }
            }
        }

        /// <inheritdoc />
        public void CreateMenus()
        {
            var menu = new RadialMenu("8fb0e291-dec2-44bd-b3c0-fc33b882838c", "Test radial menu", "H", new List<RadialMenuItem>
            {
                new RadialMenuItem("Item 1/1", cb =>
                {
                    Debug.WriteLine("Pressed item 1/1");
                    cb.Invoke(string.Empty);
                }),
                new RadialMenuItem("Item 1/2", cb =>
                {
                    Debug.WriteLine("Pressed item 1/2");
                    cb.Invoke(string.Empty);
                }),
            });
            this._menus.Add(menu);

            var menu2 = new RadialMenu("4142429d-3ed1-437e-8b7c-91b4b2a628b9", "Test radial menu 2", "J", new List<RadialMenuItem>
            {
                new RadialMenuItem("Item 2/1", cb =>
                {
                    Debug.WriteLine("Pressed item 2/1");
                    cb.Invoke(string.Empty);
                }),
                new RadialMenuItem("Item 2/2", cb =>
                {
                    Debug.WriteLine("Pressed item 2/2");
                    cb.Invoke(string.Empty);
                }),
            });
            this._menus.Add(menu2);
        }

        private Task DisableMouseMovement()
        {
            Cfx.API.DisableControlAction(0, 24, true); // Disable attack
            Cfx.API.DisableControlAction(0, 25, true); // Disable aim
            Cfx.API.DisableControlAction(0, 1, true); // LookLeftRight
            Cfx.API.DisableControlAction(0, 2, true); // LookUpDown

            return Task.FromResult(0);
        }

        private MouseCoordinate GetMouseCoordinates()
        {
            int screenWidth = 0;
            int screenHeight = 0;
            Cfx.API.GetActiveScreenResolution(ref screenWidth, ref screenHeight);
            float x = Cfx.API.GetDisabledControlNormal(2, 239);
            float y = Cfx.API.GetDisabledControlNormal(2, 240);
            return new MouseCoordinate(screenWidth * x, screenHeight * y);
        }

        private Task OnMouseMove()
        {
            if (!this._isAnyMenuVisible)
            {
                return Task.FromResult(0);
            }

            var coordinates = this.GetMouseCoordinates();
            if (Cfx.API.IsDisabledControlJustPressed(2, 237))
            {
                this._nuiService.SendMessage(new JObject
                {
                    { "type", Events.NuiEventType.RadialMenuControl },
                    { "control", "down" },
                    { "x", coordinates.X },
                    { "y", coordinates.Y },
                });
            }
            else if (Cfx.API.IsDisabledControlJustReleased(2, 237))
            {
                this._nuiService.SendMessage(new JObject
                {
                    { "type", Events.NuiEventType.RadialMenuControl },
                    { "control", "up" },
                    { "x", coordinates.X },
                    { "y", coordinates.Y },
                });
            }

            if (Math.Abs(coordinates.X - this._lastMousePosition.X) > 1 && Math.Abs(coordinates.Y - this._lastMousePosition.Y) > 1)
            {
                this._nuiService.SendMessage(new JObject
                {
                    { "type", Events.NuiEventType.RadialMenuControl },
                    { "control", "up" },
                    { "x", coordinates.X },
                    { "y", coordinates.Y },
                });
                this._lastMousePosition = new MouseCoordinate(coordinates.X, coordinates.Y);
            }

            return Task.FromResult(0);
        }

        private readonly struct MouseCoordinate
        {
            public MouseCoordinate(float x, float y)
            {
                this.X = x;
                this.Y = y;
            }

            public float X { get; }

            public float Y { get; }

            public override string ToString()
            {
                return $"X: {this.X} Y: {this.Y}";
            }
        }
    }
}