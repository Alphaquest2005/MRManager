using SystemInterfaces;
using CommonMessages;

namespace EventMessages
{
  

    public class RequestProcessState : ProcessSystemMessage, IRequestProcessState
    {
        public RequestProcessState(IProcessStateInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
        }

    }
}