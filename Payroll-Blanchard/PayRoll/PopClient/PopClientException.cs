using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PopClient
{
    [Serializable]
    public class PopClientException : Exception
    {
        /// <summary>
        /// Gets a value indicating whether the PopClient is busy.
        /// </summary>
        public bool PopClientBusy { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether the fetch operation was cancelled by the user.
        /// </summary>
        public bool PopClientUserCancelled { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the PopClientException class.
        /// </summary>
        public PopClientException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the PopClientException class.
        /// </summary>
        /// <param name="message">The message that describes the error</param>
        public PopClientException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the PopClientException class.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data</param>
        /// <param name="context">The contextual information about the source or destination</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
