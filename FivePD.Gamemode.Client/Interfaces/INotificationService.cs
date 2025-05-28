// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using FivePD.Common.Builders;
using FivePD.Gamemode.Client.Utilities;

namespace FivePD.Gamemode.Client.Interfaces
{
    /// <summary>
    /// Responsible for displaying base GTA notifications.
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// Displays a tooltip (small notification in the top-left corner).
        /// </summary>
        /// <param name="content">The text that'll be displayed.</param>
        /// <param name="withBeep">If the notification should play a beep sound.</param>
        public void ShowTooltip(string content, bool withBeep = false);

        /// <summary>
        /// Displays a tooltip (small notification in the top-left corner).
        /// </summary>
        /// <param name="content">A <see cref="GtaStyleText"/> object to display a formatted text.</param>
        /// <param name="withBeep">If the notification should play a beep sound.</param>
        public void ShowTooltip(GtaStyleText content, bool withBeep = false);

        /// <summary>
        /// Displays a notification.
        /// </summary>
        /// <param name="content">The text that'll be displayed.</param>
        public void ShowNotification(string content);

        /// <summary>
        /// Displays a notification.
        /// </summary>
        /// <param name="content">A <see cref="GtaStyleText"/> object to display a formatted text.</param>
        public void ShowNotification(GtaStyleText content);

        /// <summary>
        /// Displays a notification with a texture.
        /// </summary>
        /// <param name="texture">The texture's name which is the top-left image in the notification.</param>
        /// <param name="sender">The sender of the notification, will be displayed on the first line.</param>
        /// <param name="subject">The subject of the notification, will be displayed on the second line.</param>
        /// <param name="content">The text that'll be displayed.</param>
        public void ShowNotificationWithTexture(string texture, string sender, string subject, string content);

        /// <summary>
        /// Displays a notification with a texture.
        /// </summary>
        /// <param name="texture">The texture's name which is the top-left image in the notification.</param>
        /// <param name="sender">The sender of the notification, will be displayed on the first line.</param>
        /// <param name="subject">The subject of the notification, will be displayed on the second line.</param>
        /// <param name="content">A <see cref="GtaStyleText"/> object to display a formatted text.</param>
        public void ShowNotificationWithTexture(string texture, string sender, string subject, GtaStyleText content);
    }
}