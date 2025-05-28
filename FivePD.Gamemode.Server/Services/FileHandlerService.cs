// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using FivePD.Common.Exceptions;
using FivePD.Gamemode.Server.Interfaces;
using Newtonsoft.Json;
using Cfx = CitizenFX.Core.Native;

namespace FivePD.Gamemode.Server.Services
{
    /// <inheritdoc />
    public class FileHandlerService : IFileHandlerService
    {
        private readonly ConcurrentDictionary<string, object> _cache = new ConcurrentDictionary<string, object>();

        /// <inheritdoc />
        public List<string> LoadStringList(string path)
        {
            string data = this.InternalLoadFile(path);

            if (this._cache.ContainsKey(path))
            {
                return (List<string>)this._cache[path];
            }

            if (string.IsNullOrEmpty(data))
            {
                return new List<string>();
            }

            var list = data.Split('\n').ToList();
            this._cache[path] = list;

            return list;
        }

        /// <inheritdoc />
        public Tuple<T, Exception> LoadFile<T>(string path, bool useCache = true)
        {
            if (this._cache.ContainsKey(path) && useCache)
            {
                return new Tuple<T, Exception>((T)this._cache[path], null);
            }

            Exception exception = null;
            string data = this.InternalLoadFile(path);

            if (string.IsNullOrEmpty(data))
            {
                data = "{}";
                exception = new EmptyConfigException();
            }

            try
            {
                var deserializedObject = JsonConvert.DeserializeObject<T>(data);
                if (useCache)
                {
                    this._cache[path] = deserializedObject;
                }

                return new Tuple<T, Exception>(deserializedObject, exception);
            }
            catch (Exception e)
            {
                exception = e;
            }

            return new Tuple<T, Exception>(Activator.CreateInstance<T>(), exception);
        }

        /// <summary>
        /// Loads a file's content then returns it without validation.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <returns>The content of the file.</returns>
        private string InternalLoadFile(string path)
        {
            return Cfx.API.LoadResourceFile(Cfx.API.GetCurrentResourceName(),  path);
        }
    }
}