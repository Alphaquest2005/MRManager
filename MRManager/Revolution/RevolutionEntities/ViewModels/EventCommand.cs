using System;
using System.Collections.Generic;
using SystemInterfaces;

namespace RevolutionEntities.ViewModels
{
    public class EventCommand<TViewModel,TEvent>:EventPublication<TViewModel,TEvent>, IEventCommand<TViewModel, IEvent> where TViewModel : IViewModel where TEvent : IEvent
    {
        public EventCommand(string commandName, Func<TViewModel, IObservable<bool>> commandPredicate, Func<TViewModel, IObservable<dynamic>> subject, IEnumerable<Func<TViewModel, IObservable<dynamic>, bool>> subjectPredicate, IEnumerable<Func<TViewModel, dynamic>> messageData) : base(subject, subjectPredicate, messageData)
        {
            CommandName = commandName;
            CommandPredicate = commandPredicate;
        }

        public string CommandName { get; }
        public Func<TViewModel, IObservable<bool>> CommandPredicate { get; }
    }
}