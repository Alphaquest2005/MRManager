using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;
using RevolutionEntities.Process;

namespace SystemMessages
{
    [Export]
    public class SystemProcessStarted : ProcessSystemMessage
    {
        public SystemProcessStarted(ISystemProcess process, ISourceMessage sourceMsg) : base(process, sourceMsg)
        {
        }
    }

    [Export]
    public class SystemProcessCompleted : ProcessSystemMessage
    {
        public SystemProcessCompleted(ISystemProcess process, ISourceMessage sourceMsg) : base(process, sourceMsg)
        {
        }
    }

}