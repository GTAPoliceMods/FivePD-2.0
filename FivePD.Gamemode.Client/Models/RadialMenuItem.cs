// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System;
using CitizenFX.Core;

namespace FivePD.Gamemode.Client.Models
{
    /// <summary>
    /// A radial menu item.
    /// </summary>
    public class RadialMenuItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RadialMenuItem"/> class.
        /// </summary>
        /// <param name="title">The title of this item.</param>
        /// <param name="onPress">The method that'll be invoked when the user clicks on this item.</param>
        public RadialMenuItem(string title, Action<CallbackDelegate> onPress)
        {
            this.Title = title;
            this.OnPress = onPress;
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets asd.
        /// </summary>
        public Action<CallbackDelegate> OnPress { get; set; }
    }
}