using SystemInterfaces;
using CommonMessages;

namespace EventMessages.Commands
{

    public class RequestProcessLog:ProcessSystemMessage, IRequestProcessLog
    {
        public RequestProcessLog(IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source):base(processInfo,process, source)
        {
            
        }
    }
}
