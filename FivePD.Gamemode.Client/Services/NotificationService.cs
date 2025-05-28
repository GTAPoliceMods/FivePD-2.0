// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using FivePD.Common.Builders;
using FivePD.Gamemode.Client.Interfaces;
using FivePD.Gamemode.Client.Utilities;
using Cfx = CitizenFX.Core.Native;
using CfxUI = CitizenFX.Core.UI;

namespace FivePD.Gamemode.Client.Services
{
    /// <inheritdoc />
    public class NotificationService : INotificationService
    {
        /// <inheritdoc />
        public void ShowTooltip(string content, bool withBeep = false)
        {
            Cfx.API.SetTextComponentFormat("STRING");
            Cfx.API.AddTextComponentString(content);
            Cfx.API.EndTextCommandDisplayHelp(0, false, withBeep, -1);
        }

        /// <inheritdoc />
        public void ShowTooltip(GtaStyleText content, bool withBeep = false)
        {
            this.ShowTooltip(content.Build(), withBeep);
        }

        /// <inheritdoc />
        public void ShowNotification(string content)
        {
            CfxUI.Screen.ShowNotification(content);
        }

        /// <inheritdoc />
        public void ShowNotification(GtaStyleText content)
        {
            this.ShowNotification(content.Build());
        }

        /// <inheritdoc />
        public void ShowNotificationWithTexture(string texture, string sender, string subject, string content)
        {
            Cfx.API.SetNotificationTextEntry("STRING");
            Cfx.API.SetNotificationColorNext(4);
            Cfx.API.AddTextComponentString(content);
            Cfx.API.SetTextScale(0.5f, 0.5f);
            Cfx.API.SetNotificationMessage(texture, texture, false, 0, sender, subject);
            Cfx.API.DrawNotification(true, false);
        }

        /// <inheritdoc />
        public void ShowNotificationWithTexture(string texture, string sender, string subject, GtaStyleText content)
        {
            this.ShowNotificationWithTexture(texture, sender, subject, content.Build());
        }
    }
}