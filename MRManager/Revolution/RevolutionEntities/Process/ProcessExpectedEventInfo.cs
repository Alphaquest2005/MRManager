using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolutionEntities.Process
{
    public class ProcessExpectedEventInfo
    {
        public Type EventType { get; set; }
        public ProcessStateDetailedInfo ProcessInfo { get; set; }
        public SourceType ExpectedSourceType { get; set; }

        public ProcessExpectedEventInfo(Type eventType, ProcessStateDetailedInfo processInfo, SourceType expectedSourceType)
        {
            EventType = eventType;
            ProcessInfo = processInfo;
            ExpectedSourceType = expectedSourceType;
        }
    }
}
