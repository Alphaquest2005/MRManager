using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PopClient
{
    internal class QuitPopCommand : BasePopCommand
    {
        public QuitPopCommand(PopConnection popConnection)
            : base(popConnection)
        {
        }

        protected override string ExecuteInternal(object argument)
        {
            return PopConnection.SendQuit();
        }
    }
}
