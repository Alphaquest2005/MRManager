using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using SystemInterfaces;
using Core.Common.UI;
using EF.Entities;
using FluentValidation;
using Interfaces;
using JB.Collections.Reactive;
using Reactive.Bindings;
using ReactiveUI;
using RevolutionEntities.Process;
using ValidationSets;
using ViewModel.Interfaces;
using ViewModelInterfaces;

namespace ViewModels
{

    [Export(typeof(IPatientSummaryListViewModel))]
    public class PatientSummaryListViewModel : DynamicViewModel<ObservableListViewModel<IPatientInfo>>, IPatientSummaryListViewModel
    {
        [ImportingConstructor]
        public PatientSummaryListViewModel(ISystemProcess process, IViewInfo viewInfo, List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications, List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo, Type orientation, int priority) : base(new ObservableListViewModel<IPatientInfo>(viewInfo,eventSubscriptions, eventPublications, commandInfo, process, orientation, priority))
        {
           this.WireEvents();
           
        }


        IEntityListViewModel<IPatientInfo> IEntityListViewModel<IPatientInfo>.Instance => (IEntityListViewModel<IPatientInfo>) PatientSummaryListViewModel.Instance;
        public ReactiveProperty<IProcessStateList<IPatientInfo>> State => Instance.ViewModel.State;


         ReactiveProperty<IProcessState<IPatientInfo>> IEntityViewModel<IPatientInfo>.State => new ReactiveProperty<IProcessState<IPatientInfo>>(Instance.ViewModel.State.Value);
        public ReactiveProperty<IPatientInfo> CurrentEntity => Instance.ViewModel.CurrentEntity;

        public ObservableDictionary<string, dynamic> ChangeTracking => Instance.ViewModel.ChangeTracking;
       

        public ReactiveProperty<ObservableList<IPatientInfo>> EntitySet => Instance.ViewModel.EntitySet;

        public ReactiveProperty<ObservableList<IPatientInfo>> SelectedEntities => Instance.ViewModel.SelectedEntities;
        public ObservableBindingList<IPatientInfo> ChangeTrackingList => Instance.ViewModel.ChangeTrackingList;


        public string Field { get; set; }
        public string Value { get; set; }

        
    }
}
