using System;
using System.Collections.Generic;
using SystemInterfaces;

namespace Actor.Interfaces
{
    public interface IEventAction
    {
        Action<IProcessActor, IProcessSystemMessage> Action { get; }
        IList<IProcessExpectedEvent> Events { get; }
        int ProcessId { get; }
        bool Raised { get; set; }
    }

    public interface IEventAction<TEvent> : IEventAction
    {
        //TEvent Event { get; }
    }
}
