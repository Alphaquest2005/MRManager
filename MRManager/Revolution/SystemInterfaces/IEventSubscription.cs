using System;
using System.Collections.Generic;

namespace SystemInterfaces
{
    public interface IEventSubscription<in TViewModel, in TEvent> where TViewModel:IViewModel where TEvent:IEvent
    {
        int ProcessId { get; }
        Type EventType { get; }
        Action<TViewModel, TEvent> Action { get; }
        Func<TEvent, bool> EventPredicate { get; }
        IEnumerable<Func<TViewModel, TEvent, bool>> ActionPredicate { get; }
        Type ViewModelType { get; }
    }
}
