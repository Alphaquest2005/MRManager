using System.Collections.Generic;
using SystemInterfaces;
using RevolutionEntities.Process;

namespace DataServices.Actors
{
    public class MachineInfoData
    {
        public static List<IMachineInfo> MachineInfos = new List<IMachineInfo>()
        {
            new MachineInfo(machineName:"ALPHAQUEST-PC",
                machineLocation:"Home",
                processors:8)
        };

    }
}