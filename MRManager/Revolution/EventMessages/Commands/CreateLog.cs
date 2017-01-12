using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages
{

    public class RequestProcessLog:ProcessSystemMessage, IRequestProcessLog
    {
        public RequestProcessLog(IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source):base(processInfo,process, source)
        {
            
        }
    }
}
