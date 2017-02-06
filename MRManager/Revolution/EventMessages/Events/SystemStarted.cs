using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages.Events
{

    [Export(typeof(ISystemStarted))]
    public class SystemStarted : ProcessSystemMessage, ISystemStarted
    {
        public SystemStarted() { }
        public SystemStarted(IStateEventInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
        }
    }
}
