using SystemInterfaces;
using CommonMessages;

namespace EventMessages
{
  

    public class RequestProcessState : ProcessSystemMessage, IRequestProcessState
    {
        public RequestProcessState(ISystemProcess process, ISystemSource source) : base(process, source)
        {
        }

    }
}