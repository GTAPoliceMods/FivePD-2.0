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
     * <summary>The base ped class for the
     * gamemode. Provides a wrapper around game
     * functionality for easier interactions
     * with the framework.</summary>
     */
    public class FPed
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FPed"/> class.
        /// </summary>
        /// <param name="builder">The builder object that'll be used to initialize this instance.</param>
        public FPed(FPedBuilder builder)
        {
            this.NetworkId = builder.NetworkId;
            this.Gender = builder.Gender;
            this.Firstname = builder.Firstname;
            this.Lastname = builder.Lastname;
            this.Birthdate = builder.Birthdate;
            this.Items = builder.Items;
        }

        /// <summary>
        /// Gets the ped's network identifier.
        /// </summary>
        public int NetworkId { get; }

        /// <summary>
        /// Gets the ped's gender.
        /// </summary>
        public FGender Gender { get; }

        /// <summary>
        /// Gets the ped's firstname.
        /// </summary>
        public string Firstname { get; }

        /// <summary>
        /// Gets the ped's lastname.
        /// </summary>
        public string Lastname { get; }

        /// <summary>
        /// Gets the ped's birthdate.
        /// </summary>
        public Date Birthdate { get; }

        /// <summary>
        /// Gets the items of the ped.
        /// </summary>
        public List<Item> Items { get; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"Ped - network id: {this.NetworkId}, name: {this.Firstname} {this.Lastname}, birthdate: {this.Birthdate}";
        }
    }
}