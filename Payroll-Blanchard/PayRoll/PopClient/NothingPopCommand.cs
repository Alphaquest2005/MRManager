using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PopClient
{
    internal class NothingPopCommand : BasePopCommand
    {
        public NothingPopCommand(PopConnection popConnection)
            : base(popConnection)
        {
        }

        protected override string ExecuteInternal(object argument)
        {
            return String.Empty;
        }
    }
}
