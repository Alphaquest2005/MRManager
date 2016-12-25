using System;
using System.Collections.Generic;

namespace SystemInterfaces
{
    public interface IEventSubscription<in TEvent> where TEvent : IEvent
    {
        int ProcessId { get; }
        Type EventType { get; }
        Func<TEvent, bool> EventPredicate { get; }
    }


}
