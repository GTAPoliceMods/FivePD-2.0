// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System;
using System.Collections.Generic;

namespace FivePD.Gamemode.Client.Injection
{
    /**
     * <summary>Represents a registration for a <see cref="ServiceProvider"/>.</summary>
     */
    public abstract class Registration
    {
        /**
         * <summary>Gets or sets the <see cref="Type" /> that this registration should represent.</summary>
         */
        public Type InstanceType { get; set; }

        /**
         * <summary>Gets the <see cref="List{Type}" /> that this registration represents.</summary>
         */
        public List<Type> Types { get; } = new List<Type>();

        /**
         * <summary>Gets or sets a value indicating whether or not this is a singleton registration, which should only be instantiated once.</summary>
         */
        public bool IsSingleton { get; set; }

        /**
         * <summary>Gets or sets a <see cref="Factory" /> that this registration uses.</summary>
         */
        public Func<object> Factory { get; set; }
    }

    #pragma warning disable SA1402
    /**
     * <summary>Represents a registration for a <see cref="ServiceProvider"/> of type <typeparamref name="T"/>.</summary>
     * <typeparam name="T">The type to use.</typeparam>
     */
    public class Registration<T> : Registration
    {
        /**
         * <summary>Initializes a new instance of the <see cref="Registration{T}"/> class.</summary>
         */
        public Registration()
        {
            this.InstanceType = typeof(T);
            this.Types.Add(this.InstanceType);
        }
    }
    #pragma warning restore SA1402
}