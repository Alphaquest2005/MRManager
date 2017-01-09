using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;

namespace SystemMessages
{
  
    [Export]
    public class SystemProcessStarted : ProcessSystemMessage, ISystemProcessStarted
    {
        public SystemProcessStarted(ISystemProcess process, ISystemSource source) : base(process, source)
        {
        }
    }

  
    [Export]
    public class SystemProcessCompleted : ProcessSystemMessage, ISystemProcessCompleted
    {
        public SystemProcessCompleted(ISystemProcess process, ISystemSource source) : base(process, source)
        {
        }
    }

}