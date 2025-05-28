// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

#pragma warning disable S3264
#pragma warning disable CS0067

namespace FivePD.Gamemode.Client.Models
{
    /// <inheritdoc />
    public class MenuCheckboxItem : MenuItem
    {
        private bool _checked;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuCheckboxItem"/> class.
        /// </summary>
        /// <param name="title">The title of this item.</param>
        /// <param name="description">The description of this item.</param>
        public MenuCheckboxItem(string title, string description)
            : base(title, description, MenuItemType.Toggle)
        {
        }

        /// <summary>
        /// Fires when the item has been selected.
        /// </summary>
        public event ItemSelect OnItemSelect;

        /// <summary>
        /// Gets or sets a value indicating whether this checkbox is checked.
        /// </summary>
        public bool Checked
        {
            get => this._checked;
            set
            {
                this._checked = value;
                this.UpdateItem("checked", this._checked);
            }
        }

        private void RaiseOnItemSelect()
        {
            this._checked = !this._checked;
            this.OnItemSelect?.Invoke();
        }
    }
}