// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System;
using System.Runtime.Serialization;

namespace FivePD.Common.Exceptions
{
    /// <inheritdoc />
    [Serializable]
    public class EmptyConfigException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyConfigException"/> class.
        /// </summary>
        public EmptyConfigException()
            : base(string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyConfigException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> of this exception.</param>
        /// <param name="context">The <see cref="StreamingContext"/> of this exception.</param>
        protected EmptyConfigException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}