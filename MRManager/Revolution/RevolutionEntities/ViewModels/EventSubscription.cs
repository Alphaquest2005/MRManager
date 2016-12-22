using System;
using System.Collections.Generic;
using SystemInterfaces;

namespace RevolutionEntities.ViewModels
{
    public class EventSubscription<TViewModel, TEvent> :IEventSubscription<TViewModel, TEvent> where TViewModel : IViewModel where TEvent : IEvent
    {
        protected EventSubscription(int processId, Func<TEvent, bool> eventPredicate, IEnumerable<Func<TViewModel,TEvent, bool>> actionPredicate, Action<TViewModel, TEvent> action)
        {
            ProcessId = processId;
            EventPredicate = eventPredicate;
            ActionPredicate = actionPredicate;
            Action = action;
        }

        public Type EventType { get; } = typeof (TEvent);
        public Action<TViewModel, TEvent> Action { get; }
        public Func<TEvent, bool> EventPredicate { get; }
        public IEnumerable<Func<TViewModel, TEvent, bool>> ActionPredicate { get; }
        public Type ViewModelType { get; } = typeof (TViewModel);
        public int ProcessId { get; }
    }
}