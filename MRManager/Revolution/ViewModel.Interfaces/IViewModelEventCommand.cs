using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;

namespace ViewModel.Interfaces
{
    [InheritedExport]
    public interface IViewModelEventCommand<in TViewModel, in TEvent> : IViewModelEventPublication<TViewModel, TEvent> where TViewModel : IViewModel where TEvent : IEvent
    {
        string CommandName { get; }
        Func<TViewModel, IObservable<bool>> CommandPredicate { get; }

    }
}
