using System.ComponentModel.Composition;
using System.Diagnostics.Contracts;
using SystemInterfaces;
using Actor.Interfaces;
using CommonMessages;

namespace EventMessages.Events
{
    [Export(typeof(IComplexEventActionTimedOut))]
    public class ComplexEventActionTimedOut : ProcessSystemMessage, IComplexEventActionTimedOut
    {
        public ComplexEventActionTimedOut() { }
        public IComplexEventAction Action { get; }
        
        public ComplexEventActionTimedOut(IComplexEventAction action, IStateEventInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
            Contract.Requires(action != null);
            Action = action;
           
        }
    }
}
