// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

#pragma warning disable CS1591
#pragma warning disable SA1600

namespace FivePD.Common.Localization
{
    /// <summary>
    /// Contains all keys that are used within Duty menu.
    /// </summary>
    public class DutyMenuLocalization
    {
        public DutyMenuLocalization()
        {
            this.OnDuty = new OnDutyLocalization();
            this.GetLoadout = new GetLoadoutLocalization();
            this.ChangeLanguage = new ChangeLanguageLocalization();
            this.SpawnVehicle = new SpawnVehicleLocalization();
        }

        public string Title { get; set; } = "Duty menu";

        public OnDutyLocalization OnDuty { get; set; }

        public GetLoadoutLocalization GetLoadout { get; set; }

        public ChangeLanguageLocalization ChangeLanguage { get; set; }

        public SpawnVehicleLocalization SpawnVehicle { get; set; }

        public class OnDutyLocalization
        {
            public string Title { get; set; } = "On duty";

            public string Description { get; set; } = "Description of on duty.";
        }

        public class SpawnVehicleLocalization
        {
            public string Title { get; set; } = "Spawn vehicle";

            public string Description { get; set; } = "Description of spawn vehicle";
        }

        public class GetLoadoutLocalization
        {
            public string Title { get; set; } = "Get loadout";

            public string Description { get; set; } = "Description of get loadout";
        }

        public class ChangeLanguageLocalization
        {
            public string Title { get; set; } = "Change language";

            public string Description { get; set; } = "Description of change language";
        }
    }
}