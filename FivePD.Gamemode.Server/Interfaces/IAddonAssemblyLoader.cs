// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System.Reflection;
using System.Threading.Tasks;

namespace FivePD.Gamemode.Server.Interfaces
{
    /// <summary>
    /// Responsible for receiving assemblies and preparing them for
    /// invocation by other parts of the game mode.
    /// </summary>
    public interface IAddonAssemblyLoader
    {
        /// <summary>
        /// Initializes an addon assembly from bytes.
        /// </summary>
        /// <param name="assemblyBytes">The assembly to load.</param>
        /// <returns>The loaded assembly.</returns>
        public Assembly LoadAssemblyFromBytes(byte[] assemblyBytes);
    }
}