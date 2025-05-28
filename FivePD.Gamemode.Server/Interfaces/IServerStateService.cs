// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

namespace FivePD.Gamemode.Server.Interfaces
{
    /**
     * <summary>Handles data which only needs to exist
     * on the server. Data stored here can generally
     * be considered "secure", as it is not persisted
     * to clients automatically, though it can be sent
     * to them via indirect means.</summary>
     */
    public interface IServerStateService
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
         * <summary>Gets the data associated with a given key, adding
         * it using a default value if it does not exist.</summary>
         * <param name="container">The state container to target.</param>
         * <param name="key">The specific key associated with the data.</param>
         * <param name="value">The value to add if the key does not exist.</param>
         * <typeparam name="T">The type of value to return.</typeparam>
         * <returns>Previously set data.</returns>
         */
        public T GetOrAdd<T>(string container, string key, object value);

        /**
         * <summary>Gets the data associated with a given key.</summary>
         * <param name="container">The state container to target.</param>
         * <param name="key">The specific key associated with the data.</param>
         * <param name="data">The data to store.</param>
         */
        public void Set(string container, string key, object data);
    }
}