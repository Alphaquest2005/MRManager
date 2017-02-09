using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reactive.Linq;
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

    [Export(typeof(IInterviewListViewModel))]
    public class InterviewListViewModel : DynamicViewModel<ObservableListViewModel<IInterviewInfo>>, IInterviewListViewModel
    {
        private ReactiveProperty<IPatientSyntomInfo> _currentPatientSyntom = new ReactiveProperty<IPatientSyntomInfo>();
        private ReactiveProperty<ISyntomMedicalSystemInfo> _currentMedicalSystem = new ReactiveProperty<ISyntomMedicalSystemInfo>();
        private ReactiveProperty<ISyntomMedicalSystemInfo> _currentSystem= new ReactiveProperty<ISyntomMedicalSystemInfo>();
        private ObservableList<ISyntomMedicalSystemInfo> _systems = new ObservableList<ISyntomMedicalSystemInfo>();

        [ImportingConstructor]
        public InterviewListViewModel(ISystemProcess process,  List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications, List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo, Type orientation) : base(new ObservableListViewModel<IInterviewInfo>(eventSubscriptions, eventPublications, commandInfo, process, orientation))
        {
           this.WireEvents();
            CurrentMedicalSystem.WhenAnyValue(x => x.Value).DistinctUntilChanged().Subscribe(x => systemChange(x));
            
            this.WhenAnyValue(x => x.CurrentPatientSyntom.Value).Subscribe(x => addSystems(x));
           
        }

        

        private void addSystems(IPatientSyntomInfo patientSyntom)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Systems.Clear();
                if (patientSyntom == null || patientSyntom.Id == 0)
                {
                    CurrentMedicalSystem.Value = null;
                    return;
                }
                    
                var res = new List<ISyntomMedicalSystemInfo>();
                if (patientSyntom?.Systems != null) res = patientSyntom.Systems.ToList();
                res.Add(new SyntomMedicalSystemInfo() { System = "Create New..." });
                Systems.AddRange(res);
                Systems.Reset();
                if(CurrentMedicalSystem.Value != Systems.FirstOrDefault()) CurrentMedicalSystem.Value = Systems.FirstOrDefault();
            });
        }

        private void systemChange(ISyntomMedicalSystemInfo syntomMedicalSystemInfo)
        {
           
             Application.Current.Dispatcher.Invoke(() =>
                {
                    EntitySet.Clear();
                    if (CurrentMedicalSystem.Value == null)
                    {
                        CurrentEntity.Value = null;
                        return;
                    }
                    
                    var res = new List<IInterviewInfo>();
                    if(CurrentMedicalSystem.Value.Interviews != null) res = CurrentMedicalSystem.Value.Interviews.ToList();
                    res.Add(new InterviewInfo() { Interview = "Create New..." });
                    EntitySet.AddRange(res);
                    EntitySet.Reset();
                    CurrentEntity.Value = EntitySet.FirstOrDefault();
                    
                });
        }


        public ReactiveProperty<IProcessStateList<IInterviewInfo>> State => this.ViewModel.State;


        ReactiveProperty<IProcessState<IInterviewInfo>> IEntityViewModel<IInterviewInfo>.State => new ReactiveProperty<IProcessState<IInterviewInfo>>(this.ViewModel.State.Value);
        public ReactiveProperty<IInterviewInfo> CurrentEntity => this.ViewModel.CurrentEntity;

        public ObservableDictionary<string, dynamic> ChangeTracking => this.ViewModel.ChangeTracking;
        public ObservableList<IInterviewInfo> EntitySet => this.ViewModel.EntitySet;
        public ObservableList<IInterviewInfo> SelectedEntities => this.ViewModel.SelectedEntities;
        public ObservableBindingList<IInterviewInfo> ChangeTrackingList => this.ViewModel.ChangeTrackingList;

        public ObservableList<ISyntomMedicalSystemInfo> Systems
        {
            get { return _systems; }
            set { _systems = value; }
        }

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

      
    }
}
