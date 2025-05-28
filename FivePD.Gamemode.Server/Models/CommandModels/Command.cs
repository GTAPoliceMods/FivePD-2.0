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
    public class Command : BaseCommand
    {
        private readonly List<Subcommand> _subcommands = new List<Subcommand>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Command"/> class.
        /// </summary>
        /// <param name="name">Name of the command.</param>
        /// <param name="function">The method that'll be invoked upon entering the command. Arguments: source of sender, list of command arguments.</param>
        /// <param name="canBeInvoked">The command's function will only be invoked if this method returns true.</param>
        public Command(string name, Action<int, List<object>> function, Func<bool> canBeInvoked = null)
            : base(name, function, canBeInvoked)
        {
        }

        /// <summary>
        /// Gets the subcommands.
        /// </summary>
        public List<Subcommand> Subcommands => this._subcommands;

        /// <summary>
        /// Registers a subcommand under the command.
        /// </summary>
        /// <param name="name">Name of the subcommand.</param>
        /// <param name="function">The method that'll be invoked upon entering the command.</param>
        /// <param name="canBeInvoked">The command's function will only be invoked if this method returns true.</param>
        /// <returns>The parent command of the created subcommand.</returns>
        public Command AddSubcommand(string name, Action<int, List<object>> function, Func<bool> canBeInvoked = null)
        {
            this._subcommands.Add(new Subcommand(name, function, canBeInvoked));
            return this;
        }
    }
}