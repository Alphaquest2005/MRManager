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
        public Func<IComplexEventParameters, IProcessStateInfo> ProcessInfo { get; set; }
        public ISourceType ExpectedSourceType { get; set; }

        public ProcessAction(Func<IComplexEventParameters, IProcessSystemMessage> action, Func<IComplexEventParameters, IProcessStateInfo> processInfo, ISourceType expectedSourceType)
        {
            Action = action;
            ProcessInfo = processInfo;
            ExpectedSourceType = expectedSourceType;
        }
    }
}
