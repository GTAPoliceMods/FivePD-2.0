// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using CitizenFX.Core;
using FivePD.Common.Models;
using FivePD.Gamemode.Client.Interfaces;

namespace FivePD.Gamemode.Client.Services
{
    /// <inheritdoc />
    public class PoliceEventService : IPoliceEventService
    {
        private BasePoliceEvent _currentlyAttachedEvent;

        /// <inheritdoc />
        public bool IsAttachedToEvent()
        {
            return !(this._currentlyAttachedEvent is null);
        }

        /// <inheritdoc />
        public bool IsEventMainPlayerEqualsToPlayer()
        {
            if (!this.IsAttachedToEvent())
            {
                return false;
            }

            return this._currentlyAttachedEvent.MainPlayer == Game.PlayerPed.NetworkId;
        }

        /// <inheritdoc />
        public BasePoliceEvent GetCurrentEvent()
        {
            return this._currentlyAttachedEvent;
        }

        /// <inheritdoc />
        public void SetCurrentEvent(BasePoliceEvent policeEvent)
        {
            this._currentlyAttachedEvent = policeEvent;
        }
    }
}