// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System.Reflection;
using FivePD.Gamemode.Server.Models;

namespace FivePD.Gamemode.Server.Interfaces
{
    /// <summary>
    /// Provides easy access to information about addons.
    /// </summary>
    public interface IAddonMetadataService
    {
        /// <summary>
        /// Provide information to the service regarding an assembly and its definition.
        /// </summary>
        /// <param name="assembly">The assembly to register.</param>
        /// <param name="definition">The definition to register.</param>
        public void RegisterAddonAssemblyWithDefinition(Assembly assembly, AddonDefinition definition);

        /// <summary>
        /// Gets information about the executing addon.
        /// </summary>
        /// <returns>An <see cref="AddonDefinition"/> for the currently executing assembly.</returns>
        public AddonDefinition GetExecutingAddonDefinition();
    }
}