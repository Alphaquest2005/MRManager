using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages.Events
{
 
    [Export]
    public class SystemStarted : ProcessSystemMessage, ISystemStarted
    {
        public SystemStarted(IStateEventInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
        }
    }
}
