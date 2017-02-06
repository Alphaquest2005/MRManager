using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages.Commands
{
    [Export(typeof(IRequestProcessLog))]

    public class RequestProcessLog:ProcessSystemMessage, IRequestProcessLog
    {
        public RequestProcessLog() { }
        public RequestProcessLog(IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source):base(processInfo,process, source)
        {
            
        }
    }
}
