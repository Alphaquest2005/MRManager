﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Dynamic;
using System.Linq;
using System.Reactive;
using System.Windows;
using System.Windows.Controls;
using SystemInterfaces;
using Common.Dynamic;
using Core.Common.UI;
using EF.Entities;
using FluentValidation;
using Interfaces;
using JB.Collections.Reactive;
using PrintUtilities;
using Reactive.Bindings;
using ReactiveUI;
using RevolutionEntities.Process;
using Utilities;
using ValidationSets;
using ViewModel.Interfaces;
using ViewModelInterfaces;
using ReactiveCommand = ReactiveUI.ReactiveCommand;

namespace ViewModels
{

    [Export(typeof (IMedicalReportViewModel))]
    public class MedicalReportViewModel : DynamicViewModel<ObservableListViewModel<IPatientInfo>>,IMedicalReportViewModel
    {

        [ImportingConstructor]
        public MedicalReportViewModel(ISystemProcess process, IViewInfo viewInfo, List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications, List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo, Type orientation, int priority)
            : base(
                new ObservableListViewModel<IPatientInfo>(viewInfo, eventSubscriptions, eventPublications, commandInfo, process,
                    orientation, priority))
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            this.WireEvents();
           PrintGrid = ReactiveCommand.Create<Grid>(param =>
           {
               WPF2PDF.CreateAndOpenPDF(ref param, "Medical Report" );
               //FrameworkElement rpt = (FrameworkElement)param;
               //PrintClass.Print(ref rpt);
           });


        }
        

        IEntityListViewModel<IPatientInfo> IEntityListViewModel<IPatientInfo>.Instance => (IEntityListViewModel<IPatientInfo>) MedicalReportViewModel.Instance;

        public ReactiveProperty<IProcessStateList<IPatientInfo>> State => this.ViewModel.State;


        ReactiveProperty<IProcessState<IPatientInfo>> IEntityViewModel<IPatientInfo>.State
            => new ReactiveProperty<IProcessState<IPatientInfo>>(this.ViewModel.State.Value);

        public ReactiveProperty<IPatientInfo> CurrentEntity => this.ViewModel.CurrentEntity;

        public ObservableDictionary<string, dynamic> ChangeTracking => this.ViewModel.ChangeTracking;

        public ReactiveProperty<ObservableList<IPatientInfo>> EntitySet => this.ViewModel.EntitySet;


        public ReactiveProperty<ObservableList<IPatientInfo>> SelectedEntities => this.ViewModel.SelectedEntities;


        //private static IPatientHistoryInfo patientHistory = new PatientHistoryInfo() {PatientDetails = new PatientDetailsInfo() {Name = "Shit test"} };
        //public IPatientHistoryInfo PatientHistory
        //{
        //    get { return patientHistory; }
        //    set
        //    {
        //        patientHistory = value;
        //        OnPropertyChanged();
        //    }
        //}

        public ReactiveProperty<IPatientDetailsInfo> PatientDetails { get; } = new ReactiveProperty<IPatientDetailsInfo>();
        public ReactiveCollection<IPatientVisitInfo> PatientVisits { get; } = new ReactiveCollection<IPatientVisitInfo>();
        private ReactiveCollection<ISyntomInfo> _synptoms = new ReactiveCollection<ISyntomInfo>();

        public ReactiveCollection<ISyntomInfo> Synptoms
        {
            get { return _synptoms; }
            set
            {
                _synptoms = value;
                OnPropertyChanged();
            }
        }

       
        public ReactiveCommand<Grid, Unit> PrintGrid { get; }
    }

}
