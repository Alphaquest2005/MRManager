using SystemInterfaces;

namespace RevolutionEntities.Process
{
    public class Process : IProcess
    {
        public Process(int id,int processId, string name, string description, string symbol)
        {
            Id = id;
            ProcessId = processId;
            Name = name;
            Description = description;
            Symbol = symbol;
        }

        public int Id { get; }
        public int ProcessId { get; set; }
        public string Name { get; }
        public string Description { get; }
        public string Symbol { get; }
    }
}
