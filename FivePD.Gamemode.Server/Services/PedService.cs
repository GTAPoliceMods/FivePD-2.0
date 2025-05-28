// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System;
using System.Collections.Concurrent;
using System.Dynamic;
using System.Linq;
using CitizenFX.Core;
using FivePD.Common;
using FivePD.Common.Builders;
using FivePD.Common.Models;
using FivePD.Gamemode.Server.Interfaces;
using FivePD.Gamemode.Server.NuiModels;
using Newtonsoft.Json;

namespace FivePD.Gamemode.Server.Services
{
    /// <inheritdoc />
    public class PedService : IPedService
    {
        private readonly EventHandlerDictionary _eventHandlerDictionary;
        private readonly IConfigService _configService;
        private readonly ConcurrentDictionary<int, FPed> _peds = new ConcurrentDictionary<int, FPed>();

        /// <summary>
        /// Initializes a new instance of the <see cref="PedService"/> class.
        /// </summary>
        /// <param name="eventHandlerDictionary">The <see cref="EventHandlerDictionary" /> to use.</param>
        /// <param name="configService">The <see cref="IConfigService" /> to use.</param>
        public PedService(EventHandlerDictionary eventHandlerDictionary, IConfigService configService)
        {
            this._eventHandlerDictionary = eventHandlerDictionary;
            this._configService = configService;
        }

        /// <inheritdoc />
        public void RegisterEvents()
        {
            this._eventHandlerDictionary[Events.Ped.Generate] += new Action<string>(this.GeneratePed);
            this._eventHandlerDictionary[Events.Ped.Search] += new Action<ExpandoObject, NetworkCallbackDelegate>(this.Search);
        }

        private void GeneratePed(string serializedFPedBuilder)
        {
            var fPedBuilder = JsonConvert.DeserializeObject<FPedBuilder>(serializedFPedBuilder);
            if (fPedBuilder is null || this._peds.ContainsKey(fPedBuilder.NetworkId))
            {
                return;
            }

            var fPed = fPedBuilder
                .WithFirstname(fPedBuilder.Gender == FGender.Female ? this._configService.GetFemaleFirstnames() : this._configService.GetMaleFirstnames())
                .WithLastname(this._configService.GetLastnames())
                .WithBirthdate()
                .WithItems(this._configService.GetItems())
                .Build();

            this._peds[fPed.NetworkId] = fPed;
            Debug.WriteLine($"{fPed}");
        }

        private void Search(ExpandoObject searchFieldsObject, NetworkCallbackDelegate networkCallbackDelegate)
        {
            var searchFields = JsonConvert.DeserializeObject<PedSearchFields>(JsonConvert.SerializeObject(searchFieldsObject));
            if (searchFields is null)
            {
                networkCallbackDelegate.Invoke(string.Empty);
                return;
            }

            var peds = this._peds.AsQueryable();

            if (!string.IsNullOrEmpty(searchFields.Firstname))
            {
                peds = peds.Where(ped => ped.Value.Firstname.ToLower().Contains(searchFields.Firstname.ToLower()));
            }

            if (!string.IsNullOrEmpty(searchFields.Lastname))
            {
                peds = peds.Where(ped => ped.Value.Lastname.ToLower().Contains(searchFields.Lastname.ToLower()));
            }

            if (searchFields.Birthdate.Day != 0)
            {
                peds = peds.Where(ped => ped.Value.Birthdate.Day == searchFields.Birthdate.Day);
            }

            if (searchFields.Birthdate.Month != 0)
            {
                peds = peds.Where(ped => ped.Value.Birthdate.Month == searchFields.Birthdate.Month);
            }

            if (searchFields.Birthdate.Year != 0)
            {
                peds = peds.Where(ped => ped.Value.Birthdate.Year == searchFields.Birthdate.Year);
            }

            networkCallbackDelegate.Invoke(JsonConvert.SerializeObject(peds.Select(ped => ped.Value)));
        }
    }
}