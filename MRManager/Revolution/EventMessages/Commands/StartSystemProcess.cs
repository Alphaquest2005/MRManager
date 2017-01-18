using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages
{

    public class StartSystemProcess:ProcessSystemMessage, IStartSystemProcess
    {
        public int ProcessToBeStartedId { get; }

        public StartSystemProcess(int processId,IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source):base(processInfo, process, source)
        {
            ProcessToBeStartedId = processId;
        }
    }
}
