using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PopClient
{
    internal class PopClientDirectionalLogEventArgs : PopClientLogEventArgs
    {
        public bool IsServerResponse { get; private set; }

        public PopClientDirectionalLogEventArgs(string line, bool isServerResponse)
            : base(line)
        {
            this.IsServerResponse = isServerResponse;
        }
    }
}
