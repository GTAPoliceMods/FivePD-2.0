// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using FivePD.Common;
using FivePD.Common.Extensions;
using FivePD.Gamemode.Client.Interfaces;
using FivePD.Gamemode.Client.Utilities;
using Newtonsoft.Json.Linq;

namespace FivePD.Gamemode.Client.Services
{
    /// <inheritdoc />
    public class SoundService : ISoundService
    {
        private readonly INuiService _nuiService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SoundService"/> class.
        /// </summary>
        /// <param name="nuiService">The <see cref="INuiService"/> to use.</param>
        public SoundService(INuiService nuiService)
        {
            this._nuiService = nuiService;
        }

        /// <inheritdoc />
        public void Play(Sound sound)
        {
            this._nuiService.SendMessage(new JObject
            {
                { "type", Events.NuiEventType.PlaySound },
                { "sound", sound.GetDescription() },
            });
        }

        /// <inheritdoc />
        public void Speak(string text)
        {
            this._nuiService.SendMessage(new JObject
            {
                { "type", Events.NuiEventType.Speak },
                { "textToPlay", text },
            });
        }
    }
}