using System;
using System.Collections.Generic;
using SystemInterfaces;
using ViewModel.Interfaces;

namespace RevolutionEntities.ViewModels
{
    public class ViewModelEventCommand<TViewModel,TEvent>:EventPublication<TViewModel,TEvent>, IViewModelEventCommand<TViewModel, IEvent> where TViewModel : IViewModel where TEvent : IEvent
    {
        public ViewModelEventCommand(string key, Func<TViewModel, IObservable<bool>> commandPredicate, Func<TViewModel, IObservable<dynamic>> subject, IEnumerable<Func<TViewModel, bool>> subjectPredicate, Func<TViewModel, IViewEventPublicationParameter> messageData) : base(key, subject, subjectPredicate, messageData)
        {
            CommandPredicate = commandPredicate;
        }


        public Func<TViewModel, IObservable<bool>> CommandPredicate { get; }
    }
}