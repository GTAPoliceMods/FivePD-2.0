// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using FivePD.Gamemode.Client.Injection;

namespace FivePD.Gamemode.Client.Extensions
{
    /**
     * <summary>Holds extension methods for <see cref="Registration"/>.</summary>
     */
    public static class RegistrationExtensions
    {
        /**
         * <summary>Marks a registration as a singleton. Marked classes will likely only be instantiated once.</summary>
         * <param name="registration">The registration to modify.</param>
         * <returns>The updated registration.</returns>
         */
        public static Registration IsSingleton(this Registration registration)
        {
            registration.IsSingleton = true;

            return registration;
        }

        /**
         * <summary>Defines an implementation (<typeparamref name="T"/>) for a <paramref name="registration"/>.</summary>
         * <typeparam name="T">The type of the implementation to use.</typeparam>
         * <param name="registration">The registration to modify.</param>
         * <returns>The updated registration.</returns>
         */
        public static Registration As<T>(this Registration registration)
        {
            var type = typeof(T);
            if (!registration.Types.Contains(type))
            {
                registration.Types.Add(type);
            }

            return registration;
        }
    }
}