// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System;
using System.Runtime.Serialization;

namespace FivePD.Gamemode.Server.Exceptions
{
    /// <summary>
    /// Represents errors that occur during database
    /// connection or usage at runtime.
    /// </summary>
    [Serializable]
    public class DatabaseUsageException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="DatabaseUsageException" /> class.</summary>
        public DatabaseUsageException()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="DatabaseUsageException" /> class with a specified error message.</summary>
        /// <param name="message">The message that describes the error.</param>
        public DatabaseUsageException(string message)
            : base(message)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="DatabaseUsageException" /> class with a specified error message.</summary>
        /// <param name="message">The message that describes the error.</param>
        /// /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public DatabaseUsageException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="DatabaseUsageException" /> class with serialized data.</summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="info" /> is <see langword="null" />.</exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is <see langword="null" /> or <see cref="P:System.Exception.HResult" /> is zero (0).</exception>
        protected DatabaseUsageException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}