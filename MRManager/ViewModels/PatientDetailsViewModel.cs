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
    [Export(typeof(IPatientDetailsViewModel))]
    public class PatientDetailsViewModel : DynamicViewModel<ObservableViewModel<IPatientDetailsInfo>>, IPatientDetailsViewModel
    {
        [ImportingConstructor]
        public PatientDetailsViewModel(ISystemProcess process, IViewInfo viewInfo, List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications, List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo, Type orientation) : base(new ObservableViewModel<IPatientDetailsInfo>(viewInfo, eventSubscriptions, eventPublications, commandInfo, process, orientation))
        {
            this.WireEvents();
        }


        
       public ReactiveProperty<IProcessState<IPatientDetailsInfo>> State => this.ViewModel.State;
        public ObservableDictionary<string, dynamic> ChangeTracking => this.ViewModel.ChangeTracking;

        public IPatientInfo CurrentPatient { get; set; }
    }
}