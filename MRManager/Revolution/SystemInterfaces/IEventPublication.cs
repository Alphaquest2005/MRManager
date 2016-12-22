using System;
using System.Collections.Generic;

namespace SystemInterfaces
{
    public interface IEventPublication<in TViewModel, in TEvent> where TViewModel : IViewModel where TEvent : IEvent
    {
        int ProcessId { get; }
        Type EventType { get; }
        Func<TViewModel, IObservable<IObservable<dynamic>>> Subject { get; }
        IEnumerable<Func<TViewModel, IObservable<IObservable<dynamic>>, TEvent, bool>> SubjectPredicate { get; }
        Func<TEvent, bool> EventPredicate { get; }
        
        Type ViewModelType { get; }
    }
}