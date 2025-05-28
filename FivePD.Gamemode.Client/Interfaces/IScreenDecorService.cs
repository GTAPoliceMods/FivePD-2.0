// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

namespace FivePD.Gamemode.Client.Interfaces
{
    /**
     * <summary>Provides utility methods for controlling
     * the content presented on user's screen passively.
     * (Used for the version banner, mainly).</summary>
     */
    public interface IScreenDecorService
    {
        /**
         * <summary>Gets or sets the text to draw on the screen.</summary>
         */
        public string TextToDraw { get; set; }

        /**
         * <summary>Begins drawing text on the screen.</summary>
         */
        public void BeginTextDraw();

        /**
         * <summary>Ends drawing text on the screen.</summary>
         */
        public void EndTextDraw();

        /// <summary>
        /// Sets the text to draw on the screen with formatting.
        /// </summary>
        /// <param name="isLoading">If set to true a loading text will be added to the string.</param>
        /// <param name="hasError">If set to true an error message will be added to the string.</param>
        public void SetTextToDraw(bool isLoading, bool hasError);
    }
}