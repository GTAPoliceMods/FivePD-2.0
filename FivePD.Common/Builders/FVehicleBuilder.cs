// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System;
using System.Collections.Generic;
using System.Linq;
using FivePD.Common.ConfigModels;
using FivePD.Common.Models;

#pragma warning disable CS1591
#pragma warning disable SA1600

namespace FivePD.Common.Builders
{
    /// <summary>
    /// Used for the construction of <see cref="FVehicle"/> objects.
    /// </summary>
    public class FVehicleBuilder
    {
        private readonly Random _random = new Random(Guid.NewGuid().GetHashCode());

        /// <summary>
        /// Initializes a new instance of the <see cref="FVehicleBuilder"/> class.
        /// </summary>
        /// <param name="networkId">The network identifier of the ped.</param>
        public FVehicleBuilder(int networkId)
        {
            this.NetworkId = networkId;
        }

        public int NetworkId { get; private set; }

        public int OwnerNetworkId { get; private set; }

        public List<int> PassengerNetworkIds { get; private set; }

        public string LicensePlate { get; private set; }

        public string Color { get; private set; }

        public string Brand { get; private set; }

        public List<Item> Items { get; private set; }

        public FVehicleBuilder WithOwner(int networkId)
        {
            this.OwnerNetworkId = networkId;
            return this;
        }

        public FVehicleBuilder WithPassengers(List<int> passengerNetworkIds)
        {
            this.PassengerNetworkIds = passengerNetworkIds;
            return this;
        }

        public FVehicleBuilder WithLicensePlate(string licensePlate)
        {
            this.LicensePlate = licensePlate;
            return this;
        }

        public FVehicleBuilder WithColor(string color)
        {
            this.Color = color;
            return this;
        }

        public FVehicleBuilder WithBrand(string brand)
        {
            this.Brand = brand;
            return this;
        }

        public FVehicleBuilder WithItems(List<Item> items)
        {
            this.Items = items
                .Where(item => item.Location == Item.ItemLocation.Vehicle || item.Location == Item.ItemLocation.Everywhere)
                .OrderBy(item => Guid.NewGuid())
                .Take(this._random.Next(0, 6))
                .ToList();
            return this;
        }

        /// <summary>
        /// Initializes a new <see cref="FVehicle"/> instance.
        /// </summary>
        /// <returns>Returns the newly created <see cref="FVehicle"/> instance.</returns>
        public FVehicle Build()
        {
            return new FVehicle(this);
        }
    }
}