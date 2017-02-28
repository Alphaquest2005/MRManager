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
        public PatientSyntomViewModel(ISystemProcess process, IViewInfo viewInfo, List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications, List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo, Type orientation) : base(new ObservableListViewModel<IPatientSyntomInfo>(viewInfo,eventSubscriptions, eventPublications, commandInfo, process, orientation))
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            this.WireEvents();
            
            Instance.ViewModel.WhenAnyValue(x => x.EntitySet).Subscribe(x => insertNewRow());
            //this.WhenAnyValue(x => x.CurrentPatientVisit.Value).Subscribe(x => );

        }

        private void insertNewRow()
        {
                if (this.ViewModel.EntitySet.FirstOrDefault(x => x.Id == 0) != null) return;
                if (CurrentPatientVisit != null &&
                    (CurrentPatientVisit.Value?.Id != 0 &&
                     CurrentPatientVisit.Value?.DateOfVisit.Date == DateTime.Today.Date))
                {
                
                    this.ViewModel.EntitySet.Add(new PatientSyntomInfo() {SyntomName = "Create New..."});
                    
                }

                OnPropertyChanged(nameof(EntitySet));
        }

       
        public ReactiveProperty<IProcessStateList<IPatientSyntomInfo>> State => this.ViewModel.State;


        ReactiveProperty<IProcessState<IPatientSyntomInfo>> IEntityViewModel<IPatientSyntomInfo>.State => new ReactiveProperty<IProcessState<IPatientSyntomInfo>>(this.ViewModel.State.Value);
        public ReactiveProperty<IPatientSyntomInfo> CurrentEntity => this.ViewModel.CurrentEntity;

        public ObservableDictionary<string, dynamic> ChangeTracking => this.ViewModel.ChangeTracking;

        public ObservableList<IPatientSyntomInfo> EntitySet => this.ViewModel.EntitySet;

    
        public ObservableList<IPatientSyntomInfo> SelectedEntities => this.ViewModel.SelectedEntities;

        

        public ReactiveProperty<IPatientVisitInfo> CurrentPatientVisit { get; } = new ReactiveProperty<IPatientVisitInfo>(new PatientVisitInfo());
    }
}
