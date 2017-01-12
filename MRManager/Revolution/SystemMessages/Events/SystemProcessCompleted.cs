using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;

namespace SystemMessages
{
    [Export]
    public class SystemProcessCompleted : ProcessSystemMessage, ISystemProcessCompleted
    {
        public SystemProcessCompleted(IStateEventInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo, process, source)
        {
        }
    }
}