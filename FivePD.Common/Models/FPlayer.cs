// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

namespace FivePD.Common.Models
{
    /**
     * <summary>The base player class for the
     * gamemode. Provides a wrapper around game
     * functionality for easier interactions
     * with the framework.</summary>
     */
    public class FPlayer
    {
        /**
         * <summary>Initializes a new instance of the <see cref="FPlayer"/> class.</summary>
         * <param name="netId">The network identifier that corresponds to the player.</param>
         */
        public FPlayer(string netId)
        {
            this.NetId = netId;
        }

        /**
         * <summary>Gets the player's network identifier.</summary>
         */
        public string NetId { get; private set; }

        /**
         * <inheritdoc />
         */
        public override string ToString()
        {
            return this.NetId;
        }
    }
}