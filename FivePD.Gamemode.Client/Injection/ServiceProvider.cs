// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FivePD.Gamemode.Client.Injection
{
    /**
     * <summary>Provides an interface to resolve registered services. Built using an <see cref="ServiceProviderBuilder"/>.</summary>
     */
    public class ServiceProvider
    {
        private readonly Dictionary<string, object> _instances = new Dictionary<string, object>();
        private readonly List<Registration> _registrations;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceProvider"/> class.
        /// </summary>
        /// <param name="registrations">The registrations to use in creation.</param>
        public ServiceProvider(List<Registration> registrations)
        {
            this._registrations = registrations;
        }

        /**
         * <summary>Resolves a matching instance of the given <paramref name="type"/>.</summary>
         * <param name="type">The type to search for and return.</param>
         * <returns>A constructed instance of the given <paramref name="type"/>.</returns>
         */
        public object Resolve(Type type)
        {
            var matchingRegistration = this._registrations.SingleOrDefault(r => r.Types.Any(t => t == type));

            if (matchingRegistration == null)
            {
                throw new ArgumentException("Could not resolve type " + type.AssemblyQualifiedName);
            }

            if (matchingRegistration.IsSingleton)
            {
                return this.ResolveSingleInstance(matchingRegistration);
            }

            return this.CreateInstance(matchingRegistration);
        }

        /**
         * <summary>Resolves a matching instance of the given <typeparamref name="T"/>.</summary>
         * <typeparam name="T">The type to search for and return.</typeparam>
         * <returns>A constructed instance of the given <typeparamref name="T"/>.</returns>
         */
        public T Resolve<T>()
        {
            var resolveType = typeof(T);
            return (T)this.Resolve(resolveType);
        }

        private object ResolveSingleInstance(Registration registration)
        {
            var assemblyQualifiedName = registration.InstanceType.AssemblyQualifiedName;

            if (this._instances.ContainsKey(assemblyQualifiedName ?? throw new InvalidOperationException()))
            {
                return this._instances[assemblyQualifiedName];
            }

            var instance = this.CreateInstance(registration);
            this._instances.Add(assemblyQualifiedName, instance);

            return instance;
        }

        private object CreateInstance(Registration registration)
        {
            if (registration.Factory != null)
            {
                return registration.Factory();
            }

            var constructor = registration.InstanceType.GetConstructors().FirstOrDefault();
            var parameters = constructor?.GetParameters().Select(this.ResolveConstructorParameter).ToArray();

            var instance = Activator.CreateInstance(registration.InstanceType, parameters);

            return instance;
        }

        private object ResolveConstructorParameter(ParameterInfo parameterInfo)
        {
            return this.Resolve(parameterInfo.ParameterType);
        }
    }
}