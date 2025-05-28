// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using FivePD.Common.Models;
using Newtonsoft.Json;

#pragma warning disable CS1591
#pragma warning disable SA1600

namespace FivePD.Gamemode.Server.NuiModels
{
    public class PedSearchFields
    {
        public PedSearchFields()
        {
            this.Firstname = string.Empty;
            this.Lastname = string.Empty;
            this.Birthdate = default(Date);
        }

        [JsonProperty]
        public string Firstname { get; private set; }

        [JsonProperty]
        public string Lastname { get; private set; }

        [JsonProperty]
        public Date Birthdate { get; private set; }

        public override string ToString()
        {
            return $"{this.Firstname} - {this.Lastname} - {this.Birthdate}";
        }
    }
}