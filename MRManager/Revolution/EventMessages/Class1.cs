using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages
{
    public class ProcessLogCreated : ProcessSystemMessage, IProcessLogCreated
    {
        public IEnumerable<IComplexEventLog> EventLogs { get; }

        public ProcessLogCreated(IEnumerable<IComplexEventLog> eventLogs, IProcessStateInfo processInfo, ISystemSource source):base(processInfo,source)
        {
            EventLogs = eventLogs;
        }
    }
}
