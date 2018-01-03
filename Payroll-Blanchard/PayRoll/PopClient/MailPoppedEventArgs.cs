using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace PopClient
{
    public class MailPoppedEventArgs : EventArgs
    {
        /// <summary>
        /// Instantiates a new instance of the MailPoppedEventArgs class
        /// </summary>
        /// <param name="index">The index of the message</param>
        /// <param name="message">A MailMessage that contains the fetched message</param>
        /// <param name="size">The size of the mail</param>
        /// <param name="uidl">The uidl value of the message</param>
        /// <param name="receivedTime">Time when the message was received by the server</param>
        public MailPoppedEventArgs(int index, MailMessage message, int size, string uidl, DateTime receivedTime)
        {
            this.Index = index;
            this.Message = message;
            this.Size = size;
            this.Uidl = uidl;
            this.ReceivedTime = receivedTime;
        }

        /// <summary>
        /// Gets the index of the message
        /// </summary>
        public int Index { get; private set; }

        /// <summary>
        /// Gets the size of the mail
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// Gets the uidl value of the message
        /// </summary>
        public string Uidl { get; private set; }

        /// <summary>
        /// Gets the MailMessage that contains the fetched message
        /// </summary>
        public MailMessage Message { get; private set; }

        /// <summary>
        /// Gets the time when the message was received by the server
        /// </summary>
        public DateTime ReceivedTime { get; private set; }
    }
}
