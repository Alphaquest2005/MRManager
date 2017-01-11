using SystemInterfaces;

namespace RevolutionEntities.Process
{
    public struct State : IState
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

    public struct StateEvent : IStateEvent
    {
        public StateEvent(string status, string name, string notes, IStateCommand expectedCommand = null) 
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
       
        public StateCommand(string status, string name, IStateEvent expectedEvent = null)
        {
            ExpectedEvent = expectedEvent;
            Status = status;
            Name = name;
            Notes = "";
        }

        public IStateEvent ExpectedEvent { get; }

        public IStateCommand RegisterExpectedEvent(IStateEvent expectedEvent)
        {
            return new StateCommand(this.Status, this.Name, expectedEvent);
        }

        public string Name { get; }
        public string Status { get; }
        public string Notes { get; }
    }
}