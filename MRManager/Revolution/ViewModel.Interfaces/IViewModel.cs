using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;
using Reactive.Bindings;
using ReactiveUI;

namespace ViewModel.Interfaces
{
    [InheritedExport]
    public interface IViewModel:IProcessSource
    {
        string Name { get; }
        string Symbol { get; }
        string Description { get; }
        ISystemProcess Process { get; }
        
        List<IViewModelEventSubscription<IViewModel, IEvent>> EventSubscriptions { get; }
        List<IViewModelEventPublication<IViewModel, IEvent>> EventPublications { get; }
        Dictionary<string, ReactiveCommand<IViewModel, Unit>> Commands { get; }
        List<IViewModelEventCommand<IViewModel, IEvent>> CommandInfo { get; }

        Type Orientation { get; }
        Type ViewModelType { get; }
    }
}
