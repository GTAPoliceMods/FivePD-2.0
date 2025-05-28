// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System;
using System.Collections.Generic;
using System.Linq;
using FivePD.Gamemode.Server.Interfaces;
using FivePD.Gamemode.Server.Models.CommandModels;
using Cfx = CitizenFX.Core.Native;

namespace FivePD.Gamemode.Server.Services
{
    /// <inheritdoc />
    public class CommandService : ICommandService
    {
        private readonly List<Command> _commands = new List<Command>();

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandService"/> class.
        /// </summary>
        public CommandService()
        {
            Cfx.API.RegisterCommand("fivepd", new Action<int, List<object>, string>(this.OnCommand), false);
        }

        /// <inheritdoc />
        public Command RegisterCommand(string name, Action<int, List<object>> function, Func<bool> canInvoke = null)
        {
            var command = new Command(name, function, canInvoke);
            if (this._commands.All(key => key.Name != name))
            {
                this._commands.Add(command);
            }

            return command;
        }

        private void OnCommand(int source, List<object> args, string rawCommand)
        {
            if (args.Count == 0)
            {
                return;
            }

            string commandName = (string)args[0];
            string subcommandName = args.Count > 1 ? (string)args[1] : string.Empty;

            var command = this._commands.FirstOrDefault(command => command.Name == commandName);
            if (command is null)
            {
                return;
            }

            var shouldInvokeMainCommand = true;
            if (command.Subcommands.Count > 0 && !string.IsNullOrEmpty(subcommandName))
            {
                var subcommand = command.Subcommands.FirstOrDefault(subcommand => subcommand.Name == subcommandName);
                if (subcommand != null)
                {
                    shouldInvokeMainCommand = false;
                    args.RemoveRange(0, 2);
                    var canBeInvoked = subcommand.CanBeInvoked is null || subcommand.CanBeInvoked();
                    if (canBeInvoked)
                    {
                        subcommand.Function?.Invoke(source, args);
                    }
                }
            }

            if (shouldInvokeMainCommand)
            {
                args.RemoveAt(0);
                var canBeInvoked = command.CanBeInvoked is null || command.CanBeInvoked();
                if (canBeInvoked)
                {
                    command.Function?.Invoke(source, args);
                }
            }
        }
    }
}