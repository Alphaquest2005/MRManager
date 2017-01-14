using System;
using System.Collections.Generic;
using SystemInterfaces;
using ViewModel.Interfaces;

namespace RevolutionEntities.ViewModels
{
    public class ViewModelEventCommand<TViewModel,TEvent>:EventCommand<TViewModel,TEvent>, IViewModelEventCommand<TViewModel, IEvent> where TViewModel : IViewModel where TEvent : IEvent
    {
        public ViewModelEventCommand(string key, Func<TViewModel, IObservable<dynamic>> subject, IEnumerable<Func<TViewModel, bool>> commandPredicate, Func<TViewModel, IViewEventCommandParameter> messageData) : base(key, subject, commandPredicate, messageData)
        {
            
        }


        
    }
}