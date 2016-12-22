using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;
using RevolutionEntities.Process;

namespace SystemMessages
{
    [Export]
    public class SystemProcessStarted : SystemProcessMessage
    {
        public SystemProcessStarted(IProcess processStep,IMachineInfo machineInfo,IUser user, MessageSource source) : base(new SystemProcess(processStep, machineInfo,user), source)
        {
        }
       
    }

    [Export]
    public class SystemProcessCompleted : SystemProcessMessage
    {
        public SystemProcessCompleted(ISystemProcess process, MessageSource source) : base(process, source)
        {
        }

    }
}