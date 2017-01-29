using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
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

    [Export]
    public class SigninViewModel : DynamicViewModel<ObservableViewModel<ISignInInfo>>, ISigninViewModel
    {
        public SigninViewModel(ISystemProcess process,  List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications, List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo, Type orientation) : base(new ObservableViewModel<ISignInInfo>(eventSubscriptions, eventPublications, commandInfo, process, orientation))
        {
            this.WireEvents();
        }

        public AbstractValidator<ISignInInfo> Validator => this.ViewModel.Validator;
        public ReactiveProperty<IProcessState<ISignInInfo>> State => this.ViewModel.State;
        public ObservableDictionary<string, dynamic> ChangeTracking => this.ViewModel.ChangeTracking;
    }
}
