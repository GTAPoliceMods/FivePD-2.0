// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

namespace FivePD.Common
{
    /// <summary>
    /// Contains the locale (file's name) and the title of the available localizations.
    /// </summary>
    public class Locale
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Locale"/> class.
        /// </summary>
        /// <param name="key">Identifier of this locale.</param>
        /// <param name="title">Name of this locale.</param>
        public Locale(string key, string title)
        {
            this.Key = key;
            this.Title = title;
        }

        /// <summary>
        /// Gets the identifier of this locale (localization file's name).
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// Gets or sets name of the localization that'll be displayed in the menu.
        /// </summary>
        public string Title { get; set; }
    }
}