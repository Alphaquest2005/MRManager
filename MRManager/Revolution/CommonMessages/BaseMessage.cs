using System;
using SystemInterfaces;

namespace CommonMessages
{


    public class SystemMessage :ISystemMessage
    {
        public SystemMessage(IMachineInfo machineInfo, IMessageSource source)
        {
            Source = source;
            MessageDateTime = DateTime.Now;
            MachineInfo = machineInfo;
        }

        public IMessageSource Source { get; }
        public DateTime MessageDateTime { get; }
        public IMachineInfo MachineInfo { get; }
    }

    public interface IProcessSystemMessage:  ISystemMessage, IProcess
    {
        ISystemProcess Process { get; }
    }

    public class ProcessSystemMessage : SystemMessage, IProcessSystemMessage
    {
        public ProcessSystemMessage(ISystemProcess process, ISystemMessage msg) : base(msg.MachineInfo,msg.Source)
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
