using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
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

    [Export]
    public class PatientSummaryListViewModel : DynamicViewModel<ObservableListViewModel<IPatientInfo>>, IPatientSummaryListViewModel
    {
        public PatientSummaryListViewModel(ISystemProcess process,  List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications, List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo, Type orientation) : base(new ObservableListViewModel<IPatientInfo>(eventSubscriptions, eventPublications, commandInfo, process, orientation))
        {
           this.WireEvents();
        }

        
        public ReactiveProperty<IProcessStateList<IPatientInfo>> State => this.ViewModel.State;


        ReactiveProperty<IProcessState<IPatientInfo>> IEntityViewModel<IPatientInfo>.State => new ReactiveProperty<IProcessState<IPatientInfo>>(this.ViewModel.State.Value);
        public ReactiveProperty<IPatientInfo> CurrentEntity => this.ViewModel.CurrentEntity;

        public ObservableDictionary<string, dynamic> ChangeTracking => this.ViewModel.ChangeTracking;
        public ObservableList<IPatientInfo> EntitySet => this.ViewModel.EntitySet;
        public ObservableList<IPatientInfo> SelectedEntities => this.ViewModel.SelectedEntities;
        


        public string Field { get; set; }
        public string Value { get; set; }

        
    }
}
