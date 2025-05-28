// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System.Collections.Generic;
using CitizenFX.Core;
using FivePD.Common.Models;
using FivePD.Gamemode.Client.Interfaces;
using Newtonsoft.Json;
using Cfx = CitizenFX.Core.Native;

namespace FivePD.Gamemode.Client.Services
{
    /// <summary>
    /// <inheritdoc cref="ITempDataStorageService"/>
    /// </summary>
    public class TempDataStorageService : ClientScript, ITempDataStorageService
    {
        /// <inheritdoc />
        public dynamic Get(Entity entity, string key)
        {
            Dictionary<string, dynamic> keyPairValues = this.GetKeyValuePairs(entity);

            return keyPairValues.ContainsKey(key) ? keyPairValues[key] : null;
        }

        private Dictionary<string, dynamic> GetKeyValuePairs(Entity entity)
        {
            string bagName = $"{(FEntityType)Cfx.API.GetEntityType(entity.Handle)}:{entity.NetworkId}";
            dynamic bag = this.GlobalState.Get(bagName);

            if (bag != null)
            {
                return JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(bag);
            }

            Dictionary<string, dynamic> emptyDict = new Dictionary<string, dynamic>();

            this.GlobalState.Set(bagName, JsonConvert.SerializeObject(emptyDict), true);
            return emptyDict;
        }
    }
}