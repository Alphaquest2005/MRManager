using System;
using System.Collections.Generic;
using SystemInterfaces;

namespace ViewModel.Interfaces
{
    public interface IViewModelInfo
    {
        int ProcessId { get; }
        List<IEventSubscription<IViewModel, IEvent>> ViewEventSubscriptions { get; }

        List<IEventPublication<IViewModel, IEvent>> ViewEventPublications { get; }
        Type ViewModelType { get; }
    }
}
