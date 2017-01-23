﻿using System.Collections.Generic;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages.Events
{
    public class ProcessLogCreated : ProcessSystemMessage, IProcessLogCreated
    {
        public IEnumerable<IComplexEventLog> EventLogs { get; }

        public ProcessLogCreated(IEnumerable<IComplexEventLog> eventLogs, IStateEventInfo processInfo,ISystemProcess process, ISystemSource source):base(processInfo,process,source)
        {
            EventLogs = eventLogs;
        }
    }
}
