using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages.Events
{
  
    [Export]
    public class SystemProcessStarted : ProcessSystemMessage, ISystemProcessStarted
    {
        public SystemProcessStarted(IStateEventInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo, process, source)
        {
        }
    }
}