using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages.Events
{
    [Export(typeof(IComplexEventLogCreated))]
    public class ComplexEventLogCreated : ProcessSystemMessage, IComplexEventLogCreated
    {
        public ComplexEventLogCreated() { }
        public IEnumerable<IComplexEventLog> EventLog { get; }

        public ComplexEventLogCreated(IEnumerable<IComplexEventLog> logs, IStateEventInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
            EventLog = logs;
        }

    }
}
