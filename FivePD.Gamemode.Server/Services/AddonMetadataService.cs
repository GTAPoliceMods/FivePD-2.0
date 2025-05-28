// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System;
using System.Collections.Generic;
using System.Reflection;
using FivePD.Gamemode.Server.Interfaces;
using FivePD.Gamemode.Server.Models;

namespace FivePD.Gamemode.Server.Services
{
    /// <inheritdoc />
    public class AddonMetadataService : IAddonMetadataService
    {
        private readonly Dictionary<Assembly, AddonDefinition> _definitions = new Dictionary<Assembly, AddonDefinition>();

        /// <inheritdoc />
        public void RegisterAddonAssemblyWithDefinition(Assembly assembly, AddonDefinition definition)
        {
            this._definitions[assembly] = definition;
        }

        /// <inheritdoc />
        public AddonDefinition GetExecutingAddonDefinition()
        {
            // Technically, the executing assembly is the server gamemode. Sort through the stack trace and find a ref. to the actual addon
            foreach (var frame in new System.Diagnostics.StackTrace().GetFrames())
            {
                try
                {
                    if (frame.GetMethod().DeclaringType!.Assembly != Assembly.GetExecutingAssembly())
                    {
                        return this._definitions[frame.GetMethod().DeclaringType!.Assembly];
                    }
                }
                catch (Exception)
                {
                    // Continues when frame assembly is null
                }
            }

            return null;
        }
    }
}