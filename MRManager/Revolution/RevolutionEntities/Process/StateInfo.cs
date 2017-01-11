using SystemInterfaces;

namespace RevolutionEntities.Process
{
    public class StateInfo : ProcessStateInfo, IStateInfo
    {
        public StateInfo(int processId, IState state) : base(processId, state)
        {
        }

        public StateInfo(int processId, string name, string status, string notes) : base(processId, new State(name,status, notes))
        {
           
        }
    }
}