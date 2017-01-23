using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages.Events
{
    [Export]
    public class SystemProcessCleanedUp : ProcessSystemMessage, ISystemProcessCleanedUp
    {
        public SystemProcessCleanedUp(IStateEventInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo, process, source)
        {
        }
    }
}