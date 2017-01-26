using SystemInterfaces;

namespace RevolutionEntities.Process
{
    public struct State : IState
    {
        public State(  string name, string status, string notes)
        {
            Status = status;
            Name = name;
            Notes = notes;
        }

        public string Name { get; }
        public string Status { get; }
        public string Notes { get; }
    }

    public struct StateEvent : IStateEvent
    {
        public StateEvent(string name, string status, string notes, IStateCommand expectedCommand = null) 
        {
            ExpectedCommand = expectedCommand;
            Status = status;
            Name = name;
            Notes = notes;
        }

        public IStateCommand ExpectedCommand { get; }
        public string Name { get; }
        public string Status { get; }
        public string Notes { get; }
    }

    public struct StateCommand : IStateCommand
    {
       
        public StateCommand(string name, string status, IStateEvent expectedEvent = null)
        {
            ExpectedEvent = expectedEvent;
            Status = status;
            Name = name;
            Notes = "";
        }

        public IStateEvent ExpectedEvent { get; }
        public string Name { get; }
        public string Status { get; }
        public string Notes { get; }
    }
}