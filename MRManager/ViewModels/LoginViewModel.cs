using System.Collections.Generic;
using SystemInterfaces;
using Core.Common.UI;
using EF.Entities;
using Interfaces;
using Reactive.Bindings;
using ValidationSets;

namespace ViewModels
{
    public class LoginViewModel : ObservableViewModel<IPersons>
    {
        public LoginViewModel(List<IEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IEventPublication<IViewModel, IEvent>> eventPublications, List<IEventCommand<IViewModel, IEvent>> commandInfo, ISystemProcess process) : base(new EntityValidator<IPersons>(), eventSubscriptions, eventPublications,commandInfo, process)
        {
            
            CurrentEntity = new ReactiveProperty<IPersons>(new Persons());
          
        }
    }
}
