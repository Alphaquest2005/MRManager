using SystemInterfaces;
using CommonMessages;

namespace EventMessages.Commands
{
  

    public class RequestProcessState : ProcessSystemMessage, IRequestProcessState
    {
        public RequestProcessState(IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
        }

    }
}