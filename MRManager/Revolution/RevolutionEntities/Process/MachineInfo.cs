using SystemInterfaces;

namespace RevolutionEntities.Process
{

    public class MachineInfo : IMachineInfo
    {
        public MachineInfo(string machineName, int processors)
        {
            MachineName = machineName;
            Processors = processors;
        }

        public string MachineName { get; }
        public int Processors { get; }
    }
}
