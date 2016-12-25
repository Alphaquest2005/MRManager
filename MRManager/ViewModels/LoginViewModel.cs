using System.Collections.Generic;
using SystemInterfaces;
using Core.Common.UI;
using EF.Entities;
using Interfaces;
using Reactive.Bindings;
using ValidationSets;

namespace ViewModels
{
    public class LoginViewModel : DynamicViewModel<ObservableViewModel<UserSignIn>>
    {
        public LoginViewModel(List<IEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IEventPublication<IViewModel, IEvent>> eventPublications, List<IEventCommand<IViewModel, IEvent>> commandInfo, ISystemProcess process)
            :base(new ObservableViewModel<UserSignIn>(new EntityValidator<UserSignIn>(), eventSubscriptions, eventPublications, commandInfo, process))
        {
            
        }
    }
}
