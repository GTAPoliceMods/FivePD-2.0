// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

namespace FivePD.Gamemode.Server.API
{
    /**
     * <summary>Defines basic addon metadata that shouldn't
     * be stored in the addon definition file.</summary>
     */
    public class AddonMetadataAttribute : System.Attribute
    {
        /**
        * <summary>Initializes a new instance of the <see cref="AddonMetadataAttribute"/> class.</summary>
        * <param name="name">The <see cref="Name"/> to use.</param>
        * <param name="friendlyVersion">The <see cref="FriendlyVersion"/> to use.</param>
        * <param name="version">The <see cref="Version"/> to use.</param>
        */
        public AddonMetadataAttribute(string name, string friendlyVersion, int version)
        {
         this.Name = name;
         this.FriendlyVersion = friendlyVersion;
         this.Version = version;
        }

        /**
         * <summary>Gets or sets the technical name for the addon. Should be PascalCase.</summary>
         */
        public string Name { get; set; }

        /**
         * <summary>Gets or sets the friendly name for the addon. This is shown to users.</summary>
         */
        public string FriendlyVersion { get; set; }

        /**
         * <summary>Gets or sets the technical version for the addon. Higher values are newer.</summary>
         */
        public int Version { get; set; }
    }
}