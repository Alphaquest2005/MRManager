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
    public class QuestionaireViewModel : DynamicViewModel<ObservableListViewModel<IQuestionResponseOptionInfo>>, IQuestionaireViewModel
    {

        [ImportingConstructor]
        public QuestionaireViewModel(ISystemProcess process,
            List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions,
            List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications,
            List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo, Type orientation)
            : base(
                new ObservableListViewModel<IQuestionResponseOptionInfo>(eventSubscriptions, eventPublications, commandInfo,
                    process, orientation))
        {
            this.WireEvents();
            this.WhenAnyValue(x => x.CurrentEntity.Value).Subscribe(x => UpdateChangeCollectionList(x));
            
           
        }

        private void updateChangeTracking(IObservedChange<IResponseOptionInfo, string> change)
        {
            if (change.Sender.ResponseId != 0)
            {
                ChangeTracking.AddOrUpdate(nameof(IResponseInfo.Id), change.Sender.ResponseId);
                ChangeTracking.AddOrUpdate(nameof(IResponseInfo.Value), change.Value);
            }
            else
            {
                ChangeTracking.AddOrUpdate(nameof(IResponseInfo.Id), change.Sender.ResponseId);
                ChangeTracking.AddOrUpdate(nameof(IResponseInfo.PatientResponseId), change.Sender.PatientResponseId);
                ChangeTracking.AddOrUpdate(nameof(IResponseInfo.ResponseOptionId), change.Sender.Id);
                ChangeTracking.AddOrUpdate(nameof(IResponseInfo.Value), change.Value);
            }
        }

        private void UpdateChangeCollectionList(IQuestionResponseOptionInfo patientResponseInfo)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ChangeTrackingList.Clear();
                if (patientResponseInfo == null) return;
                var resLst = new List<ResponseOptionInfo>();
                BindingOperations.EnableCollectionSynchronization(resLst, lockObject);
               
                if (patientResponseInfo?.ResponseOptions != null)
                {

                    resLst.AddRange(patientResponseInfo.ResponseOptions.Select(x => (ResponseOptionInfo)x ));
                    if (CurrentPatientVisit != null)
                    {
                        foreach (
                            var itm in patientResponseInfo.PatientResponses.Where(x => x.PatientVisitId == CurrentPatientVisit.Id)
                            )
                        {
                           var res =  resLst.First(x => x.Id == itm.Id);
                            res.Value = itm.Value;
                            res.ResponseId = itm.ResponseId;
                            res.PatientResponseId = itm.Id;

                        }
                    }
                    
                }
                

                resLst.Add(new ResponseOptionInfo()
                                {
                                    Id = 0,
                                    Description = "Edit to Create New Response Option",
                                    QuestionId = patientResponseInfo.Id,
                                    Type = "TextBox"
                                });
                
                ChangeTrackingList.AddRange(resLst);
               
                
                
            });

        }

        public IPatientVisitInfo CurrentPatientVisit
        {
            get { return _currentPatientVisit; }
            set
            {
                _currentPatientVisit = value;
                UpdateChangeCollectionList(CurrentEntity.Value);
            }
        }

        //AutoProperty Fucking up i really don't jnow why
        private ObservableBindingList<IResponseOptionInfo> _changeTrackingList = new ObservableBindingList<IResponseOptionInfo>();
        private readonly object lockObject = new object();
        private IPatientVisitInfo _currentPatientVisit;

        public ObservableBindingList<IResponseOptionInfo> ChangeTrackingList
        {
            get { return _changeTrackingList; }
            set { _changeTrackingList = value; }
        }


        public ReactiveProperty<IProcessStateList<IQuestionResponseOptionInfo>> State => this.ViewModel.State;


        ReactiveProperty<IProcessState<IQuestionResponseOptionInfo>> IEntityViewModel<IQuestionResponseOptionInfo>.State => new ReactiveProperty<IProcessState<IQuestionResponseOptionInfo>>(this.ViewModel.State.Value);
        public ReactiveProperty<IQuestionResponseOptionInfo> CurrentEntity => this.ViewModel.CurrentEntity;

        public ObservableDictionary<string, dynamic> ChangeTracking => this.ViewModel.ChangeTracking;
        public ObservableList<IQuestionResponseOptionInfo> EntitySet => this.ViewModel.EntitySet;
        public ObservableList<IQuestionResponseOptionInfo> SelectedEntities => this.ViewModel.SelectedEntities;

        ObservableBindingList<IQuestionResponseOptionInfo> IEntityListViewModel<IQuestionResponseOptionInfo>.
            ChangeTrackingList => this.ViewModel.ChangeTrackingList;


        public string Field { get; set; }
        public string Value { get; set; }
    }
}
