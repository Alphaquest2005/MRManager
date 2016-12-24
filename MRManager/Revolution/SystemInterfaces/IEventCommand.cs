using System;

namespace SystemInterfaces
{
    public interface IEventCommand<in TViewModel, in TEvent>:IEventPublication<TViewModel,TEvent> where TViewModel : IViewModel where TEvent : IEvent
    {
        string CommandName { get; }
        Func<TViewModel,IObservable<bool>> CommandPredicate { get; }

    }
}