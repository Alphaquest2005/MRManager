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

    [Export(typeof (IQuestionListViewModel))]
    public class QuestionListViewModel : DynamicViewModel<ObservableListViewModel<IQuestionInfo>>,IQuestionListViewModel
    {

        private IInterviewInfo _currentInterview;

        [ImportingConstructor]
        public QuestionListViewModel(ISystemProcess process, IViewInfo viewInfo, List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications, List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo, Type orientation, int priority)
            : base(
                new ObservableListViewModel<IQuestionInfo>(viewInfo, eventSubscriptions, eventPublications, commandInfo, process,
                    orientation, priority))
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            this.WireEvents();

        }


        IEntityListViewModel<IQuestionInfo> IEntityListViewModel<IQuestionInfo>.Instance => (IEntityListViewModel<IQuestionInfo>) QuestionListViewModel.Instance;

        public ReactiveProperty<IProcessStateList<IQuestionInfo>> State => this.ViewModel.State;


        ReactiveProperty<IProcessState<IQuestionInfo>> IEntityViewModel<IQuestionInfo>.State
            => new ReactiveProperty<IProcessState<IQuestionInfo>>(this.ViewModel.State.Value);

        public ReactiveProperty<IQuestionInfo> CurrentEntity => this.ViewModel.CurrentEntity;

        public ObservableDictionary<string, dynamic> ChangeTracking => this.ViewModel.ChangeTracking;

        public ReactiveProperty<ObservableList<IQuestionInfo>> EntitySet => this.ViewModel.EntitySet;


        public ReactiveProperty<ObservableList<IQuestionInfo>> SelectedEntities => this.ViewModel.SelectedEntities;

        public IInterviewInfo CurrentInterview
        {
            get { return _currentInterview; }
            set
            {
                var @equals = _currentInterview?.Equals(value);
                if (@equals != null && (bool) @equals) return;
                _currentInterview = value;
                OnPropertyChanged();
            }
        }


    }
}
