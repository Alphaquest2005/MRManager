using SystemInterfaces;

namespace RevolutionEntities.Process
{
    public class MachineInfo : IMachineInfo
    {
        public MachineInfo(string machineName, string machineLocation, int processors)
        {
            MachineName = machineName;
            MachineLocation = machineLocation;
            Processors = processors;
        }

        public string MachineName { get; }
        public string MachineLocation { get; }
        public int Processors { get; }
     }
}
