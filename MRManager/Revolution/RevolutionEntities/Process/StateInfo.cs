using SystemInterfaces;

namespace RevolutionEntities.Process
{
    public class StateInfo : ProcessStateInfo, IStateInfo
    {
        public StateInfo(int processId, IState state) : base(processId, state)
        {
        }
    }
}