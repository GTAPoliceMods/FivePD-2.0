// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

namespace FivePD.Gamemode.Server.Interfaces
{
    /**
     * <summary>Handles data which should be replicated
     * automatically across both clients and servers.
     * Any data stored via this state service should
     * not be sensitive to reads by clients, and may
     * be exposed at any time.</summary>
     */
    public interface IReplicatedStateService
    {
        /**
         * <summary>Gets the data associated with a given key.</summary>
         * <param name="container">The state container to target.</param>
         * <param name="key">The specific key associated with the data.</param>
         * <typeparam name="T">The type of value to return.</typeparam>
         * <returns>Previously set data.</returns>
         */
        public T Get<T>(string container, string key);

        /**
         * <summary>Gets the data associated with a given key.</summary>
         * <param name="container">The state container to target.</param>
         * <param name="key">The specific key associated with the data.</param>
         * <param name="data">The data to store.</param>
         */
        public void Set(string container, string key, object data);
    }
}