// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using FivePD.Gamemode.Client.Interfaces;

namespace FivePD.Gamemode.Client.Services
{
    /**
     * <inheritdoc />
     */
    public class TickService : ITickService
    {
        private readonly AddTickHandler _addTickHandler;

        private readonly RemoveTickHandler _removeTickHandler;

        /**
         * <summary>Initializes a new instance of the <see cref="TickService"/> class.</summary>
         * <param name="addTickHandler">The method to use when adding a function to be called each tick.</param>
         * <param name="removeTickHandler">The method to use when removing a function.</param>
         */
        public TickService(AddTickHandler addTickHandler, RemoveTickHandler removeTickHandler)
        {
            this._addTickHandler = addTickHandler;
            this._removeTickHandler = removeTickHandler;
        }

        /**
         * <inheritdoc />
         */
        public event Func<Task> OnTick
        {
            add => this._addTickHandler(value);

            remove => this._removeTickHandler(value);
        }
    }
}