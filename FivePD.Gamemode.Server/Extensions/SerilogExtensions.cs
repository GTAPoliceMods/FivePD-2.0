// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System;
using Serilog;
using Serilog.Configuration;

namespace FivePD.Gamemode.Server.Extensions
{
    /// <summary>
    /// Holds extension methods for <see cref="Serilog"/> classes.
    /// </summary>
    public static class SerilogExtensions
    {
        /// <summary>
        /// Writes log events to the Cfx console.
        /// </summary>
        /// <param name="loggerConfiguration">Logger sink configuration.</param>
        /// <param name="outputTemplate">A message template describing the format used to write to the sink.</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public static LoggerConfiguration CfxDebugSink(this LoggerSinkConfiguration loggerConfiguration, string outputTemplate, IFormatProvider formatProvider = null)
        {
            return loggerConfiguration.Sink(new CfxDebugSink(outputTemplate, formatProvider));
        }
    }
}