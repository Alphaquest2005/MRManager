using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PopClient
{
    internal class StatPopCommand : BasePopCommand
    {
        public StatPopCommand(PopConnection popConnection)
            : base(popConnection)
        {
        }

        protected override string ExecuteInternal(object argument)
        {
            return PopConnection.SendStat();
        }

        private int count;

        public int Count
        {
            get { return count; }
        }

        private int size;

        public int Size
        {
            get { return size; }
        }

        protected override void ParseInternal()
        {
            string[] parts = this.Response.Trim().Split();

            if (parts.Length != 3 || !Int32.TryParse(parts[1], out count) || !Int32.TryParse(parts[2], out size))
            {
                throw new PopClientException("Unknown STAT response from server.");
            }
        }
    }
}
