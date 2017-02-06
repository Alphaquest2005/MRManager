using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages.Commands
{
    [Export(typeof(IRequestProcessState))]


    public class RequestProcessState : ProcessSystemMessage, IRequestProcessState
    {
        public RequestProcessState() { }
        public RequestProcessState(IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
        }

    }
}