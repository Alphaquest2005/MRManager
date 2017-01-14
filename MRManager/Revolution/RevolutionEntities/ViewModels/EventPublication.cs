using System;
using System.Collections.Generic;
using SystemInterfaces;
using ViewModel.Interfaces;

namespace RevolutionEntities.ViewModels
{
    public class EventPublication<TViewModel, TEvent>: IViewModelEventPublication<TViewModel, TEvent> where TViewModel : IViewModel where TEvent : IEvent
    {
        public EventPublication(string key, Func<TViewModel, IObservable<dynamic>> subject, IEnumerable<Func<TViewModel, bool>> subjectPredicate, Func<TViewModel, IViewEventPublicationParameter> messageData)
        {
            EventType = typeof(TEvent);
            Subject = subject;
            SubjectPredicate = subjectPredicate;
            MessageData = messageData;
            Key = key;
            ViewModelType = typeof(TViewModel);
        }

        public string Key { get; }
        public Type EventType { get; }
        public Func<TViewModel, IObservable<dynamic>> Subject { get; }
        public IEnumerable<Func<TViewModel, bool>> SubjectPredicate { get; }
        public Func<TViewModel, IViewEventPublicationParameter> MessageData { get; }

        Type ViewModelType { get; }
    }

    public class EventCommand<TViewModel, TEvent> : IViewModelEventCommand<TViewModel, TEvent> where TViewModel : IViewModel where TEvent : IEvent
    {
        public EventCommand(string key, Func<TViewModel, IObservable<dynamic>> subject, IEnumerable<Func<TViewModel, bool>> commandPredicate, Func<TViewModel, IViewEventCommandParameter> messageData)
        {
            EventType = typeof(TEvent);
            Subject = subject;
            CommandPredicate = commandPredicate;
            MessageData = messageData;
            Key = key;
            ViewModelType = typeof(TViewModel);
        }

        public string Key { get; }
        public Type EventType { get; }
        public Func<TViewModel, IObservable<dynamic>> Subject { get; }
        public IEnumerable<Func<TViewModel, bool>> CommandPredicate { get; }
        public Func<TViewModel, IViewEventCommandParameter> MessageData { get; }

        Type ViewModelType { get; }
    }
}