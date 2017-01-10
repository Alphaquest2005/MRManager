using SystemInterfaces;
using Actor.Interfaces;
using RevolutionEntities.Process;

namespace RevolutionData
{
    class ProcessExpectedEventInfos
    {
        public static ProcessExpectedEventInfo ProcessStarted = new ProcessExpectedEventInfo(typeof(ISystemProcessStarted), new ProcessStateInfo("Process Started", "First Step"), new SourceType(typeof(IProcessService)));
    }
}
