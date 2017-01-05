﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;

namespace CommonMessages
{
    public class SourceMessage: ISourceMessage
    {
        public SourceMessage(IMessageSource source, IMachineInfo machineInfo)
        {
            Source = source;
            MachineInfo = machineInfo;
        }

        public IMessageSource Source { get; }
        public DateTime MessageDateTime { get; } = DateTime.Now;
        public IMachineInfo MachineInfo { get; }
    }
}
