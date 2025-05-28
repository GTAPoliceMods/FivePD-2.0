// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using CitizenFX.Core;
using Newtonsoft.Json;

namespace FivePD.Common.Models
{
    /// <inheritdoc />
    public class TrafficStop : BasePoliceEvent
    {
        [JsonIgnore]
        private Vehicle _stoppedVehicle;

        /// <summary>
        /// Initializes a new instance of the <see cref="TrafficStop"/> class.
        /// </summary>
        public TrafficStop()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TrafficStop"/> class.
        /// </summary>
        /// <param name="mainPlayerNetworkId">The network identifier of the firstly attached player.</param>
        /// <param name="coordinate">The position of this traffic stop.</param>
        /// <param name="stoppedVehicle">The vehicle that was stopped by the player.</param>
        public TrafficStop(int mainPlayerNetworkId, Vector3 coordinate, Vehicle stoppedVehicle)
            : base(mainPlayerNetworkId, coordinate)
        {
            this.StoppedVehicle = stoppedVehicle;
        }

        /// <summary>
        /// Gets the network identifier of the stopped vehicle.
        /// </summary>
        public int StoppedVehicleNetworkId { get; private set; }

        /// <summary>
        /// Gets the stopped vehicle.
        /// </summary>
        [JsonIgnore]
        public Vehicle StoppedVehicle
        {
            get => this._stoppedVehicle;
            private set
            {
                this._stoppedVehicle = value;
                this.StoppedVehicleNetworkId = value.NetworkId;
            }
        }
    }
}