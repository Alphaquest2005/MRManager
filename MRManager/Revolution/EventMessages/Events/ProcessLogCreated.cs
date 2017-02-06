using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages.Events
{

    

    [Export(typeof(IProcessLogCreated))]
    public class ProcessLogCreated : ProcessSystemMessage, IProcessLogCreated
    {
        public ProcessLogCreated() { }
        public IEnumerable<IComplexEventLog> EventLogs { get; }

        public ProcessLogCreated(IEnumerable<IComplexEventLog> eventLogs, IStateEventInfo processInfo,ISystemProcess process, ISystemSource source):base(processInfo,process,source)
        {
            EventLogs = eventLogs;
        }
    }
}
