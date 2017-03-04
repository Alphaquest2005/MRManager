using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using SystemInterfaces;
using Core.Common.UI;
using EF.Entities;
using FluentValidation;
using Interfaces;
using JB.Collections.Reactive;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using ReactiveUI;
using RevolutionEntities.Process;
using ValidationSets;
using ViewModel.Interfaces;
using ViewModelInterfaces;

namespace ViewModels
{

    [Export(typeof(IQuestionaireViewModel))]
    public class QuestionaireViewModel : DynamicViewModel<ObservableListViewModel<IResponseOptionInfo>>, IQuestionaireViewModel
    {
        IEntityListViewModel<IResponseOptionInfo> IEntityListViewModel<IResponseOptionInfo>.Instance => (IEntityListViewModel<IResponseOptionInfo>) QuestionaireViewModel.Instance;

        [ImportingConstructor]
        public QuestionaireViewModel(ISystemProcess process, IViewInfo viewInfo, List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications, List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo, Type orientation, int priority)
            : base(
                new ObservableListViewModel<IResponseOptionInfo>(viewInfo, eventSubscriptions, eventPublications, commandInfo,
                    process, orientation, priority))
        {
            this.WireEvents();
           
           
        }

        public ObservableList<IQuestionResponseOptionInfo> Questions
        {
            get { return _questions; }
            set
            {
                _questions = value; 
                OnPropertyChanged();
            }
        }

        private IPatientVisitInfo _currentPatientVisit;
        public IPatientVisitInfo CurrentPatientVisit
        {
            get { return _currentPatientVisit; }
            set
            {
                _currentPatientVisit = value;
                OnPropertyChanged();
            }
        }
        private IPatientSyntomInfo _currentPatientSyntom;
        public IPatientSyntomInfo CurrentPatientSyntom
        {
            get { return _currentPatientSyntom; }
            set
            {
                _currentPatientSyntom = value;
                OnPropertyChanged();
            }
        }

        public ReactiveProperty<IQuestionResponseOptionInfo> CurrentQuestion { get; } = new ReactiveProperty<IQuestionResponseOptionInfo>();

        
        
        private ObservableList<IQuestionResponseOptionInfo> _questions = new ObservableList<IQuestionResponseOptionInfo>();


        
        public ReactiveProperty<IProcessStateList<IResponseOptionInfo>> State => this.ViewModel.State;


        ReactiveProperty<IProcessState<IResponseOptionInfo>> IEntityViewModel<IResponseOptionInfo>.State => new ReactiveProperty<IProcessState<IResponseOptionInfo>>(this.ViewModel.State.Value);
        public ReactiveProperty<IResponseOptionInfo> CurrentEntity => this.ViewModel.CurrentEntity;

        public ObservableDictionary<string, dynamic> ChangeTracking => this.ViewModel.ChangeTracking;
        public ReactiveProperty<ObservableList<IResponseOptionInfo>> EntitySet => this.ViewModel.EntitySet;
        public ReactiveProperty<ObservableList<IResponseOptionInfo>> SelectedEntities => this.ViewModel.SelectedEntities;

        public ReactiveProperty<IQuestionResponseTypes> DataType { get; } = new ReactiveProperty<IQuestionResponseTypes>();
        
    }
}
