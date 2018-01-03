using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PopClient
{
    internal interface IPopCommand
    {
        event EventHandler<PopClientDirectionalLogEventArgs> PopClientLog;

        void Execute(params object[] arguments);

        bool IsMultiLineResponse { get; }
    }
}
