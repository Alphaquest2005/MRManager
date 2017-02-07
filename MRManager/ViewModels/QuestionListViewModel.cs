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
                ChangeTrackingList.Clear();
                
                if (entitySet.Any())
                {
                    var resLst = entitySet.OrderBy(z => z.QuestionNumber).ToList();
                    var currentInterviewId = entitySet.First().InterviewId;
                    resLst.Add(new QuestionInfo()
                    {
                        Id = 0,
                        Description = "Edit to Create New Question",
                        EntityAttributeId = 0,
                        InterviewId = currentInterviewId,
                        Attribute = "Unspecified",
                        Entity = "Unspecified",
                        Type = "TextBox"
                    });
                    ChangeTrackingList.AddRange(resLst);

                }
               
            });

        }

       

        private void updateType(IObservedChange<IQuestionInfo, string> typeChange)
        {
            if (typeChange.Sender.Entity != "Unspecified")
            {
                ChangeTracking.AddOrUpdate(nameof(IQuestionInfo.EntityAttributeId), typeChange.Sender.EntityAttributeId);
                ChangeTracking.AddOrUpdate(nameof(IQuestionInfo.Type), typeChange.Value);
            }
            else
            {
                ChangeTracking.AddOrUpdate(nameof(IQuestionInfo.Type), typeChange.Value);
            }
        }

        private void updateAttribute(IObservedChange<IQuestionInfo, string> attributeChange)
        {
            if (attributeChange.Sender.Entity != "Unspecified")
            {
                ChangeTracking.AddOrUpdate(nameof(IQuestionInfo.EntityAttributeId), attributeChange.Sender.EntityAttributeId);
                ChangeTracking.AddOrUpdate(nameof(IQuestionInfo.Attribute), attributeChange.Value);
            }
            else
            {
                ChangeTracking.AddOrUpdate(nameof(IQuestionInfo.Attribute), attributeChange.Value);
            }
        }

        private void updateEntity(IObservedChange<IQuestionInfo, string> entityChange)
        {
            if (entityChange.Sender.EntityAttributeId != 0)
            {
                ChangeTracking.AddOrUpdate(nameof(IQuestionInfo.EntityAttributeId), entityChange.Sender.EntityAttributeId);
                ChangeTracking.AddOrUpdate(nameof(IQuestionInfo.Entity), entityChange.Value);
            }
            else
            {
                ChangeTracking.AddOrUpdate(nameof(IQuestionInfo.EntityAttributeId), entityChange.Sender.EntityAttributeId);
                ChangeTracking.AddOrUpdate(nameof(IQuestionInfo.Entity), entityChange.Value);
                ChangeTracking.AddOrUpdate(nameof(IQuestionInfo.Attribute), entityChange.Sender.Attribute);
            }
        }

        private void updateDescription(IObservedChange<IQuestionInfo, string> descriptionChange)
        {
            if (descriptionChange.Sender.Id != 0)
            {
                ChangeTracking.AddOrUpdate(nameof(IQuestionInfo.Id), descriptionChange.Sender.Id);
                ChangeTracking.AddOrUpdate(nameof(IQuestionInfo.Description), descriptionChange.Value);
            }
            else
            {
                //add question
                ChangeTracking.AddOrUpdate(nameof(IQuestionInfo.Id), descriptionChange.Sender.Id);
                ChangeTracking.AddOrUpdate(nameof(IQuestionInfo.Description), descriptionChange.Value);
                ChangeTracking.AddOrUpdate(nameof(IQuestionInfo.InterviewId), CurrentInterview.Id);

            }
           
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
                    ChangeTrackingList.Clear();
                    return;
                }
                if (ChangeTrackingList.Any()) return;
                ChangeTrackingList.Add(new QuestionInfo()
                {
                    Id = 0,
                    Description = "Edit to Create New Question",
                    EntityAttributeId = 0,
                    InterviewId = _currentInterview.Id,
                    Attribute = "Unspecified",
                    Entity = "Unspecified",
                    Type = "TextBox"
                });

               
            }
        }


        public ObservableBindingList<IQuestionInfo> ChangeTrackingList
        {
            get { return _changeTrackingList; }
            set { _changeTrackingList = value; }
        }
    }
}
