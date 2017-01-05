﻿using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;

namespace SystemMessages
{
  
    [Export]
    public class SystemProcessStarted : ProcessSystemMessage, ISystemProcessStarted
    {
        public SystemProcessStarted(ISystemProcess process, ISourceMessage sourceMsg) : base(process, sourceMsg)
        {
        }
    }

  
    [Export]
    public class SystemProcessCompleted : ProcessSystemMessage, ISystemProcessCompleted
    {
        public SystemProcessCompleted(ISystemProcess process, ISourceMessage sourceMsg) : base(process, sourceMsg)
        {
        }
    }

}