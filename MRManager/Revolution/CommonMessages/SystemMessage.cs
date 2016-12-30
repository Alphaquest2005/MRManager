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
}
