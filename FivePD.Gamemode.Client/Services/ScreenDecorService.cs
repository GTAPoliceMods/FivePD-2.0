// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using FivePD.Common.Builders;
using FivePD.Gamemode.Client.Interfaces;
using FivePD.Gamemode.Client.Utilities;
using Cfx = CitizenFX.Core.Native;

namespace FivePD.Gamemode.Client.Services
{
    /**
     * <inheritdoc />
     */
    public class ScreenDecorService : IScreenDecorService
    {
        private readonly ITickService _tickService;

        /**
         * <summary>Initializes a new instance of the <see cref="ScreenDecorService"/> class.</summary>
         * <param name="tickService">The <see cref="ITickService" /> to use.</param>
         */
        public ScreenDecorService(ITickService tickService)
        {
            this._tickService = tickService;
        }

        /**
         * <inheritdoc />
         */
        public string TextToDraw { get; set; } = string.Empty;

        /**
         * <inheritdoc />
         */
        public void BeginTextDraw()
        {
            this._tickService.OnTick += this.DrawOnScreen;
        }

        /**
         * <inheritdoc />
         */
        public void EndTextDraw()
        {
            this._tickService.OnTick -= this.DrawOnScreen;
        }

        /**
         * <inheritdoc />
         */
        public void SetTextToDraw(bool isLoading, bool hasError)
        {
            var formattedTextBuilder = new GtaStyleText()
                .White("First ")
                .Red("Response Multiplayer ")
                .White(" - v1.0.0 - ");

#if DEBUG
            formattedTextBuilder = formattedTextBuilder.White("Development Build");
#endif

            if (isLoading)
            {
                formattedTextBuilder.White(" - ").Yellow("Loading...");
            }

            if (hasError)
            {
                formattedTextBuilder.White(" - ").Red("An unknown error occured, contact the server owner.");
            }

            this.TextToDraw = formattedTextBuilder.Build();
        }

        private Task DrawOnScreen()
        {
            Cfx.API.SetTextFont(0);
            Cfx.API.SetTextProportional(false);
            Cfx.API.SetTextScale(0.0f, 0.3f);
            Cfx.API.SetTextColour(255, 255, 255, 255);
            Cfx.API.SetTextDropshadow(0, 0, 0, 0, 255);
            Cfx.API.SetTextEdge(1, 0, 0, 0, 255);
            Cfx.API.SetTextDropShadow();
            Cfx.API.SetTextOutline();
            Cfx.API.SetTextEntry("STRING");
            Cfx.API.AddTextComponentString(this.TextToDraw);
            Cfx.API.DrawText(0.005f, 0.005f);

            return Task.FromResult(0);
        }
    }
}