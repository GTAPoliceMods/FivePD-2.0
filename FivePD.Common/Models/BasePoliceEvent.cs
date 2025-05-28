// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System;
using System.Collections.Generic;
using CitizenFX.Core;
using Newtonsoft.Json;

#pragma warning disable S2292

namespace FivePD.Common.Models
{
    /// <summary>
    /// A base event class which should be inherited by callouts, traffic stops and ambient events.
    /// </summary>
    public class BasePoliceEvent
    {
        private string _title;
        private FPoliceEventPriority _priority = FPoliceEventPriority.Code1;
        private string _address;

        /// <summary>
        /// Initializes a new instance of the <see cref="BasePoliceEvent"/> class.
        /// </summary>
        public BasePoliceEvent()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BasePoliceEvent"/> class.
        /// </summary>
        /// <param name="mainPlayerNetworkId">The network identifier of the firstly attached player.</param>
        /// <param name="coordinate">The position of this event.</param>
        public BasePoliceEvent(int mainPlayerNetworkId, Vector3 coordinate)
        {
            this.MainPlayer = mainPlayerNetworkId;
            this.Coordinate = coordinate;
            this.Id = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Gets the identifier of this event.
        /// </summary>
        [JsonProperty]
        public string Id { get; private set; }

        /// <summary>
        /// Gets the first player's network identifier who was attached to this event.
        /// </summary>
        [JsonProperty]
        public int MainPlayer { get; private set; }

        /// <summary>
        /// Gets all players' network identifier who were attached to this event after initialization.
        /// </summary>
        [JsonProperty]
        public List<int> AttachedPlayers { get; private set; }

        /// <summary>
        /// Gets or sets the title of this event.
        /// </summary>
        public string Title
        {
            get => this._title;

            set
            {
                this._title = value;

                // TODO: Update title in the NUI callout view and other places for all players.
            }
        }

        /// <summary>
        /// Gets or sets the address of this event.
        /// </summary>
        public string Address
        {
            get => this._address;

            set
            {
                this._address = value;

                // TODO: Update address in the NUI callout view and other places for all players.
            }
        }

        /// <summary>
        /// Gets the coordinate of this event.
        /// </summary>
        [JsonProperty]
        public Vector3 Coordinate { get; private set; }

        /// <summary>
        /// Gets or sets the priority of this event.
        /// </summary>
        public FPoliceEventPriority Priority
        {
            get => this._priority;

            set
            {
                this._priority = value;

                // TODO: Update priority in the NUI callout view and other places for all players.
            }
        }
    }
}