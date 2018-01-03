using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PopClient
{
    public class MailPopInfoFetchedEventArgs : EventArgs
    {
        /// <summary>
        ///  Instantiates a new instance of the MailPopInfoFetchedEventArgs class
        /// </summary>
        /// <param name="count">The number of messages</param>
        /// <param name="size">Total size of all messages</param>
        public MailPopInfoFetchedEventArgs(int count, int size)
        {
            this.Count = count;
            this.Size = size;
        }

        /// <summary>
        /// Get the number of messages
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Gets the total size of all messages
        /// </summary>
        public int Size { get; private set; }
    }
}
