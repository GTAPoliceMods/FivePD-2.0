// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System.Collections.Generic;

namespace FivePD.Gamemode.Client.Models
{
    /// <summary>
    /// A radial menu.
    /// </summary>
    public class RadialMenu
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RadialMenu"/> class.
        /// </summary>
        /// <param name="id">The unique id of this menu.</param>
        /// <param name="title">The name of this menu, just as an identifier.</param>
        /// <param name="key">The keybind of this menu.</param>
        /// <param name="items">The items of this menu.</param>
        public RadialMenu(string id, string title, string key, List<RadialMenuItem> items)
        {
            this.Id = id;
            this.Title = title;
            this.Key = key;
            this.Items = items;
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets asd.
        /// </summary>
        public List<RadialMenuItem> Items { get; }
    }
}