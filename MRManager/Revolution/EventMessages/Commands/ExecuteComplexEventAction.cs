using System.ComponentModel.Composition;
using SystemInterfaces;
using Actor.Interfaces;
using CommonMessages;

namespace EventMessages.Commands
{
    [Export(typeof(IExecuteComplexEventAction))]
    public class ExecuteComplexEventAction:ProcessSystemMessage, IExecuteComplexEventAction
    {
        public ExecuteComplexEventAction() { }
        public IProcessAction Action { get;  }
        public IComplexEventParameters ComplexEventParameters { get; }


        public ExecuteComplexEventAction(IProcessAction action, IComplexEventParameters complexEventParameters, IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo ,process, source)
        {
            Action = action;
            ComplexEventParameters = complexEventParameters;
        }
    }
}
