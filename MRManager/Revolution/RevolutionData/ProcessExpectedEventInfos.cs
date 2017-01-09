using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;
using Actor.Interfaces;
using RevolutionEntities.Process;

namespace RevolutionData
{
    class ProcessExpectedEventInfos
    {
        public static ProcessExpectedEventInfo ProcessStarted = new ProcessExpectedEventInfo(eventType: typeof(ISystemProcessStarted), processInfo: new ProcessStateDetailedInfo("Process Started", "First Step"), expectedSourceType: new SourceType(typeof(IProcessService)));
    }
}
