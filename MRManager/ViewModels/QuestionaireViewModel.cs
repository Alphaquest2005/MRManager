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
using Reactive.Bindings.Extensions;
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


        public QuestionaireViewModel(ISystemProcess process,
            List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions,
            List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications,
            List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo, Type orientation)
            : base(
                new ObservableListViewModel<IPatientResponseInfo>(eventSubscriptions, eventPublications, commandInfo,
                    process, orientation))
        {
            this.WireEvents();
            this.WhenAnyValue(x => x.CurrentEntity.Value).Subscribe(x => UpdateChangeCollectionList(x));
            
           
        }

        private void updateChangeTracking(IObservedChange<IResponseOptionInfo, string> change)
        {
            ChangeTracking.AddOrUpdate(nameof(IResponseInfo.Id), change.Sender.ResponseId);
            ChangeTracking.AddOrUpdate(nameof(IResponseInfo.Value), change.Value);
        }

        private void UpdateChangeCollectionList(IPatientResponseInfo patientResponseInfo)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ChangeTrackingList.Clear();
                if (patientResponseInfo?.ResponseOptions == null) return;
                ChangeTrackingList.AddRange(patientResponseInfo.ResponseOptions);
                foreach (var itm in ChangeTrackingList)
                {
                    itm.ObservableForProperty(x =>x.Value).Subscribe(x => updateChangeTracking(x));
                }
            });

        }
        //AutoProperty Fucking up i really don't jnow why
        private ObservableBindingList<IResponseOptionInfo> _changeTrackingList = new ObservableBindingList<IResponseOptionInfo>();
        public ObservableBindingList<IResponseOptionInfo> ChangeTrackingList
        {
            get { return _changeTrackingList; }
            set { _changeTrackingList = value; }
        }


        public ReactiveProperty<IProcessStateList<IPatientResponseInfo>> State => this.ViewModel.State;


        ReactiveProperty<IProcessState<IPatientResponseInfo>> IEntityViewModel<IPatientResponseInfo>.State => new ReactiveProperty<IProcessState<IPatientResponseInfo>>(this.ViewModel.State.Value);
        public ReactiveProperty<IPatientResponseInfo> CurrentEntity => this.ViewModel.CurrentEntity;

        public ObservableDictionary<string, dynamic> ChangeTracking => this.ViewModel.ChangeTracking;
        public ObservableList<IPatientResponseInfo> EntitySet => this.ViewModel.EntitySet;
        public ObservableList<IPatientResponseInfo> SelectedEntities => this.ViewModel.SelectedEntities;
        


        public string Field { get; set; }
        public string Value { get; set; }
    }
}
