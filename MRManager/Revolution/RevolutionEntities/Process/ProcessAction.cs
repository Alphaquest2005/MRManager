using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;
using Actor.Interfaces;

namespace RevolutionEntities.Process
{

    public class ProcessAction : IProcessAction
    {
        public Func<IComplexEventParameters, IProcessSystemMessage> Action { get; set; }
        public IProcessStateDetailedInfo ProcessInfo { get; set; }
        public ISourceType ExpectedSourceType { get; set; }

        public ProcessAction(Func<IComplexEventParameters, IProcessSystemMessage> action, IProcessStateDetailedInfo processInfo, ISourceType expectedSourceType)
        {
            Action = action;
            ProcessInfo = processInfo;
            ExpectedSourceType = expectedSourceType;
        }
    }
}
