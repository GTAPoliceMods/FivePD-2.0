// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System.Collections.Generic;
using FivePD.Common.ConfigModels;

#pragma warning disable SA1618

namespace FivePD.Gamemode.Server.Interfaces
{
    /// <summary>
    /// Provides utility methods for the FivePD resource itself.
    /// </summary>
    public interface IConfigService
    {
        /// <summary>
        /// Loads and returns the database connection settings.
        /// </summary>
        /// <returns>Returns the database connection settings.</returns>
        public DatabaseConfig GetDatabaseConfig();

        /// <summary>
        /// Loads and returns the /Config/items.json file's content.
        /// </summary>
        /// <returns>Returns all items from the config file.</returns>
        public List<Item> GetItems();

        /// <summary>
        /// Loads the /Config/FemaleFirstnames.txt and returns a list from its content.
        /// </summary>
        /// <returns>A list containing each line of the file.</returns>
        public List<string> GetFemaleFirstnames();

        /// <summary>
        /// Loads the /Config/MaleFirstnames.txt and returns a list from its content.
        /// </summary>
        /// <returns>A list containing each line of the file.</returns>
        public List<string> GetMaleFirstnames();

        /// <summary>
        /// Loads the /Config/Lastnames.txt and returns a list from its content.
        /// </summary>
        /// <returns>A list containing each line of the file.</returns>
        public List<string> GetLastnames();
    }
}