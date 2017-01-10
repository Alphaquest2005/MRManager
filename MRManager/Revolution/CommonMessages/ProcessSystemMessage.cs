using SystemInterfaces;

namespace CommonMessages
{
    public class ProcessSystemMessage : SystemMessage, IProcessSystemMessage
    {
        public ProcessSystemMessage(IProcessStateInfo processInfo, ISystemSource source) : base(source.MachineInfo,source)
        {
            Process = processInfo.Process;
            ProcessInfo = processInfo;
            ParentProcessId = Process.ParentProcessId;
            Name = processInfo.Process.Name;
            Description = processInfo.Process.Description;
            Symbol = processInfo.Process.Symbol;
            User = processInfo.Process.User;
            Id = processInfo.Process.Id;
        }

        public int Id { get; }
        public int ParentProcessId { get; } 
        public string Name { get; }
        public string Description { get; }
        public string Symbol { get; }
        public IUser User { get; }
        public ISystemProcess Process { get; }
        public IProcessStateInfo ProcessInfo { get; }
    }
}