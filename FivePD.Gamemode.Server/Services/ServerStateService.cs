// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System.Collections.Concurrent;
using FivePD.Gamemode.Server.Interfaces;

namespace FivePD.Gamemode.Server.Services
{
    /**
     * <inheritdoc />
     */
    public class ServerStateService : IServerStateService
    {
        private readonly ConcurrentDictionary<string, ConcurrentDictionary<string, dynamic>> _state = new ConcurrentDictionary<string, ConcurrentDictionary<string, dynamic>>();

        /**
         * <inheritdoc />
         */
        public T Get<T>(string container, string key)
        {
            return (T)this.Get(container, key);
        }

        /**
         * <inheritdoc />
         */
        public T GetOrAdd<T>(string container, string key, object value)
        {
            this._state.TryAdd(container, new ConcurrentDictionary<string, dynamic>());

            return (T)this._state[container].GetOrAdd(key, value);
        }

        /**
         * <inheritdoc />
         */
        public void Set(string container, string key, object data)
        {
            this._state.TryAdd(container, new ConcurrentDictionary<string, dynamic>());

            this._state[container][key] = data;
        }

        private dynamic Get(string container, string key)
        {
            return this._state[container][key];
        }
    }
}