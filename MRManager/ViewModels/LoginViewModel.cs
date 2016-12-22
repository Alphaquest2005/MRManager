using System.Collections.Generic;
using SystemInterfaces;
using Interfaces;

namespace ViewModels
{
    public class LoginViewModel : ReadEntityViewModel<IPersons>
    {
        public LoginViewModel(List<IEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IEventPublication<IViewModel, IEvent>> eventPublications, ISystemProcess process) : base(eventSubscriptions, eventPublications, process)
        {

        }
    }
}
