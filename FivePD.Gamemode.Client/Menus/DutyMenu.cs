// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;
using FivePD.Common;
using FivePD.Common.ConfigModels;
using FivePD.Common.ConfigModels.MenuConfigs;
using FivePD.Gamemode.Client.Interfaces;
using FivePD.Gamemode.Client.Models;
using Newtonsoft.Json.Linq;
using Cfx = CitizenFX.Core.Native;

#pragma warning disable S1450
#pragma warning disable CS4014
#pragma warning disable S3241

namespace FivePD.Gamemode.Client.Menus
{
    /// <inheritdoc />
    public class DutyMenu : Menu
    {
        private readonly ILocalizationService _localizationService;
        private readonly INuiService _nuiService;
        private readonly IEntityService _entityService;
        private readonly List<Loadout> _loadouts;
        private readonly List<Vehicles.PoliceVehicle> _policeVehicles;
        private readonly DutyMenuConfig _config;

        private MenuCheckboxItem _dutyCheckboxItem;
        private MenuListItem _spawnVehicleListItem;
        private MenuListItem _getLoadoutListItem;
        private MenuListItem _changeLanguageListItem;

        /// <summary>
        /// Initializes a new instance of the <see cref="DutyMenu"/> class.
        /// </summary>
        /// <param name="localizationService">The <see cref="ILocalizationService"/> to use.</param>
        /// <param name="nuiService">The <see cref="INuiService"/> to use.</param>
        /// <param name="configService">The <see cref="IConfigService"/> to use.</param>
        /// <param name="entityService">The <see cref="IEntityService"/> to use.</param>
        /// >
        public DutyMenu(ILocalizationService localizationService, INuiService nuiService, IConfigService configService, IEntityService entityService)
            : base("duty_menu", "DutyMenu.Title", "F10", true)
        {
            this._localizationService = localizationService;
            this._nuiService = nuiService;
            this._entityService = entityService;

            this._loadouts = configService.GetLoadoutsConfig();
            this._policeVehicles = configService.GetVehiclesConfig().Police;
            this._config = configService.GetMenuConfig().DutyMenu;
            this.CreateAndSetItems();
            this.WaitAndUpdateLocales();
        }

        private void CreateAndSetItems()
        {
            var items = new List<MenuItem>();

            if (this._config.DutyToggle.Enabled)
            {
                this._dutyCheckboxItem = new MenuCheckboxItem("DutyMenu.OnDuty.Title", "DutyMenu.OnDuty.Description");
                items.Add(this._dutyCheckboxItem);
            }

            if (this._config.SpawnVehicle.Enabled)
            {
                this._spawnVehicleListItem = new MenuListItem("DutyMenu.SpawnVehicle.Title", "DutyMenu.SpawnVehicle.Description", this._policeVehicles.Select(vehicle => vehicle.Name).ToList());
                this._spawnVehicleListItem.OnListItemSelect += index =>
                {
                    var policeVehicle = this._policeVehicles.ElementAt(index);
                    this._entityService.SpawnVehicle((uint)Cfx.API.GetHashKey(policeVehicle.Model), Game.PlayerPed.Position + (Game.PlayerPed.ForwardVector * 5f), Game.PlayerPed.Heading);
                };
                items.Add(this._spawnVehicleListItem);
            }

            if (this._config.GetLoadout.Enabled)
            {
                this._getLoadoutListItem = new MenuListItem("DutyMenu.GetLoadout.Title", "DutyMenu.GetLoadout.Description", this._loadouts.Select(x => x.Title).ToList());
                this._getLoadoutListItem.OnListItemSelect += index =>
                {
                    var loadout = this._loadouts.ElementAt(index);

                    foreach (var weaponItem in loadout.Weapons)
                    {
                        var weapon = (WeaponHash)Cfx.API.GetHashKey(weaponItem.Key);
                        Game.PlayerPed.Weapons.Give(weapon, weaponItem.Ammunition, false, true);

                        foreach (var componentKey in weaponItem.ComponentKeys)
                        {
                            var componentHash = (uint)Cfx.API.GetHashKey(componentKey);
                            var weaponHash = (uint)weapon;
                            if (!Cfx.API.DoesWeaponTakeWeaponComponent(weaponHash, componentHash))
                            {
                                continue;
                            }

                            Cfx.API.GiveWeaponComponentToPed(Game.PlayerPed.Handle, weaponHash, componentHash);
                        }
                    }
                };
                items.Add(this._getLoadoutListItem);
            }

            this._changeLanguageListItem = new MenuListItem("DutyMenu.ChangeLanguage.Title", "DutyMenu.ChangeLanguage.Description", this._localizationService.Locales.Select(x => x.Title).ToList());
            this._changeLanguageListItem.OnListItemSelect += index =>
            {
                var locale = this._localizationService.Locales.ElementAt(index);
                this._localizationService.LoadLocalization(locale.Key);
                this._nuiService.SendMessage(new JObject
                {
                    { "type", Events.NuiEventType.LocalizationChange },
                    { "data", JObject.FromObject(this._localizationService.CurrentLocalization) },
                });
            };
            items.Add(this._changeLanguageListItem);

            this.SetItems(items);
        }

        private async Task WaitAndUpdateLocales()
        {
            var locales = await this._localizationService.LoadAvailableLocales();
            this._changeLanguageListItem.SetItems(locales.Select(locale => locale.Title).ToList());
        }
    }
}