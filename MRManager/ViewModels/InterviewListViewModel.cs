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
    public class InterviewListViewModel : DynamicViewModel<ObservableListViewModel<IInterviewInfo>>, IInterviewListViewModel
    {
        public InterviewListViewModel(ISystemProcess process,  List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications, List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo, Type orientation) : base(new ObservableListViewModel<IInterviewInfo>(eventSubscriptions, eventPublications, commandInfo, process, orientation))
        {
           this.WireEvents();
        }

        
        public ReactiveProperty<IProcessStateList<IInterviewInfo>> State => this.Instance.State;


        ReactiveProperty<IProcessState<IInterviewInfo>> IEntityViewModel<IInterviewInfo>.State => new ReactiveProperty<IProcessState<IInterviewInfo>>(this.Instance.State.Value);
        public ReactiveProperty<IInterviewInfo> CurrentEntity => this.Instance.CurrentEntity;

        public ObservableDictionary<string, dynamic> ChangeTracking => this.Instance.ChangeTracking;
        public ObservableList<IInterviewInfo> EntitySet => this.Instance.EntitySet;
        public ObservableList<IInterviewInfo> SelectedEntities => this.Instance.SelectedEntities;


        public string Field { get; set; }
        public string Value { get; set; }
    }
}
