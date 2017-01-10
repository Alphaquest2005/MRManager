using System.Diagnostics.Contracts;
using SystemInterfaces;
using Actor.Interfaces;
using CommonMessages;


namespace EventMessages
{

    public class ComplexEventActionTimedOut : ProcessSystemMessage, IComplexEventActionTimedOut
    {
    
        public IComplexEventAction Action { get; }
        
        public ComplexEventActionTimedOut(IComplexEventAction action, IProcessStateInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
            Contract.Requires(action != null);
            Action = action;
           
        }
    }
}
