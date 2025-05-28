// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System;
using System.Collections.Generic;

namespace FivePD.Gamemode.Server.Models.CommandModels
{
    /// <summary>
    /// Represents a "fivepd" command.
    /// </summary>
    public class BaseCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseCommand"/> class.
        /// </summary>
        /// <param name="name">Name of the command.</param>
        /// <param name="function">The method that'll be invoked upon entering the command. Arguments: source of sender, list of command arguments.</param>
        /// <param name="canBeInvoked">The command's function will only be invoked if this method returns true.</param>
        protected BaseCommand(string name, Action<int, List<object>> function, Func<bool> canBeInvoked = null)
        {
            this.Name = name;
            this.Function = function;
            this.CanBeInvoked = canBeInvoked;
        }

        /// <summary>
        /// Gets the name of the command.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the method which will be invoked upon entering the command.
        /// </summary>
        public Action<int, List<object>> Function { get; private set; }

        /// <summary>
        /// Gets the method which determines whether the command's function can be invoked or not.
        /// </summary>
        public Func<bool> CanBeInvoked { get; private set; }
    }
}