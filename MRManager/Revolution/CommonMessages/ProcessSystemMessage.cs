using SystemInterfaces;

namespace CommonMessages
{
    public class ProcessSystemMessage : SystemMessage, IProcessSystemMessage
    {
        public ProcessSystemMessage(ISystemProcess process, ISystemSource source) : base(source.MachineInfo,source)
        {
            Process = process;
            ParentProcessId = Process.ParentProcessId;
            Name = process.Name;
            Description = process.Description;
            Symbol = process.Symbol;
            User = process.User;
            Id = process.Id;
        }

        public int Id { get; }
        public int ParentProcessId { get; } 
        public string Name { get; }
        public string Description { get; }
        public string Symbol { get; }
        public IUser User { get; }
        public ISystemProcess Process { get; }
    }
}