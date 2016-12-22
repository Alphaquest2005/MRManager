using SystemInterfaces;

namespace RevolutionEntities.Process
{
    public class SystemProcess : Process, ISystemProcess
    {
        public SystemProcess(IProcess process, IMachineInfo machineInfo, IUser user) : base(process.Id,process.ProcessId,process.Name,process.Description, process.Symbol)
        {
            MachineInfo = machineInfo;
            User = user;
        }

        public IMachineInfo MachineInfo { get; }
        public IUser User { get; }
    }
}