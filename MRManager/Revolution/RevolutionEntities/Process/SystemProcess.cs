using SystemInterfaces;
using DataInterfaces;

namespace RevolutionEntities.Process
{
    public class SystemProcess : Process, ISystemProcess
    {
        public SystemProcess(IProcess process, IMachineInfo machineInfo) : base(process.Id,process.ParentProcessId,process.Name,process.Description, process.Symbol, process.User)
        {
            MachineInfo = machineInfo;
        }

        public IMachineInfo MachineInfo { get; }
   
    }

    public class SystemProcess<TEntity> : SystemProcess, ISystemEntityProcess<TEntity> where TEntity : IEntity
    {
        public SystemProcess(TEntity entity, ISystemProcess process) : base(process, process.MachineInfo)
        {
            Entity = entity;
        }

        public TEntity Entity { get; }
    }
}