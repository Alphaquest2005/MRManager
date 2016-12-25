using System;
using System.Collections.Generic;
using SystemInterfaces;

namespace RevolutionEntities.ViewModels
{
    public class EventPublication<TViewModel, TEvent>: IEventPublication<TViewModel, TEvent> where TViewModel : IViewModel where TEvent : IEvent
    {
        public EventPublication(Func<TViewModel, IObservable<dynamic>> subject, IEnumerable<Func<TViewModel, bool>> subjectPredicate, IEnumerable<Func<TViewModel, dynamic>> messageData)
        {
            EventType = typeof(TEvent);
            Subject = subject;
            SubjectPredicate = subjectPredicate;
            MessageData = messageData;
            ViewModelType = typeof(TViewModel);
        }

        public Type EventType { get; }
        public Func<TViewModel, IObservable<dynamic>> Subject { get; }
        public IEnumerable<Func<TViewModel, bool>> SubjectPredicate { get; }
        public IEnumerable<Func<TViewModel, dynamic>> MessageData { get; }

        Type ViewModelType { get; }
    }
}