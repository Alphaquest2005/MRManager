using System;
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
    public class QuestionListViewModel : DynamicViewModel<ObservableListViewModel<IQuestionInfo>>, IQuestionListViewModel
    {
        private ObservableBindingList<IQuestionInfo> _changeTrackingList = new ObservableBindingList<IQuestionInfo>();
        private ObservableList<IQuestionInfo> _entitySet;

        public QuestionListViewModel(ISystemProcess process,  List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications, List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo, Type orientation) : base(new ObservableListViewModel<IQuestionInfo>(eventSubscriptions, eventPublications, commandInfo, process, orientation))
        {
           this.WireEvents();
            _entitySet = this.ViewModel.EntitySet;
            Instance.ViewModel.WhenAnyValue(x => x.EntitySet).Subscribe(x => UpdateChangeCollectionList(x));
        }

        private void updateChangeTracking(IObservedChange<IQuestionInfo, IQuestionInfo> change)
        {
           
        }

        private void UpdateChangeCollectionList(ObservableList<IQuestionInfo> entitySet)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ChangeTrackingList.Clear();
                if (!entitySet.Any()) return;
                ChangeTrackingList.AddRange(entitySet);
                foreach (var itm in ChangeTrackingList)
                {
                    itm.ObservableForProperty(x => x).Subscribe(x => updateChangeTracking(x));
                }
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
    

        public ObservableBindingList<IQuestionInfo> ChangeTrackingList
        {
            get { return _changeTrackingList; }
            set { _changeTrackingList = value; }
        }
    }
}
