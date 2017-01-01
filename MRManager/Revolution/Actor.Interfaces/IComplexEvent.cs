using System;
using System.Collections.Generic;
using SystemInterfaces;

namespace Actor.Interfaces
{
    public interface IComplexEvent
    {
        IList<IProcessExpectedEvent> Events { get; }
        int ProcessId { get; }
       

    }

    public interface IComplexEventParameters
    {
        IProcessActor Actor { get; }
        IList<IProcessSystemMessage> Messages { get; }
        dynamic Msg { get; }
    }


}