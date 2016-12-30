using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;

namespace ViewModel.Interfaces
{
    [InheritedExport]
    public interface IViewModelInfo
    {
        int ProcessId { get; }
        List<IViewModelEventSubscription<IViewModel, IEvent>> Subscriptions { get; }

        List<IViewModelEventPublication<IViewModel, IEvent>> Publications { get; }

        List<IViewModelEventCommand<IViewModel, IEvent>> Commands { get; }
        Type ViewModelType { get; }
        Type Orientation { get; }

    }
}
