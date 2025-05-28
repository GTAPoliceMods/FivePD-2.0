// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System.Collections.Generic;
using FivePD.Common.Builders;
using FivePD.Common.ConfigModels;

namespace FivePD.Common.Models
{
    /**
     * <summary>The base vehicle class for the
     * gamemode. Provides a wrapper around game
     * functionality for easier interactions
     * with the framework.</summary>
     */
    public class FVehicle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FVehicle"/> class.
        /// </summary>
        /// <param name="builder">The builder object that'll be used to initialize this instance.</param>
        public FVehicle(FVehicleBuilder builder)
        {
            this.NetworkId = builder.NetworkId;
            this.LicensePlate = builder.LicensePlate;
            this.Color = builder.Color;
            this.Brand = builder.Brand;
            this.Items = builder.Items;
        }

        /// <summary>
        /// Gets the vehicle's network identifier.
        /// </summary>
        public int NetworkId { get; }

        /// <summary>
        /// Gets the vehicle's license plate.
        /// </summary>
        public string LicensePlate { get; }

        /// <summary>
        /// Gets the vehicle's color.
        /// </summary>
        public string Color { get; }

        /// <summary>
        /// Gets the vehicle's brand.
        /// </summary>
        public string Brand { get; }

        /// <summary>
        /// Gets the items of the vehicle.
        /// </summary>
        public List<Item> Items { get; }
    }
}