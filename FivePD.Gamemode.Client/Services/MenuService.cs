// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using CitizenFX.Core;
using FivePD.Common;
using FivePD.Common.Extensions;
using FivePD.Gamemode.Client.Extensions;
using FivePD.Gamemode.Client.Interfaces;
using FivePD.Gamemode.Client.Menus;
using FivePD.Gamemode.Client.Models;
using FivePD.Gamemode.Client.Utilities;
using Newtonsoft.Json.Linq;
using Cfx = CitizenFX.Core.Native;

#pragma warning disable S3011

namespace FivePD.Gamemode.Client.Services
{
    /// <inheritdoc />
    public class MenuService : IMenuService
    {
        private readonly EventHandlerDictionary _eventHandlerDictionary;
        private readonly INuiService _nuiService;
        private readonly IKeybindService _keybindService;
        private readonly ILocalizationService _localizationService;
        private readonly IConfigService _configService;
        private readonly IEntityService _entityService;
        private readonly INotificationService _notificationService;
        private readonly List<Menu> _menus;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuService"/> class.
        /// </summary>
        /// <param name="eventHandlerDictionary">The <see cref="EventHandlerDictionary" /> to use.</param>
        /// <param name="nuiService">The <see cref="INuiService" /> to use.</param>
        /// <param name="keybindService">The <see cref="IKeybindService" /> to use.</param>
        /// <param name="localizationService">The <see cref="ILocalizationService" /> to use.</param>
        /// <param name="configService">The <see cref="IConfigService"/> to use.</param>
        /// <param name="entityService">The <see cref="IEntityService"/> to use.</param>
        /// <param name="notificationService">The <see cref="INotificationService"/> to use.</param>
        public MenuService(
            EventHandlerDictionary eventHandlerDictionary,
            INuiService nuiService,
            IKeybindService keybindService,
            ILocalizationService localizationService,
            IConfigService configService,
            IEntityService entityService,
            INotificationService notificationService)
        {
            this._eventHandlerDictionary = eventHandlerDictionary;
            this._nuiService = nuiService;
            this._keybindService = keybindService;
            this._localizationService = localizationService;
            this._configService = configService;
            this._entityService = entityService;
            this._notificationService = notificationService;
            this._menus = new List<Menu>();
        }

        /// <inheritdoc />
        public void RegisterEvents()
        {
            this._eventHandlerDictionary.AddNuiEvent(Events.NuiEventType.IsNuiReady, new Action<ExpandoObject, CallbackDelegate>((body, cb) =>
            {
                this.CheckMenusForConflictingKeys();
                this.SendToNui();
                cb.Invoke(string.Empty);
            }));
        }

        /// <inheritdoc />
        public void CreateMenus()
        {
            this._menus.AddRange(new Menu[]
            {
                new DutyMenu(this._localizationService, this._nuiService, this._configService, this._entityService),
            });
        }

        /// <inheritdoc />
        public void RegisterMenuKeybinds()
        {
            foreach (var menu in this._menus)
            {
                void OnPress()
                {
                    this._nuiService.Show(Events.NuiEventType.MenuView, menu.Id, false, false);
                }

                this._keybindService.RegisterKeybind($"fivepd:openMenu_${menu.Id}", "-", menu.Key, OnPress);

                foreach (var item in menu.Items)
                {
                    var eventName = $"fivepd:menu:trigger_item_{item.GetHashCode()}";
                    if (item.Type == MenuItem.MenuItemType.List)
                    {
                        this._eventHandlerDictionary.AddNuiEvent(eventName, new Action<int, CallbackDelegate>(
                            (index, cb) =>
                            {
                                this.GetPrivateMethod(item, "RaiseOnListItemSelect")?.Invoke(item, new object[] { index });
                                cb.Invoke(string.Empty);
                            }));

                        this._eventHandlerDictionary.AddNuiEvent($"fivepd:menu:list_onchange_{item.GetHashCode()}", new Action<int, CallbackDelegate>(
                            (index, cb) =>
                            {
                                this.GetPrivateMethod(item as MenuListItem, "RaiseOnListIndexChange")?.Invoke(item, new object[] { index });
                                cb.Invoke(string.Empty);
                            }));
                    }
                    else
                    {
                        this._eventHandlerDictionary.AddNuiEvent(eventName, new Action<int, CallbackDelegate>(
                            (index, cb) =>
                            {
                                this.GetPrivateMethod(item, "RaiseOnItemSelect")?.Invoke(item, new object[] { });
                                cb.Invoke(string.Empty);
                            }));
                    }
                }
            }
        }

        private void CheckMenusForConflictingKeys()
        {
            var keyCount = new Dictionary<string, int>();
            foreach (var key in this._menus.Select(menu => menu.Key))
            {
                if (keyCount.ContainsKey(key))
                {
                    keyCount[key] += 1;
                }
                else
                {
                    keyCount[key] = 0;
                }
            }

            foreach (var key in keyCount.Where(key => key.Value > 0).Select(key => key.Key))
            {
                var notificationText = new GtaStyleText().Red("[FivePD error]").NewLine().Reset().Build() +
                   this._localizationService.CurrentLocalization.ErrorLocalizations
                       .MultipleMenusWithSameKey.ReplaceParams(
                           new Dictionary<string, string>
                           {
                               { "key", new GtaStyleText().Red(key).Build() },
                           },
                           new GtaStyleText().Reset().Build());

                this._notificationService.ShowNotification(notificationText);
            }
        }

        private void SendToNui()
        {
            var menus = new JArray();
            foreach (var menu in this._menus)
            {
                var items = new JArray();
                foreach (var item in menu.Items)
                {
                    var itemData = new JObject
                    {
                        { "Title", item.Title },
                        { "Description", item.Description },
                        { "Hashcode", item.GetHashCode().ToString() },
                        { "Type", (int)item.Type },
                    };

                    if (item.Type == MenuItem.MenuItemType.List)
                    {
                        var casted = item as MenuListItem;
                        itemData.Add("Items", JArray.FromObject(casted?.Items));
                    }

                    items.Add(itemData);
                }

                menus.Add(new JObject
                {
                    { "Title", menu.Title },
                    { "UseLocalization", menu.UseLocalization },
                    { "Id", menu.Id },
                    { "Items", items },
                });
            }

            this._nuiService.SendMessage(new JObject
            {
                { "type", Events.NuiEventType.SendMenuObjects },
                { "data", menus },
            });
        }

        private MethodInfo GetPrivateMethod(object objectVariable, string methodName)
        {
            return objectVariable.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
        }
    }
}