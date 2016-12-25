using System;
using System.Collections.Generic;
using System.Linq;
using SystemInterfaces;
using Utilities;
using ViewModel.Interfaces;

namespace RevolutionEntities.ViewModels
{
    
    public class ViewEventSubscription<TViewModel, TEvent>: ViewModelEventSubscription<TViewModel, TEvent>, IViewModelEventSubscription<IViewModel, IEvent> where TViewModel : IViewModel where TEvent : IEvent
    {
        public ViewEventSubscription(int processId, Func<TEvent, bool> eventPredicate, IEnumerable<Func<TViewModel, TEvent, bool>> actionPredicate, Action<TViewModel, TEvent> action)
           : base(processId,eventPredicate,actionPredicate,action)
        {
           Action = (Action<IViewModel, IEvent>)base.Action.Convert(typeof(IViewModel), typeof(IEvent));
           EventPredicate = (Func<IEvent, bool>)base.EventPredicate.Convert(typeof(IEvent), typeof(bool));
           ActionPredicate = base.ActionPredicate.Select(x => (Func<IViewModel, IEvent, bool>)x.Convert(typeof(IViewModel), typeof(IEvent), typeof(bool))).ToList();
        }

        public new Action<IViewModel, IEvent> Action { get; } 
        public new Func<IEvent, bool> EventPredicate { get; }
        public new IEnumerable<Func<IViewModel, IEvent, bool>> ActionPredicate { get; }
    }
}
