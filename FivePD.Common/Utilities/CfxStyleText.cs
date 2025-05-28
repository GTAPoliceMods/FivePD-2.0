// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System.Text;

#pragma warning disable CS1591
#pragma warning disable SA1602
#pragma warning disable SA1611
#pragma warning disable SA1615

namespace FivePD.Common.Utilities
{
    /// <summary>
    /// A string builder wrapper to easily create multicolored texts.
    /// </summary>
    public class CfxStyleText
    {
        private readonly StringBuilder _stringBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="CfxStyleText"/> class.
        /// </summary>
        public CfxStyleText()
        {
            this._stringBuilder = new StringBuilder();
        }

        /// <summary>
        /// Sets the text color to red.
        /// </summary>
        public CfxStyleText Red(string text = "")
        {
            return this.Append(Formats.Red, text);
        }

        /// <summary>
        /// Sets the text color to green.
        /// </summary>
        public CfxStyleText Green(string text = "")
        {
            return this.Append(Formats.Green, text);
        }

        /// <summary>
        /// Sets the text color to blue.
        /// </summary>
        public CfxStyleText Blue(string text = "")
        {
            return this.Append(Formats.Blue, text);
        }

        /// <summary>
        /// Sets the text color to dark blue.
        /// </summary>
        public CfxStyleText DarkBlue(string text = "")
        {
            return this.Append(Formats.DarkBlue, text);
        }

        /// <summary>
        /// Sets the text color to yellow.
        /// </summary>
        public CfxStyleText Yellow(string text = "")
        {
            return this.Append(Formats.Yellow, text);
        }

        /// <summary>
        /// Sets the text color to purple.
        /// </summary>
        public CfxStyleText Purple(string text = "")
        {
            return this.Append(Formats.Purple, text);
        }

        /// <summary>
        /// Sets the text color to white.
        /// </summary>
        public CfxStyleText White(string text = "")
        {
            return this.Append(Formats.White, text);
        }

        /// <summary>
        /// Returns the created text.
        /// </summary>
        public string Build()
        {
            return this._stringBuilder.ToString();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.Build();
        }

        private CfxStyleText Append(string color, string text)
        {
            this._stringBuilder.Append(color);
            if (!string.IsNullOrEmpty(text))
            {
                this._stringBuilder.Append(text);
            }

            return this;
        }

        private abstract class Formats
        {
            public const string White = "^0";
            public const string Red = "^1";
            public const string Green = "^2";
            public const string Yellow = "^3";
            public const string DarkBlue = "^4";
            public const string Blue = "^5";
            public const string Purple = "^6";
        }
    }
}