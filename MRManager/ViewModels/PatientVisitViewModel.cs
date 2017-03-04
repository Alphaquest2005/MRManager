using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Utilities;
using ValidationSets;
using ViewModel.Interfaces;
using ViewModelInterfaces;

namespace ViewModels
{

    [Export(typeof(IPatientVisitViewModel))]
    public class PatientVisitViewModel : DynamicViewModel<ObservableListViewModel<IPatientVisitInfo>>, IPatientVisitViewModel
    {
        
       

        [ImportingConstructor]
        public PatientVisitViewModel(ISystemProcess process, IViewInfo viewInfo, List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications, List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo, Type orientation, int priority) : base(new ObservableListViewModel<IPatientVisitInfo>(viewInfo,eventSubscriptions, eventPublications, commandInfo, process, orientation, priority))
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            this.WireEvents();
            
            
        }


        IEntityListViewModel<IPatientVisitInfo> IEntityListViewModel<IPatientVisitInfo>.Instance => (IEntityListViewModel<IPatientVisitInfo>) PatientVisitViewModel.Instance;
        public ReactiveProperty<IProcessStateList<IPatientVisitInfo>> State => this.ViewModel.State;


        ReactiveProperty<IProcessState<IPatientVisitInfo>> IEntityViewModel<IPatientVisitInfo>.State => new ReactiveProperty<IProcessState<IPatientVisitInfo>>(this.ViewModel.State.Value, ReactivePropertyMode.DistinctUntilChanged);
        public ReactiveProperty<IPatientVisitInfo> CurrentEntity => this.ViewModel.CurrentEntity;

        public ObservableDictionary<string, dynamic> ChangeTracking => this.ViewModel.ChangeTracking;

        public ReactiveProperty<ObservableList<IPatientVisitInfo>> EntitySet => this.ViewModel.EntitySet;
        

    
        public ReactiveProperty<ObservableList<IPatientVisitInfo>> SelectedEntities => this.ViewModel.SelectedEntities;
        public ObservableBindingList<IPatientVisitInfo> ChangeTrackingList => this.ViewModel.ChangeTrackingList;


        public IPatientInfo CurrentPatient { get; set; }
    }
}
