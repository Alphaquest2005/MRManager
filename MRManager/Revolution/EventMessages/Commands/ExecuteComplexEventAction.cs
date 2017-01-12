using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;
using Actor.Interfaces;
using CommonMessages;

namespace EventMessages
{
    public class ExecuteComplexEventAction:ProcessSystemMessage, IExecuteComplexEventAction
    {
        public IProcessAction Action { get;  }
        public IComplexEventParameters ComplexEventParameters { get; }


        public ExecuteComplexEventAction(IProcessAction action, IComplexEventParameters complexEventParameters, IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo ,process, source)
        {
            Action = action;
            ComplexEventParameters = complexEventParameters;
        }
    }
}
