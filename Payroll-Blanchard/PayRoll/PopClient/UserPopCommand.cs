using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PopClient
{
    internal class UserPopCommand : BasePopCommand
    {
        public UserPopCommand(PopConnection popConnection)
            : base(popConnection)
        {
        }

        protected override string ExecuteInternal(object argument)
        {
            return PopConnection.SendUser(argument as string);
        }
    }
}
