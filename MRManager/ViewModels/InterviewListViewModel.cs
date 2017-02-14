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
        private ReactiveProperty<IPatientSyntomInfo> _currentPatientSyntom = new ReactiveProperty<IPatientSyntomInfo>(new PatientSyntomInfo());
        private ReactiveProperty<ISyntomMedicalSystemInfo> _currentMedicalSystem = new ReactiveProperty<ISyntomMedicalSystemInfo>(new SyntomMedicalSystemInfo());
        private ReactiveProperty<IMedicalSystems> _selectedMedicalSystem = new ReactiveProperty<IMedicalSystems>(new MedicalSystems());
        private ReactiveProperty<ObservableList<ISyntomMedicalSystemInfo>> _systems = new ReactiveProperty<ObservableList<ISyntomMedicalSystemInfo>>(new ObservableBindingList<ISyntomMedicalSystemInfo>());

        [ImportingConstructor]
        public InterviewListViewModel(ISystemProcess process,  List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications, List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo, Type orientation) : base(new ObservableListViewModel<IInterviewInfo>(eventSubscriptions, eventPublications, commandInfo, process, orientation))
        {
           this.WireEvents();
           
            
            this.WhenAnyValue(x => x.Systems.Value).Subscribe(x => addSystems(x));
            this.WhenAnyValue(x => x.CurrentMedicalSystem.Value).Where(x => x != null).Subscribe(x => addNewRow(x.Interviews));
            
        }

        

        private void addSystems(ObservableList<ISyntomMedicalSystemInfo> observableList)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (Systems.Value.FirstOrDefault(x => x.Id == 0) != null) return;
                Systems.Value.Add(new SyntomMedicalSystemInfo() { System = "Create New..." });
                
            });
        }

        private void addNewRow(IList<IInterviewInfo> observableList)
        {
            if (observableList?.FirstOrDefault(x => x.Id == 0) != null) return;
            var res = observableList?.ToList()?? new List<IInterviewInfo>();
                res.Add(new InterviewInfo() { Interview = "Create New..." });

                this.ViewModel.EntitySet = new ObservableList<IInterviewInfo>(res);
                OnPropertyChanged(nameof(EntitySet));
        }


        public ReactiveProperty<IProcessStateList<IInterviewInfo>> State => this.ViewModel.State;


        ReactiveProperty<IProcessState<IInterviewInfo>> IEntityViewModel<IInterviewInfo>.State => new ReactiveProperty<IProcessState<IInterviewInfo>>(this.ViewModel.State.Value);
        public ReactiveProperty<IInterviewInfo> CurrentEntity => this.ViewModel.CurrentEntity;

        public ObservableDictionary<string, dynamic> ChangeTracking => this.ViewModel.ChangeTracking;
        public ObservableList<IInterviewInfo> EntitySet => this.ViewModel.EntitySet;
        public ObservableList<IInterviewInfo> SelectedEntities => this.ViewModel.SelectedEntities;
       

        public ReactiveProperty<ObservableList<ISyntomMedicalSystemInfo>> Systems
        {
            get { return _systems; }
            set
            {
                _systems = value;
            }
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

        public ReactiveProperty<IMedicalSystems> SelectedMedicalSystem
        {
            get { return _selectedMedicalSystem; }
            set { _selectedMedicalSystem = value; }
        }
    }
}
