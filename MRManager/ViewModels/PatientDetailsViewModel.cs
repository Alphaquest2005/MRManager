using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;
using Core.Common.UI;
using Interfaces;
using JB.Collections.Reactive;
using Reactive.Bindings;
using ViewModel.Interfaces;
using ViewModelInterfaces;

namespace ViewModels
{
    [Export]
    public class PatientDetailsViewModel : DynamicViewModel<ObservableViewModel<IPatientDetailsInfo>>, IPatientDetailsViewModel
    {
        public PatientDetailsViewModel(ISystemProcess process, List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications, List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo, Type orientation) : base(new ObservableViewModel<IPatientDetailsInfo>(eventSubscriptions, eventPublications, commandInfo, process, orientation))
        {
            this.WireEvents();
        }


        
       public ReactiveProperty<IProcessState<IPatientDetailsInfo>> State => this.Instance.State;
        public ObservableDictionary<string, dynamic> ChangeTracking => this.Instance.ChangeTracking;

    }
}