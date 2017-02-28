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
        public Source(Guid sourceId, string sourceName, ISourceType sourceType, ISystemProcess process, IMachineInfo machineInfo)
        {
            SourceId = sourceId;
            SourceName = sourceName;
            MachineInfo = machineInfo;
            Process = process;
            SourceType = sourceType;
        }

        public Guid SourceId { get; }
        public string SourceName { get; }
        public ISourceType SourceType { get; }
        public ISystemProcess Process { get; }
        public IMachineInfo MachineInfo { get; set; }
    }
}
