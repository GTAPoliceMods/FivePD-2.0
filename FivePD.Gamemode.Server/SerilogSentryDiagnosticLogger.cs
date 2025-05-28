// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.
#if DEBUG
using System;
using Sentry;
using Sentry.Extensibility;
using Serilog.Events;

namespace FivePD.Gamemode.Server
{
    /// <summary>
    /// <see cref="IDiagnosticLogger"/> that uses Serilog to output information.
    /// </summary>
    public class SerilogSentryDiagnosticLogger : IDiagnosticLogger
    {
        /// <inheritdoc />
        public bool IsEnabled(SentryLevel level)
        {
            return Serilog.Log.Logger.IsEnabled(LogEventLevel.Verbose);
        }

        /// <inheritdoc />
        public void Log(SentryLevel logLevel, string message, Exception exception = null, params object[] args)
        {
            Serilog.Log.ForContext("SourceIdentifier", "SentryDebug").Verbose(
                "{Message}",
                $@"{logLevel,7}: {string.Format(message, args)} {exception}");
        }
    }
}
#endif