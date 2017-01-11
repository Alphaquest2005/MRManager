using SystemInterfaces;

namespace RevolutionEntities.Process
{
    public class StateCommandInfo : ProcessStateInfo, IStateCommandInfo
    {
        public StateCommandInfo(int processId, IStateCommand state):base(processId, state)
        {
            State = state;
           
        }


        public new IStateCommand State { get; }
    }
}