using System;
using SystemInterfaces;

namespace CommonMessages
{


    public class SystemMessage :ISystemMessage
    {
        public SystemMessage(IMachineInfo machineInfo, IUser user, MessageSource source)
        {
            Source = source;
            User = user;
            MachineInfo = machineInfo;
            MessageDateTime = DateTime.Now;
        }

        public MessageSource Source { get; }
        public IMachineInfo MachineInfo { get; }

        public IUser User { get; }
        public DateTime MessageDateTime { get; }
    }

    public interface ISystemProcessMessage
    {
        ISystemProcess Process { get; }
    }

    public class SystemProcessMessage : SystemMessage, ISystemProcessMessage
    {
        public ISystemProcess Process { get; }

        public SystemProcessMessage(ISystemProcess process, MessageSource source):base(process.MachineInfo, process.User,source)
        {
            Process = process;
        }
    }
}
