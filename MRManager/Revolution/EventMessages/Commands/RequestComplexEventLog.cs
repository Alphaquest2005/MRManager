using SystemInterfaces;
using CommonMessages;

namespace EventMessages.Commands
{
    public class RequestComplexEventLog : ProcessSystemMessage, IRequestComplexEventLog
    {
        public RequestComplexEventLog(IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
        }
    }
}
