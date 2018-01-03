using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PopClient
{
    internal class UidlPopCommand : BasePopCommand
    {
        public UidlPopCommand(PopConnection popConnection)
            : base(popConnection)
        {
        }

        protected override string ExecuteInternal(object argument)
        {
            return PopConnection.SendUidl((int)argument);
        }

        private string uidl;

        public string Uidl
        {
            get { return uidl; }
        }

        protected override void ParseInternal()
        {
            string[] parts = this.Response.Trim().Split();

            if (parts.Length != 3)
            {
                throw new PopClientException("Unknown LIST response from server.");
            }

            uidl = parts[2];
        }
    }
}
