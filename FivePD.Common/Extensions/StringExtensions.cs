// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System;
using System.Collections.Generic;

namespace FivePD.Common.Extensions
{
    /// <summary>
    /// Holds extension methods for strings.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Replaces parameter identifiers with their values depending on its template.
        /// The template {{key}} will be converted to formatting + key.
        /// The template {{key(value)}} will be converted to formatting + value.
        /// More in-depth example: https://github.com/GTAPoliceMods/FivePD/issues/46#issuecomment-1100093724.
        /// </summary>
        /// <param name="text">The text that's parameters needs to be replaced.</param>
        /// <param name="parameters">A dictionary containing the key + formatting pairs.</param>
        /// <param name="formattingReset">This string will be placed after the replaced value to reset its formatting.</param>
        /// <returns>The new string with the replaced parameters.</returns>
        public static string ReplaceParams(this string text, Dictionary<string, string> parameters, string formattingReset = "")
        {
            foreach (var item in parameters)
            {
                var startIndex = text.IndexOf("{{" + item.Key + "}}", StringComparison.Ordinal);
                if (startIndex == -1)
                {
                    startIndex = text.IndexOf("{{" + item.Key + "(", StringComparison.Ordinal);
                    for (var i = startIndex; i < text.Length; i++)
                    {
                        if (text[i] != ')')
                        {
                            continue;
                        }

                        var sub = text.Substring(startIndex, i - startIndex + 3);
                        if (!sub.StartsWith("{{") || !sub.EndsWith("}}"))
                        {
                            continue;
                        }

                        var newValue = text.Substring(startIndex + 2 + item.Key.Length + 1, i - startIndex - item.Key.Length - 3);
                        text = text.Replace(sub, item.Value + newValue + formattingReset);
                    }
                }
                else
                {
                    text = text.Replace("{{" + item.Key + "}}", item.Value + formattingReset);
                }
            }

            return text;
        }
    }
}