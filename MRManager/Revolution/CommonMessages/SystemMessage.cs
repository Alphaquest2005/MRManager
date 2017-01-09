using System;
using SystemInterfaces;

namespace CommonMessages
{
    public class SystemMessage :ISystemMessage
    {
        public SystemMessage(IMachineInfo machineInfo, ISource source)
        {
            Source = source;
            MessageDateTime = DateTime.Now;
            MachineInfo = machineInfo;
        }

        public ISource Source { get; }
        public DateTime MessageDateTime { get; }
        public IMachineInfo MachineInfo { get; }
    }
}
