using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;

namespace ViewModel.Interfaces
{
    
    public interface IViewModelEventPublication<in TViewModel, in TEvent> : IEventPublication where TViewModel : IViewModel where TEvent : IEvent
    {
        // int ProcessId { get; }
        Func<TViewModel, IObservable<dynamic>> Subject { get; }
        IEnumerable<Func<TViewModel, bool>> SubjectPredicate { get; }
        //Func<TEvent, bool> EventPredicate { get; }
        Func<TViewModel, IViewEventPublicationParameter> MessageData { get; }

    }

    
    public interface IViewModelEventCommand<in TViewModel, in TEvent> : IEventPublication where TViewModel : IViewModel where TEvent : IEvent
    {
        // int ProcessId { get; }
        Func<TViewModel, IObservable<dynamic>> Subject { get; }
        IEnumerable<Func<TViewModel, bool>> CommandPredicate { get; }
        //Func<TEvent, bool> EventPredicate { get; }
        Func<TViewModel, IViewEventCommandParameter> MessageData { get; }

    }
}
