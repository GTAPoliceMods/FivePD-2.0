// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using FivePD.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Cfx = CitizenFX.Core.Native;

#pragma warning disable S3264
#pragma warning disable CS0067

namespace FivePD.Gamemode.Client.Models
{
    /// <summary>
    /// Represents a normal menu item in the Nui.
    /// </summary>
    public abstract class MenuItem
    {
        private string _title;
        private bool _enabled;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItem"/> class.
        /// </summary>
        /// <param name="title">The title of this item.</param>
        /// <param name="description">The description of this item.</param>
        /// <param name="type">The type of this item.</param>
        protected MenuItem(string title, string description, MenuItemType type)
        {
            this._title = title;
            this._enabled = true;
            this.Description = description;
            this.Type = type;
        }

        /// <summary>
        /// Used for firing the item selection event.
        /// </summary>
        public delegate void ItemSelect();

        /// <summary>
        /// The type of the item.
        /// </summary>
        public enum MenuItemType
        {
            /// <summary>
            /// Will only trigger an action on press.
            /// </summary>
            Button,

            /// <summary>
            /// Will display a select from which the user can select a value.
            /// </summary>
            List,

            /// <summary>
            /// Will display a checkbox.
            /// </summary>
            Toggle,
        }

        /// <summary>
        /// Gets or sets the title the item.
        /// </summary>
        public string Title
        {
            get => this._title;
            set
            {
                this._title = value;
                this.UpdateItem("title", this._title);
            }
        }

        /// <summary>
        /// Gets the description.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Gets or sets a value indicating whether the item is clickable.
        /// </summary>
        public bool Enabled
        {
            get => this._enabled;
            set
            {
                this._enabled = value;
                this.UpdateItem("enabled", this._enabled);
            }
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        public MenuItemType Type { get; }

        /// <summary>
        /// Updates a menu item's given field with the passed value.
        /// </summary>
        /// <typeparam name="T">The type of the new value.</typeparam>
        /// <param name="fieldname">The field's name to update.</param>
        /// <param name="value">The value which will be set to the given field.</param>
        protected void UpdateItem<T>(string fieldname, T value)
        {
            Cfx.API.SendNuiMessage(JsonConvert.SerializeObject(new JObject
            {
                { "type", Events.NuiEventType.UpdateMenuItem },
                { "hashcode", this.GetHashCode().ToString() },
                { "fieldname", fieldname },
                { "value", JToken.FromObject(value) },
            }));
        }
    }
}