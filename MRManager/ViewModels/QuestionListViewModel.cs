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
    public class QuestionListViewModel : DynamicViewModel<ObservableListViewModel<IQuestionInfo>>,
        IQuestionListViewModel
    {

        private IInterviewInfo _currentInterview;

        [ImportingConstructor]
        public QuestionListViewModel(ISystemProcess process,
            List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions,
            List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications,
            List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo, Type orientation)
            : base(
                new ObservableListViewModel<IQuestionInfo>(eventSubscriptions, eventPublications, commandInfo, process,
                    orientation))
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            this.WireEvents();

            Instance.ViewModel.WhenAnyValue(x => x.EntitySet).Subscribe(x => UpdateChangeCollectionList(x));
        }


        private void UpdateChangeCollectionList(ObservableList<IQuestionInfo> entitySet)
        {

            if (_currentInterview == null)
            {
                this.ViewModel.EntitySet.Clear();
            }
            else
            {
                if (this.ViewModel.EntitySet.FirstOrDefault(x => x.Id == 0) != null) return;
                var res = entitySet.OrderBy(z => z.QuestionNumber).ToList();
                res.Add(new QuestionInfo()
                {
                    Id = 0,
                    Description = "Edit to Create New Question",
                    EntityAttributeId = 0,
                    InterviewId = _currentInterview.Id,
                    Attribute = "Unspecified",
                    Entity = "Unspecified",
                    Type = "TextBox"
                });
                this.ViewModel.EntitySet = new ObservableList<IQuestionInfo>(res);
            }
            OnPropertyChanged(nameof(EntitySet));

        }





        public ReactiveProperty<IProcessStateList<IQuestionInfo>> State => this.ViewModel.State;


        ReactiveProperty<IProcessState<IQuestionInfo>> IEntityViewModel<IQuestionInfo>.State
            => new ReactiveProperty<IProcessState<IQuestionInfo>>(this.ViewModel.State.Value);

        public ReactiveProperty<IQuestionInfo> CurrentEntity => this.ViewModel.CurrentEntity;

        public ObservableDictionary<string, dynamic> ChangeTracking => this.ViewModel.ChangeTracking;

        public ObservableList<IQuestionInfo> EntitySet => this.ViewModel.EntitySet;


        public ObservableList<IQuestionInfo> SelectedEntities => this.ViewModel.SelectedEntities;

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
