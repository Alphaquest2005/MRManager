using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;

namespace Actor.Interfaces
{
    public enum ActionTrigger
    {
        Partial,
        All,
        Any
    }
    
    public interface IComplexEventAction : IComplexEvent
    {
        Type ExpectedMessageType { get; }
        IProcessStateInfo ProcessInfo { get; }
        IProcessAction Action { get; }

        ActionTrigger ActionTrigger { get;}

    }
}
