using System;
using SystemInterfaces;

namespace CommonMessages
{
    public class SystemMessage :ISystemMessage
    {
        public SystemMessage(IMachineInfo machineInfo, ISystemSource source)
        {
            Source = source;
            MessageDateTime = DateTime.Now;
            MachineInfo = machineInfo;
        }

        public ISystemSource Source { get; }
        public DateTime MessageDateTime { get; }
        public IMachineInfo MachineInfo { get; }
    }
}
