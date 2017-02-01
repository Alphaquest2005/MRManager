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

    [Export]
    public class PatientVisitViewModel : DynamicViewModel<ObservableListViewModel<IPatientVisitInfo>>, IPatientVisitViewModel
    {
        private ObservableBindingList<IPatientVisitInfo> _changeTrackingList = new ObservableBindingList<IPatientVisitInfo>();
        private ObservableList<IPatientVisitInfo> _entitySet;
       

        public PatientVisitViewModel(ISystemProcess process,  List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications, List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo, Type orientation) : base(new ObservableListViewModel<IPatientVisitInfo>(eventSubscriptions, eventPublications, commandInfo, process, orientation))
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            this.WireEvents();
            _entitySet = this.ViewModel.EntitySet;

            Instance.ViewModel.WhenAnyValue(x => x.EntitySet).Subscribe(x => addNewRow(x));
        }
        private void addNewRow(ObservableList<IPatientVisitInfo> observableList)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                EntitySet.Clear();
                if(!observableList.Any()) return;
                var res = observableList.ToList();
                res.Add(new PatientVisitInfo() { Purpose = "Create New..." });
                
                EntitySet.AddRange(res);
                EntitySet.Reset();
                CurrentEntity.Value = EntitySet.FirstOrDefault();
            });
        }


        public ReactiveProperty<IProcessStateList<IPatientVisitInfo>> State => this.ViewModel.State;


        ReactiveProperty<IProcessState<IPatientVisitInfo>> IEntityViewModel<IPatientVisitInfo>.State => new ReactiveProperty<IProcessState<IPatientVisitInfo>>(this.ViewModel.State.Value);
        public ReactiveProperty<IPatientVisitInfo> CurrentEntity => this.ViewModel.CurrentEntity;

        public ObservableDictionary<string, dynamic> ChangeTracking => this.ViewModel.ChangeTracking;

        public ObservableList<IPatientVisitInfo> EntitySet
        {
            get { return _entitySet; }
            set { _entitySet = value; }
        }

    
        public ObservableList<IPatientVisitInfo> SelectedEntities => this.ViewModel.SelectedEntities;


        public ObservableBindingList<IPatientVisitInfo> ChangeTrackingList
        {
            get { return _changeTrackingList; }
            set { _changeTrackingList = value; }
        }

        public IPatientInfo CurrentPatient { get; set; }
    }
}
