using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PopClient
{
    internal class ListPopCommand : BasePopCommand
    {
        public ListPopCommand(PopConnection popConnection)
            : base(popConnection)
        {
        }

        protected override string ExecuteInternal(object argument)
        {
            return PopConnection.SendList((int)argument);
        }

        private int size;

        public int Size
        {
            get { return size; }
        }

        protected override void ParseInternal()
        {
            string[] parts = this.Response.Trim().Split();

            if (parts.Length != 3 || !Int32.TryParse(parts[2], out size))
            {
                throw new PopClientException("Unknown LIST response from server.");
            }
        }
    }
}
