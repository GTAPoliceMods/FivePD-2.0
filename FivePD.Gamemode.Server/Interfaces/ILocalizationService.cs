// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using FivePD.Common.Localization;

namespace FivePD.Gamemode.Server.Interfaces
{
    /// <summary>
    /// Provides utility methods to handle translation files.
    /// </summary>
    public interface ILocalizationService
    {
        /// <summary>
        /// Gets the default english localization.
        /// </summary>
        /// <returns>The default english localization.</returns>
        public Localization GetDefaultLocalization();

        /// <summary>
        /// Registers all necessary NUI events for the MDT and menus.
        /// </summary>
        public void RegisterEventHandlers();

        /// <summary>
        /// Finds and loads all translation files located in fivepd/Localization.
        /// </summary>
        public void LoadAvailableLocales();

        /// <summary>
        /// Validates all localization files, both with the Json and the Localization schema.
        /// </summary>
        public void ValidateLocalizationFiles();
    }
}