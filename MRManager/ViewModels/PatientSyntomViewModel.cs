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

    [Export(typeof(IPatientSyntomViewModel))]
    public class PatientSyntomViewModel : DynamicViewModel<ObservableListViewModel<IPatientSyntomInfo>>, IPatientSyntomViewModel
    {
        
       
        [ImportingConstructor]
        public PatientSyntomViewModel(ISystemProcess process, IViewInfo viewInfo, List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications, List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo, Type orientation, int priority) : base(new ObservableListViewModel<IPatientSyntomInfo>(viewInfo,eventSubscriptions, eventPublications, commandInfo, process, orientation, priority))
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            this.WireEvents();
            
          
        }


        IEntityListViewModel<IPatientSyntomInfo> IEntityListViewModel<IPatientSyntomInfo>.Instance => (IEntityListViewModel<IPatientSyntomInfo>) PatientSyntomViewModel.Instance;
        public ReactiveProperty<IProcessStateList<IPatientSyntomInfo>> State => this.ViewModel.State;


        ReactiveProperty<IProcessState<IPatientSyntomInfo>> IEntityViewModel<IPatientSyntomInfo>.State => new ReactiveProperty<IProcessState<IPatientSyntomInfo>>(this.ViewModel.State.Value);
        public ReactiveProperty<IPatientSyntomInfo> CurrentEntity => this.ViewModel.CurrentEntity;

        public ObservableDictionary<string, dynamic> ChangeTracking => this.ViewModel.ChangeTracking;

        public ReactiveProperty<ObservableList<IPatientSyntomInfo>> EntitySet => this.ViewModel.EntitySet;

    
        public ReactiveProperty<ObservableList<IPatientSyntomInfo>> SelectedEntities => this.ViewModel.SelectedEntities;

        

        public ReactiveProperty<IPatientVisitInfo> CurrentPatientVisit { get; } = new ReactiveProperty<IPatientVisitInfo>(new PatientVisitInfo());
    }
}
