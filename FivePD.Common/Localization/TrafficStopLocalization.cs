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
    /// Contains all keys that are used for traffic stop related messages.
    /// </summary>
    public class TrafficStopLocalization
    {
        public string Initiate { get; set; } = "Turn on your sirens to {{initiate(initiate)}} the traffic stop.";

        public string WaitUntilStop { get; set; } = "Wait until the vehicle has come to a stop, then proceed with the traffic stop.";

        public string Proceed { get; set; } = "The vehicle has stopped. Proceed with the traffic stop.";

        public string Canceled { get; set; } = "You've canceled the traffic stop.";

        public string StoppedVehicle { get; set; } = "Stopped vehicle";
    }
}