using SystemInterfaces;
using CommonMessages;

namespace EventMessages
{
    public class RequestProcessState : ProcessSystemMessage
    {
        public RequestProcessState(ISystemProcess process, ISourceMessage sourceMsg) : base(process, sourceMsg)
        {
        }

    }
}