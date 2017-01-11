using SystemInterfaces;

namespace RevolutionEntities.Process
{
    public class StateEventInfo : ProcessStateInfo, IStateEventInfo
    {
        public new IStateEvent State { get; }
        
        public StateEventInfo(int processId, string name, string status, string notes, IStateCommand expectedCommand) : base(processId, new StateEvent(name,status, notes,expectedCommand))
        {
            State = new StateEvent(name, status, notes, expectedCommand);
        }

        public StateEventInfo(int processId, IStateEvent state):base(processId, state)
        {
            State = state;
           
        }
    }
}