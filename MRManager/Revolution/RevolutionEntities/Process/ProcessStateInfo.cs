using System;
using SystemInterfaces;

namespace RevolutionEntities.Process
{
    public abstract class ProcessStateInfo: IProcessStateInfo
    {
        protected ProcessStateInfo(int processId, IState state)
        {
            ProcessId = processId;
            State = state;
        }

        public int ProcessId { get; }
        public IState State { get; }
    }


}