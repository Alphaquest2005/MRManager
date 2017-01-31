﻿using System;
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

    [Export]
    public class InterviewListViewModel : DynamicViewModel<ObservableListViewModel<IInterviewInfo>>, IInterviewListViewModel
    {
        private ReactiveProperty<IPatientSyntomInfo> _currentPatientSyntom = new ReactiveProperty<IPatientSyntomInfo>();
        private ReactiveProperty<ISyntomMedicalSystemInfo> _currentMedicalSystem = new ReactiveProperty<ISyntomMedicalSystemInfo>();
        private ReactiveProperty<ISyntomMedicalSystemInfo> _currentSystem= new ReactiveProperty<ISyntomMedicalSystemInfo>();

        public InterviewListViewModel(ISystemProcess process,  List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications, List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo, Type orientation) : base(new ObservableListViewModel<IInterviewInfo>(eventSubscriptions, eventPublications, commandInfo, process, orientation))
        {
           this.WireEvents();
            CurrentMedicalSystem.WhenAnyValue(x => x.Value).Subscribe(x => systemChange(x));
        }

        private void systemChange(ISyntomMedicalSystemInfo syntomMedicalSystemInfo)
        {
            if (CurrentMedicalSystem.Value == null) return;
             Application.Current.Dispatcher.Invoke(() =>
                {
                    EntitySet.Clear();
                    EntitySet.AddRange(CurrentMedicalSystem.Value.Interviews);
                    EntitySet.Reset();
                });
        }


        public ReactiveProperty<IProcessStateList<IInterviewInfo>> State => this.ViewModel.State;


        ReactiveProperty<IProcessState<IInterviewInfo>> IEntityViewModel<IInterviewInfo>.State => new ReactiveProperty<IProcessState<IInterviewInfo>>(this.ViewModel.State.Value);
        public ReactiveProperty<IInterviewInfo> CurrentEntity => this.ViewModel.CurrentEntity;

        public ObservableDictionary<string, dynamic> ChangeTracking => this.ViewModel.ChangeTracking;
        public ObservableList<IInterviewInfo> EntitySet => this.ViewModel.EntitySet;
        public ObservableList<IInterviewInfo> SelectedEntities => this.ViewModel.SelectedEntities;
    


        public string Field { get; set; }
        public string Value { get; set; }

        public ReactiveProperty<IPatientSyntomInfo> CurrentPatientSyntom
        {
            get { return _currentPatientSyntom; }
            set
            {
                _currentPatientSyntom = value;
                
            }
        }

        public ReactiveProperty<ISyntomMedicalSystemInfo> CurrentMedicalSystem
        {
            get { return _currentMedicalSystem; }
            set
            {
                _currentMedicalSystem = value;
               
            }
        }

        public ReactiveProperty<ISyntomMedicalSystemInfo> CurrentSystem
        {
            get { return _currentSystem; }
            set { _currentSystem = value; }
        }
    }
}
