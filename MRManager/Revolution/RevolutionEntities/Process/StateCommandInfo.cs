using SystemInterfaces;

namespace RevolutionEntities.Process
{
    public class StateCommandInfo : ProcessStateInfo, IStateCommandInfo
    {
        public StateCommandInfo(int processId, IStateCommand state, IStateEvent expectedEvent):base(processId, state)
        {
            State = state;
            ExpectedEvent = expectedEvent;
        }

        public new IStateCommand State { get; }
        public IStateEvent ExpectedEvent { get; }
    }
}