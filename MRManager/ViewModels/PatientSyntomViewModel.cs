﻿using System;
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
    public class PatientSyntomViewModel : DynamicViewModel<ObservableListViewModel<IPatientSyntomInfo>>, IPatientSyntomViewModel
    {
        private ObservableBindingList<IPatientSyntomInfo> _changeTrackingList = new ObservableBindingList<IPatientSyntomInfo>();
        private ObservableList<IPatientSyntomInfo> _entitySet;
        private IPatientVisitInfo _currentPatientVisit;


        public PatientSyntomViewModel(ISystemProcess process,  List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications, List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo, Type orientation) : base(new ObservableListViewModel<IPatientSyntomInfo>(eventSubscriptions, eventPublications, commandInfo, process, orientation))
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            this.WireEvents();
            _entitySet = this.ViewModel.EntitySet;
           
        }


       
        public ReactiveProperty<IProcessStateList<IPatientSyntomInfo>> State => this.ViewModel.State;


        ReactiveProperty<IProcessState<IPatientSyntomInfo>> IEntityViewModel<IPatientSyntomInfo>.State => new ReactiveProperty<IProcessState<IPatientSyntomInfo>>(this.ViewModel.State.Value);
        public ReactiveProperty<IPatientSyntomInfo> CurrentEntity => this.ViewModel.CurrentEntity;

        public ObservableDictionary<string, dynamic> ChangeTracking => this.ViewModel.ChangeTracking;

        public ObservableList<IPatientSyntomInfo> EntitySet
        {
            get { return _entitySet; }
            set { _entitySet = value; }
        }

    
        public ObservableList<IPatientSyntomInfo> SelectedEntities => this.ViewModel.SelectedEntities;


        public ObservableBindingList<IPatientSyntomInfo> ChangeTrackingList
        {
            get { return _changeTrackingList; }
            set { _changeTrackingList = value; }
        }

        public IPatientVisitInfo CurrentPatientVisit
        {
            get { return _currentPatientVisit; }
            set
            {
                _currentPatientVisit = value;
                Application.Current.Dispatcher.Invoke(() =>
                {
                    EntitySet.Clear();
                    EntitySet.AddRange(_currentPatientVisit.PatientSyntoms);
                    EntitySet.Reset();
                });
                
            }
        }
    }
}