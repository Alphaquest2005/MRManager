using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;
using RevolutionEntities.Process;

namespace RevolutionData
{
    public class MachineInfoData
    {
        public static List<IMachineInfo> MachineInfos = new List<IMachineInfo>()
        {
            new MachineInfo(machineName:"ALPHAQUEST-PC",
                processors:8)
        };

    }
}
