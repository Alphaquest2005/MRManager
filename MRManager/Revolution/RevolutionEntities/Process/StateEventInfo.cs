using SystemInterfaces;

namespace RevolutionEntities.Process
{
    public class StateEventInfo : ProcessStateInfo, IStateEventInfo
    {
        public new IStateEvent State { get; }
        public IStateCommand ExpectedCommand { get; }

        public StateEventInfo(int processId, string name, string status, string notes, IStateCommand expectedCommand) : base(processId, new StateEvent(name,status, notes))
        {
            ExpectedCommand = expectedCommand;
            State = new StateEvent(name, status, notes);
        }

        public StateEventInfo(int processId, IStateEvent state, IStateCommand expectedCommand):base(processId, state)
        {
            State = state;
            ExpectedCommand = expectedCommand;
        }
    }
}