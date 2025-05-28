// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System;
using System.Reflection;
using CitizenFX.Core;
using FivePD.Gamemode.Server.Interfaces;
using Cfx = CitizenFX.Core.Native;

namespace FivePD.Gamemode.Server.Services
{
    /**
     * <inheritdoc />
     **/
    public class ReplicatedStateService : IReplicatedStateService
    {
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
        public void Set(string container, string key, object data)
        {
            CreateInstance<StateBag>(container).Set(key, data, true);
        }

        #pragma warning disable S3011
        /**
         * This state service is powered by Cfx's statebag system.
         * Right now, methods for controlling statebags are protected
         * via accessibility properties and are not available to
         * other assemblies. We're using reflection to access them
         * directly. This is imperfect, but its usage is very limited
         * and should be acceptable for the time being. This method
         * is a wrapper allowing us easily construct instances from
         * classes without a public constructor. Pretty cool, right?
         */
        private static T CreateInstance<T>(params object[] args)
        {
            var type = typeof(T);
            if (type.FullName == null)
            {
                throw new ArgumentException("Invalid type");
            }

            var instance = type.Assembly.CreateInstance(
                type.FullName,
                false,
                BindingFlags.Instance | BindingFlags.NonPublic,
                null,
                args,
                null,
                null);

            return (T)instance;
        }
        #pragma warning restore S3011

        private dynamic Get(string container, string key)
        {
            return CreateInstance<StateBag>(container).Get(key);
        }
    }
}