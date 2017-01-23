using System.Collections.Generic;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages.Events
{

    public class ComplexEventLogCreated : ProcessSystemMessage, IComplexEventLogCreated
    {
        public IEnumerable<IComplexEventLog> EventLog { get; }

        public ComplexEventLogCreated(IEnumerable<IComplexEventLog> logs, IStateEventInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
            EventLog = logs;
        }

    }
}
