using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;

namespace SystemMessages
{
  
    [Export]
    public class SystemProcessStarted : ProcessSystemMessage, ISystemProcessStarted
    {
        public SystemProcessStarted(IProcessStateInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo, process, source)
        {
        }
    }

  
    [Export]
    public class SystemProcessCompleted : ProcessSystemMessage, ISystemProcessCompleted
    {
        public SystemProcessCompleted(IProcessStateInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo, process, source)
        {
        }
    }

}