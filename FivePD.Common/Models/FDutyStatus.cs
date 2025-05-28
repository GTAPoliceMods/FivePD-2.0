// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

namespace FivePD.Common.Models
{
    /**
     * <summary>An enum representing a player's duty
     * status within the gamemode (i.e. if they are
     * acting as a user of the gamemode or not).</summary>
     */
    public enum FDutyStatus
    {
        /**
         * <summary>The player is not on duty
         * and should not be interacted with.
         * </summary>
         */
        OffDuty,
        /**
         * <summary>The player is on duty but is
         * not currently receiving interactions.</summary>
         */
        OnDutyAndNotReceiving,
        /**
         * <summary>The player is on duty and
         * is receiving interactions.</summary>
         */
        OnDutyAndReceiving,
    }
}