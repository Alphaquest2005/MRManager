using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;

namespace ViewModel.Interfaces
{
    
    public interface IViewModelInfo
    {
        
        int ProcessId { get; }

        IViewInfo ViewInfo { get; }
        List<IViewModelEventSubscription<IViewModel, IEvent>> Subscriptions { get; }

        List<IViewModelEventPublication<IViewModel, IEvent>> Publications { get; }

        List<IViewModelEventCommand<IViewModel, IEvent>> Commands { get; }
        Type ViewModelType { get; }
        Type Orientation { get; }
        int Priority { get; }
        
    }

    public interface IViewInfo
    {
        string Name { get; }
        string Symbol { get; }
        string Description { get; }
    }
}
