using SystemInterfaces;

namespace RevolutionEntities.Process
{
    public class State : IState
    {
        public State(string status, string name, string notes)
        {
            Status = status;
            Name = name;
            Notes = notes;
        }

        public string Name { get; }
        public string Status { get; }
        public string Notes { get; }
    }

    public class StateEvent : State, IStateEvent
    {
        public StateEvent(string status, string name, string notes) : base(status, name, notes)
        {
        }
    }

    public class StateCommand :State, IStateCommand
    {
        public StateCommand(string status, string name) : base(status, name, "")
        {
        }
    }
}