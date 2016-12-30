using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;
using CommonMessages;
using DataInterfaces;
using Reactive.Bindings;

namespace ViewModel.Interfaces
{
    [InheritedExport]
    public interface IViewModel
    {
        string Name { get; }
        string Symbol { get; }
        string Description { get; }
        ISystemProcess Process { get; }
        ISourceMessage SourceMessage { get; }

        List<IViewModelEventSubscription<IViewModel, IEvent>> EventSubscriptions { get; }
        List<IViewModelEventPublication<IViewModel, IEvent>> EventPublications { get; }
        Dictionary<string, dynamic> Commands { get; }
        List<IViewModelEventCommand<IViewModel, IEvent>> CommandInfo { get; }

        Type Orientation { get; }
        Type ViewModelType { get; }
    }
}
