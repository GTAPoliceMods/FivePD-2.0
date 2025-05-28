// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System;
using System.Collections.Generic;

namespace FivePD.Gamemode.Server.Interfaces
{
    /// <summary>
    /// Provides utility methods for file handling.
    /// </summary>
    public interface IFileHandlerService
    {
        /// <summary>
        /// Loads a file's content, splits it by new lines and creates a list from them.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <returns>A string list, containing each line of the file.</returns>
        public List<string> LoadStringList(string path);

        /// <summary>
        /// Loads a file's content.
        /// If empty, returns T with its default values.
        /// Otherwise returns the data converted to the given type.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the content.</typeparam>
        /// <param name="path">The path of the file.</param>
        /// <param name="useCache">Determines if the data should be cached.</param>
        /// <returns>The data converted to T.</returns>
        public Tuple<T, Exception> LoadFile<T>(string path, bool useCache = true);
    }
}