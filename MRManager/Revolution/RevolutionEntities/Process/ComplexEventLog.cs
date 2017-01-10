using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;

namespace RevolutionEntities.Process
{
    public class ComplexEventLog : IComplexEventLog
    {
        public string Operation { get;  }
        public string Status { get;  }
        public DateTime Time { get;  }
        public string SourceGuid { get;  }
        public string SourceName { get; }
        public string Source { get; }
        public string ExpectedSource { get; }
        public string Message { get; }
        public string ProcessInfo { get; }

        public ComplexEventLog(string operation, string status, DateTime time, string sourceGuid, string sourceName, string source, string expectedSource, string message, string processInfo)
        {
            Operation = operation;
            Status = status;
            Time = time;
            SourceGuid = sourceGuid;
            SourceName = sourceName;
            Source = source;
            ExpectedSource = expectedSource;
            Message = message;
            ProcessInfo = processInfo;
        }
    }
}
