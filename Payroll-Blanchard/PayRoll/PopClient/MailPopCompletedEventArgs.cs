using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PopClient
{
    public class MailPopCompletedEventArgs : EventArgs
    {
        /// <summary>
        /// Instantiates a new instance of the MailPopCompletedEventArgs class
        /// </summary>
        public MailPopCompletedEventArgs()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the MailPopCompletedEventArgs class
        /// </summary>
        /// <param name="ex">Any exception that was thrown during the asynchronous fetch</param>
        public MailPopCompletedEventArgs(Exception ex)
        {
            this.Exception = ex;
            this.Aborted = true;
        }

        /// <summary>
        /// Gets any exception that was thrown during the asynchronous fetch
        /// </summary>
        public Exception Exception { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the fetch operation was aborted
        /// </summary>
        public bool Aborted { get; private set; }
    }
}
