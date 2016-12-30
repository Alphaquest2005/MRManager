using System;
using System.Collections.Generic;
using SystemInterfaces;
using Core.Common.UI;
using EF.Entities;
using FluentValidation;
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
        public LoginViewModel(ISystemProcess process, List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications, List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo, Type orientation) : base(new ObservableViewModel<UserSignIn>(eventSubscriptions, eventPublications, commandInfo, process, orientation))
        {
            CurrentEntity = this.Instance.CurrentEntity;
            ChangeTracking = this.Instance.ChangeTracking;
            this.WireEvents();
        }

        public ReactiveProperty<UserSignIn> CurrentEntity { get; }
        public ObservableDictionary<string, dynamic> ChangeTracking { get; }
    }
}
