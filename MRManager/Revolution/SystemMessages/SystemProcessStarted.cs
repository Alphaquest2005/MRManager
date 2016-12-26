using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;
using RevolutionEntities.Process;

namespace SystemMessages
{
    [Export]
    public class SystemProcessStarted : ProcessSystemMessage
    {
        public SystemProcessStarted(ISystemProcess process, ISystemMessage msg) : base(process, msg)
        {
        }
    }

    [Export]
    public class SystemProcessCompleted : ProcessSystemMessage
    {
        public SystemProcessCompleted(ISystemProcess process, ISystemMessage msg) : base(process, msg)
        {
        }
    }
}