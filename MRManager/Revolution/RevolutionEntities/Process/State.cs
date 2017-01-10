using SystemInterfaces;

namespace RevolutionEntities.Process
{
    public class State : IState
    {
        public State(string status, string name)
        {
            Status = status;
            Name = name;
        }

        public string Name { get; }
        public string Status { get; }
        
    }

    public class StateWithNotes : State, IStateWithNotes
    {
        public StateWithNotes(string status, string name, string notes) : base(status, name)
        {
            Notes = notes;
        }

        public string Notes { get; }
    }
}