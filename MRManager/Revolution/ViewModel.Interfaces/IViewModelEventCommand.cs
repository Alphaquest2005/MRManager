using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;

namespace ViewModel.Interfaces
{
    public interface IViewModelEventCommand<in TViewModel, in TEvent> : IViewModelEventPublication<TViewModel, TEvent> where TViewModel : IViewModel where TEvent : IEvent
    {
        string CommandName { get; }
        Func<TViewModel, IObservable<bool>> CommandPredicate { get; }

    }
}
