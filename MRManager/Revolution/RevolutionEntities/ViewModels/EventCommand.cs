using System;
using System.Collections.Generic;
using SystemInterfaces;
using ViewModel.Interfaces;

namespace RevolutionEntities.ViewModels
{
    public class ViewModelEventCommand<TViewModel,TEvent>:EventPublication<TViewModel,TEvent>, IViewModelEventCommand<TViewModel, IEvent> where TViewModel : IViewModel where TEvent : IEvent
    {
        public ViewModelEventCommand(string commandName, Func<TViewModel, IObservable<bool>> commandPredicate, Func<TViewModel, IObservable<dynamic>> subject, IEnumerable<Func<TViewModel, bool>> subjectPredicate, IEnumerable<Func<TViewModel, dynamic>> messageData) : base(subject, subjectPredicate, messageData)
        {
            CommandName = commandName;
            CommandPredicate = commandPredicate;
        }

        public string CommandName { get; }
        public Func<TViewModel, IObservable<bool>> CommandPredicate { get; }
    }
}