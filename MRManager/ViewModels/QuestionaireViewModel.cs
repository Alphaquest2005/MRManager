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

        [ImportingConstructor]
        public QuestionaireViewModel(ISystemProcess process,
            List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions,
            List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications,
            List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo, Type orientation)
            : base(
                new ObservableListViewModel<IResponseOptionInfo>(eventSubscriptions, eventPublications, commandInfo,
                    process, orientation))
        {
            this.WireEvents();
            this.WhenAnyValue(x => x.CurrentQuestion.Value).Subscribe(x => UpdateChangeCollectionList(x));
            
           
        }


        private void UpdateChangeCollectionList(IQuestionResponseOptionInfo patientResponseInfo)
        {
            
                
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
                                    QuestionResponseTypeId = 1
                                });
                
                this.ViewModel.EntitySet = new ObservableList<IResponseOptionInfo>(resLst.Select(x => (IResponseOptionInfo)x).ToList());
                OnPropertyChanged(nameof(EntitySet));

        }

        public IList<IQuestionResponseOptionInfo> Questions
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
                UpdateChangeCollectionList(CurrentQuestion.Value);
            }
        }
        private IPatientSyntomInfo _currentPatientSyntom;
        public IPatientSyntomInfo CurrentPatientSyntom
        {
            get { return _currentPatientSyntom; }
            set
            {
                _currentPatientSyntom = value;
            }
        }

        public ReactiveProperty<IQuestionResponseOptionInfo> CurrentQuestion { get; } = new ReactiveProperty<IQuestionResponseOptionInfo>();

        
        private readonly object lockObject = new object();
        private IList<IQuestionResponseOptionInfo> _questions = new ObservableList<IQuestionResponseOptionInfo>();
        


        public ReactiveProperty<IProcessStateList<IResponseOptionInfo>> State => this.ViewModel.State;


        ReactiveProperty<IProcessState<IResponseOptionInfo>> IEntityViewModel<IResponseOptionInfo>.State => new ReactiveProperty<IProcessState<IResponseOptionInfo>>(this.ViewModel.State.Value);
        public ReactiveProperty<IResponseOptionInfo> CurrentEntity => this.ViewModel.CurrentEntity;

        public ObservableDictionary<string, dynamic> ChangeTracking => this.ViewModel.ChangeTracking;
        public ObservableList<IResponseOptionInfo> EntitySet => this.ViewModel.EntitySet;
        public ObservableList<IResponseOptionInfo> SelectedEntities => this.ViewModel.SelectedEntities;

        public ReactiveProperty<IQuestionResponseTypes> DataType { get; } = new ReactiveProperty<IQuestionResponseTypes>();
        
    }
}
