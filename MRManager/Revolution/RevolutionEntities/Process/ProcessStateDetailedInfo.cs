using System;
using SystemInterfaces;

namespace RevolutionEntities.Process
{
    public class ProcessStateInfo: IProcessStateInfo
    {
        public ProcessStateInfo(ISystemProcess process, IState state)
        {
            Process = process;
            State = state;
        }

        public ISystemProcess Process { get; }
        public IState State { get; }
    }

    public class DynamicProcessStateInfo : ProcessStateInfo
    {
        public DynamicProcessStateInfo(ISystemProcess process, string name, string status, string notes) : base(process, new StateWithNotes(name,status, notes))
        {
        }
    }


}