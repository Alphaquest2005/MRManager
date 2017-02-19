using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;
using Actor.Interfaces;
using MefContrib;
using System.Diagnostics.Contracts;

namespace RevolutionEntities.Process
{

    public class ProcessAction : IProcessAction
    {
        public Func<IComplexEventParameters, Task<IProcessSystemMessage>> Action { get; set; }
        public Func<IComplexEventParameters, IProcessStateInfo> ProcessInfo { get; set; }
        public ISourceType ExpectedSourceType { get; set; }

        public ProcessAction(Func<IComplexEventParameters, Task<IProcessSystemMessage>> action, Func<IComplexEventParameters, IStateCommandInfo> processInfo, ISourceType expectedSourceType)
        {
            Contract.Requires(action != null);
            Action = action;
            ProcessInfo = processInfo;
            ExpectedSourceType = expectedSourceType;
        }
    }
}
