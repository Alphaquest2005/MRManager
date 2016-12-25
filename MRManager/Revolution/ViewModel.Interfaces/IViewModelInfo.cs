using System;
using System.Collections.Generic;
using SystemInterfaces;

namespace ViewModel.Interfaces
{
    public interface IViewModelInfo
    {
        int ProcessId { get; }
        List<IEventSubscription<IViewModel, IEvent>> Subscriptions { get; }

        List<IEventPublication<IViewModel, IEvent>> Publications { get; }

        List<IEventCommand<IViewModel, IEvent>> Commands { get; }
        Type ViewModelType { get; }
    }
}
