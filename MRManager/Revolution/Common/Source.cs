using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;

namespace Common
{
    public class Source: ISystemSource
    {
        public Source(Guid sourceId, string sourceName, Type sourceType, IMachineInfo machineInfo)
        {
            SourceId = sourceId;
            SourceName = sourceName;
            MachineInfo = machineInfo;
            SourceType = sourceType;
        }

        public Guid SourceId { get; }
        public string SourceName { get; }
        public Type SourceType { get; }
        public IMachineInfo MachineInfo { get; set; }
    }
}
