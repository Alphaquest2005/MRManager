using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface IViewModel
    {
        string Name { get; }
        string Symbol { get; }
        string Description { get; }
        ISystemProcess Process { get; }
        List<IEventSubscription<IViewModel, IEvent>> EventSubscriptions { get; }
        List<IEventPublication<IViewModel, IEvent>> EventPublications { get; }
        List<IViewCommand<IViewModel,IEvent>> Commands { get; }
    }
}
