using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PopClient
{
    public class PopClientLogEventArgs : EventArgs
    {
        /// <summary>
        /// Gets a string representing a message log line
        /// </summary>
        public string Line { get; private set; }

        /// <summary>
        /// Instantiates a new instance of the PopClientLogEventArgs class
        /// </summary>
        /// <param name="line">A string representing a message log line</param>
        public PopClientLogEventArgs(string line)
        {
            this.Line = line;
        }
    }    
}
