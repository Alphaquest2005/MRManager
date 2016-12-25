using System;
using System.Collections;
using System.Collections.Generic;

namespace SystemInterfaces
{
    public interface IEventPublication<in TViewModel, in TEvent> where TViewModel : IViewModel where TEvent : IEvent
    {
       // int ProcessId { get; }
        Type EventType { get; }
        Func<TViewModel, IObservable<dynamic>> Subject { get; }
        IEnumerable<Func<TViewModel, bool>> SubjectPredicate { get; }
        //Func<TEvent, bool> EventPredicate { get; }
        IEnumerable<Func<TViewModel, dynamic>> MessageData { get; }
       
    }

 }