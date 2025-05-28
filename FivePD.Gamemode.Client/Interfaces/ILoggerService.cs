// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System;

namespace FivePD.Gamemode.Client.Interfaces
{
    /// <summary>
    /// Wrapper for FiveM's Debug.WriteLine.
    /// </summary>
    public interface ILoggerService
    {
        /// <summary>
        /// Log an informational message. E.g.:
        /// <code>Info("My info: {variable}", variable);</code>
        /// <code>Info("My serialized info: {@variable}", variable);</code>
        /// </summary>
        /// <param name="message">Message to log.</param>
        /// <param name="parameters">Parameters to insert.</param>
        public void Info(string message, params dynamic[] parameters);

        /// <summary>
        /// Log an warning message. E.g.:
        /// <code>Info("My warning: {variable}", variable);</code>
        /// <code>Info("My serialized warning: {@variable}", variable);</code>
        /// </summary>
        /// <param name="message">Message to log.</param>
        /// <param name="parameters">Parameters to insert.</param>
        public void Warn(string message, params dynamic[] parameters);

        /// <summary>
        /// Log an error message. E.g.:
        /// <code>Info("My error: {variable}", variable);</code>
        /// <code>Info("My serialized error: {@variable}", variable);</code>
        /// </summary>
        /// <param name="message">Message to log.</param>
        /// <param name="parameters">Parameters to insert.</param>
        public void Error(string message, params dynamic[] parameters);

        /// <summary>
        /// Log an exception.
        /// </summary>
        /// <param name="exception">Exception to log.</param>
        public void Error(Exception exception);
    }
}