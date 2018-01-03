using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace PopClient
{
    internal class RetrPopCommand : BasePopCommand
    {
        public MailMessage Message { get; private set; }

        public DateTime ReceivedTime  { get; private set; }

        public RetrPopCommand(PopConnection popConnection)
            : base(popConnection)
        {
        }

        protected override string ExecuteInternal(object argument)
        {
            return PopConnection.SendRetr((int)argument);
        }

        protected override void ParseInternal()
        {
            var converter = new CDOMessageConverter(this.MultiLineResponse);
            this.Message = converter.ToMailMessage();
            this.ReceivedTime = converter.ReceivedTime;
        }

        public override bool IsMultiLineResponse
        {
            get
            {
                return true;
            }
        }
    }
}
