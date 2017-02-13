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

    [Export(typeof(IQuestionListViewModel))]
    public class QuestionListViewModel : DynamicViewModel<ObservableListViewModel<IQuestionInfo>>, IQuestionListViewModel
    {
        private ObservableBindingList<IQuestionInfo> _changeTrackingList = new ObservableBindingList<IQuestionInfo>();
        private ObservableList<IQuestionInfo> _entitySet;
        private IInterviewInfo _currentInterview;
        [ImportingConstructor]
        public QuestionListViewModel(ISystemProcess process,  List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications, List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo, Type orientation) : base(new ObservableListViewModel<IQuestionInfo>(eventSubscriptions, eventPublications, commandInfo, process, orientation))
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            this.WireEvents();
            _entitySet = this.ViewModel.EntitySet;
            Instance.ViewModel.WhenAnyValue(x => x.EntitySet).Subscribe(x => UpdateChangeCollectionList(x));
        }


        private void UpdateChangeCollectionList(ObservableList<IQuestionInfo> entitySet)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                EntitySet.Clear();
                var resLst = entitySet.OrderBy(z => z.QuestionNumber).ToList();
                EntitySet.AddRange(resLst);
                EntitySet.Reset();

            });

        }





        public ReactiveProperty<IProcessStateList<IQuestionInfo>> State => this.ViewModel.State;


        ReactiveProperty<IProcessState<IQuestionInfo>> IEntityViewModel<IQuestionInfo>.State => new ReactiveProperty<IProcessState<IQuestionInfo>>(this.ViewModel.State.Value);
        public ReactiveProperty<IQuestionInfo> CurrentEntity => this.ViewModel.CurrentEntity;

        public ObservableDictionary<string, dynamic> ChangeTracking => this.ViewModel.ChangeTracking;

        public ObservableList<IQuestionInfo> EntitySet
        {
            get { return _entitySet; }
            set { _entitySet = value; }
        }

    
        public ObservableList<IQuestionInfo> SelectedEntities => this.ViewModel.SelectedEntities;

        public IInterviewInfo CurrentInterview
        {
            get { return _currentInterview; }
            set
            {
                _currentInterview = value;
                if (_currentInterview == null)
                {
                    EntitySet.Clear();
                    return;
                }
                
                EntitySet.Add(new QuestionInfo()
                {
                    Id = 0,
                    Description = "Edit to Create New Question",
                    EntityAttributeId = 0,
                    InterviewId = _currentInterview.Id,
                    Attribute = "Unspecified",
                    Entity = "Unspecified",
                    Type = "TextBox"
                });
                EntitySet.Reset();

               
            }
        }


    }
}
