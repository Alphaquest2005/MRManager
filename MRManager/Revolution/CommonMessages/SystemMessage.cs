using System;
using System.ComponentModel.Composition;
using SystemInterfaces;

namespace CommonMessages
{
    [Export(typeof(ISystemMessage))]
    public class SystemMessage :ISystemMessage
    {
        public SystemMessage()
        {
            
        }
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
