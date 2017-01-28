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
    public class QuestionaireViewModel : DynamicViewModel<ObservableListViewModel<IPatientResponseInfo>>, IQuestionaireViewModel
    {
        public QuestionaireViewModel(ISystemProcess process,  List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications, List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo, Type orientation) : base(new ObservableListViewModel<IPatientResponseInfo>(eventSubscriptions, eventPublications, commandInfo, process, orientation))
        {
           this.WireEvents();
        }

        
        public ReactiveProperty<IProcessStateList<IPatientResponseInfo>> State => this.Instance.State;


        ReactiveProperty<IProcessState<IPatientResponseInfo>> IEntityViewModel<IPatientResponseInfo>.State => new ReactiveProperty<IProcessState<IPatientResponseInfo>>(this.Instance.State.Value);
        public ReactiveProperty<IPatientResponseInfo> CurrentEntity => this.Instance.CurrentEntity;

        public ObservableDictionary<string, dynamic> ChangeTracking => this.Instance.ChangeTracking;
        public ObservableList<IPatientResponseInfo> EntitySet => this.Instance.EntitySet;
        public ObservableList<IPatientResponseInfo> SelectedEntities => this.Instance.SelectedEntities;
        public ReactiveProperty<RowState> RowState => this.Instance.RowState;


        public string Field { get; set; }
        public string Value { get; set; }
    }
}
