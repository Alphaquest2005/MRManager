using System.Collections.Generic;
using SystemInterfaces;
using RevolutionEntities.Process;

namespace RevolutionData
{
    public class MachineInfoData
    {
        public static List<IMachineInfo> MachineInfos = new List<IMachineInfo>
        {
            new MachineInfo("ALPHAQUEST-PC",
                8)
        };

    }
}
