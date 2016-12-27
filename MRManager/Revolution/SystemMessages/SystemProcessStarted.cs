using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;
using RevolutionEntities.Process;

namespace SystemMessages
{
    [Export]
    public class SystemProcessStarted : ProcessSystemMessage
    {
        public SystemProcessStarted(ISystemProcess process, ISourceMessage msg) : base(process, msg)
        {
        }
    }

    [Export]
    public class SystemProcessCompleted : ProcessSystemMessage
    {
        public SystemProcessCompleted(ISystemProcess process, ISourceMessage msg) : base(process, msg)
        {
        }
    }
}