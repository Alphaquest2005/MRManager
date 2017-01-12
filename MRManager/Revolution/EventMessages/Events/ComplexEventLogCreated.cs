using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages
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
