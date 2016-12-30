﻿using SystemInterfaces;
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

    public class ProcessState : IProcessState
    {
        public ProcessState(int processId, IProcessStateDetailedInfo stateInfo)
        {
            StateInfo = stateInfo;
            ProcessId = processId;
        }

        public int ProcessId { get; }
        public IProcessStateDetailedInfo StateInfo { get; }
    }

    public class ProcessState<TEntity> :ProcessState, IProcessState<TEntity> where TEntity : IEntity
    {
        public ProcessState(int processId, TEntity entity, IProcessStateDetailedInfo info) : base(processId, info)
        {
            Entity = entity;
        }

        public TEntity Entity { get; }
    }
}