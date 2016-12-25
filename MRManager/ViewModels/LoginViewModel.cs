using System.Collections.Generic;
using SystemInterfaces;
using Core.Common.UI;
using EF.Entities;
using Interfaces;
using JB.Collections.Reactive;
using Reactive.Bindings;
using ReactiveUI;
using ValidationSets;
using ViewModel.Interfaces;
using ViewModelInterfaces;

namespace ViewModels
{
    public class LoginViewModel : DynamicViewModel<ObservableViewModel<UserSignIn>>, IEntityViewModel<UserSignIn>
    {


        public LoginViewModel(List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions,
            List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications,
            List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo, ISystemProcess process)
            : base(
                new ObservableViewModel<UserSignIn>(new EntityValidator<UserSignIn>(), eventSubscriptions,
                    eventPublications, commandInfo, process))
        {

        }

        public ReactiveProperty<UserSignIn> CurrentEntity => this.CurrentEntity;
        public ObservableDictionary<string, dynamic> ChangeTracking => this.ChangeTracking;

        private string _status;

        public string Status
        {
            get { return _status; }
            set { ((ReactiveObject) this.Instance).RaiseAndSetIfChanged(ref _status, value); }
        }
    }
}
