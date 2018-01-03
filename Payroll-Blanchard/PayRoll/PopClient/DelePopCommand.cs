using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PopClient
{
    internal class DelePopCommand : BasePopCommand
    {
        public DelePopCommand(PopConnection popConnection)
            : base(popConnection)
        {
        }

        protected override string ExecuteInternal(object argument)
        {
            return PopConnection.SendDele((int)argument);
        }
    }
}
