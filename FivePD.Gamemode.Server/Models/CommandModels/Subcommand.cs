// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System;
using System.Collections.Generic;

namespace FivePD.Gamemode.Server.Models.CommandModels
{
    /// <inheritdoc />
    public class Subcommand : BaseCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Subcommand"/> class.
        /// </summary>
        /// <param name="name">Name of the command.</param>
        /// <param name="function">The method that'll be invoked upon entering the command. Arguments: source of sender, list of command arguments.</param>
        /// <param name="canBeInvoked">The command's function will only be invoked if this method returns true.</param>
        public Subcommand(string name, Action<int, List<object>> function, Func<bool> canBeInvoked = null)
            : base(name, function, canBeInvoked)
        {
        }
    }
}