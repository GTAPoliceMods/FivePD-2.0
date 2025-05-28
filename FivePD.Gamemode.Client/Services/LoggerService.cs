// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using CitizenFX.Core;
using FivePD.Gamemode.Client.Interfaces;
using Newtonsoft.Json;

namespace FivePD.Gamemode.Client.Services
{
    /// <inheritdoc />
    public class LoggerService : ILoggerService
    {
        /// <summary>
        /// Defines the level of info being logged.
        /// </summary>
        private enum LogLevel
        {
            /// <summary>
            /// Informational messages that highlight the progress of the application.
            /// </summary>
            Info,

            /// <summary>
            /// Potentially harmful situations of interest that indicate potential problems.
            /// </summary>
            Warning,

            /// <summary>
            /// Error events of considerable importance that will prevent normal program execution.
            /// </summary>
            Error,
        }

        /// <inheritdoc />
        public void Info(string message, params dynamic[] parameters)
        {
            this.LogToConsole(LogLevel.Info, message, parameters);
        }

        /// <inheritdoc />
        public void Warn(string message, params dynamic[] parameters)
        {
            this.LogToConsole(LogLevel.Warning, message, parameters);
        }

        /// <inheritdoc />
        public void Error(string message, params dynamic[] parameters)
        {
            this.LogToConsole(LogLevel.Error, message, parameters);
        }

        /// <inheritdoc />
        public void Error(Exception exception)
        {
            this.LogToConsole(LogLevel.Error, exception.Message);
            this.LogToConsole(LogLevel.Error, exception.StackTrace);
        }

        private void LogToConsole(LogLevel logLevel, string message, params dynamic[] parameters)
        {
            StringBuilder extraData = new StringBuilder();

            // `null` was passed as the only parameter
            if (parameters is null)
            {
                parameters = new dynamic[] { null };
            }

            if (parameters.Length > 0)
            {
                List<string> matches = new List<string>();
                foreach (string word in message.Split(new[] { ' ', '.', ',', ':', '-', '/', '\\' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (word.StartsWith("{") && word.EndsWith("}"))
                    {
                        matches.Add(word);
                    }
                }

                for (int i = 0; i < parameters.Length; i++)
                {
                    if (i >= matches.Count)
                    {
                        extraData.Append($"\n{JsonConvert.SerializeObject(parameters[i], Formatting.Indented)}\n");
                        continue;
                    }

                    string param = matches[i];
                    bool serialize = param.Contains('@');

                    if (serialize)
                    {
                        string json = JsonConvert.SerializeObject(parameters[i], Formatting.Indented);
                        json = string.Join(" ", json.Replace("\n", " ").Replace("\r", " ").Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
                        message = message.Replace(param, $"{json}");
                    }
                    else
                    {
                        if (parameters[i] is null)
                        {
                            message = message.Replace(param, "null");
                        }
                        else
                        {
                            message = message.Replace(param, parameters[i].ToString());
                        }
                    }
                }
            }

            switch (logLevel)
            {
                case LogLevel.Info:
                    Debug.WriteLine($"[FivePD ^5INFO^7]: ^5{message}");
                    break;

                case LogLevel.Warning:
                    Debug.WriteLine($"[FivePD ^3WARNING^7]: ^3{message}");
                    break;

                case LogLevel.Error:
                    Debug.WriteLine($"[FivePD ^1ERROR^7]: ^1{message}");
                    break;
            }

            if (extraData.Length > 0)
            {
                Debug.WriteLine(extraData.ToString());
            }
        }
    }
}