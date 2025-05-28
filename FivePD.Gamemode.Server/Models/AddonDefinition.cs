// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System.Collections.Generic;
using System.Reflection;

namespace FivePD.Gamemode.Server.Models
{
    /**
     * <summary>The base model representing a gamemode extension.</summary>
     */
    public class AddonDefinition
    {
        /**
         * <summary>Gets or sets the technical name for the addon. Should be PascalCase.</summary>
         */
        public string Name { get; set; }

        /**
         * <summary>Gets or sets the friendly name for the addon. Should be Title Case.</summary>
         */
        public string FriendlyName { get; set; }

        /**
         * <summary>Gets or sets the machine-readable version for the addon.
         * A higher value indicates a newer version.</summary>
         */
        public int Version { get; set; }

        /**
         * <summary>Gets or sets the friendly version for the addon.</summary>
         */
        public string FriendlyVersion { get; set; }

        /**
         * <summary>Gets or sets a value indicating whether the addon is internal.
         * Internal addons are prioritized.</summary>
         */
        public bool IsInternal { get; set; }

        /**
         * <summary>Gets or sets the assemblies that the addon requires.</summary>
         * <returns>An enumerable of all assemblies which must be loaded.</returns>
         */
        public IList<byte[]> Assemblies { get; set; }
    }
}