// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System;
using System.IO;
using CitizenFX.Core;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting.Display;

namespace FivePD.Gamemode.Server
{
    /// <summary>
    /// Custom <see cref="CitizenFX.Core.Debug.Write(string)"/> wrapper.
    /// </summary>
    public class CfxDebugSink : ILogEventSink
    {
        private readonly MessageTemplateTextFormatter _formatter;

        /// <summary>
        /// Initializes a new instance of the <see cref="CfxDebugSink"/> class.
        /// </summary>
        /// <param name="outputTemplate">A message template describing the format used to write to the sink.</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        public CfxDebugSink(string outputTemplate, IFormatProvider formatProvider)
        {
            this._formatter = new MessageTemplateTextFormatter(outputTemplate, formatProvider);
        }

        /// <inheritdoc/>
        public void Emit(LogEvent logEvent)
        {
            using var writer = new StringWriter();
            this._formatter.Format(logEvent, writer);
            var message = writer.ToString();
            Debug.Write(message);
        }
    }
}