﻿using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;


namespace SystemMessages
{
    [Export]
    public class SystemStarted : ProcessSystemMessage
    {
        public SystemStarted(ISystemProcess process, ISourceMessage msg) : base(process, msg)
        {
        }
    }
}
