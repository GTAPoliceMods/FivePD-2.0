// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System;
using System.Collections.Generic;
using System.Linq;

namespace FivePD.Gamemode.Client.Injection
{
    /**
     * <summary>A simple builder for <see cref="ServiceProvider"/>.</summary>
     */
    public class ServiceProviderBuilder
    {
        private readonly List<Registration> _registration = new List<Registration>();

        /**
         * <summary>Registers type <typeparamref name="T"/> to this <see cref="ServiceProviderBuilder"/>.</summary>
         * <typeparam name="T">The type to register.</typeparam>
         * <returns>A created registration for the provided type.</returns>
         */
        public Registration<T> RegisterType<T>()
        {
            var type = typeof(T);
            var registration = new Registration<T>();

            if (this._registration.Any(r => r.Types.Any(t => t == type)))
            {
                throw new ArgumentException($"Type {type.AssemblyQualifiedName} already registered.");
            }

            this._registration.Add(registration);

            return registration;
        }

        /**
         * <summary>Registers type <see cref="Func{T}"/> to this <see cref="ServiceProviderBuilder"/> through a <typeparamref name="T"/>.</summary>
         * <typeparam name="T">The type to register.</typeparam>
         * <param name="factory">The factory to use in creation.</param>
         * <returns>A created registration for the provided type.</returns>
         */
        public Registration<T> Register<T>(Func<T> factory)
        {
            var type = typeof(T);
            var registration = new Registration<T>
            {
                Factory = () => factory(),
            };

            if (this._registration.Any(r => r.Types.Any(t => t == type)))
            {
                throw new ArgumentException($"Type {type.AssemblyQualifiedName} already registered.");
            }

            this._registration.Add(registration);

            return registration;
        }

        /**
         * <summary>Creates a new <see cref="ServiceProvider"/> from this <see cref="ServiceProviderBuilder" />.</summary>
         * <returns>The built <see cref="ServiceProvider"/>.</returns>
         */
        public ServiceProvider Build()
        {
            return new ServiceProvider(this._registration);
        }
    }
}