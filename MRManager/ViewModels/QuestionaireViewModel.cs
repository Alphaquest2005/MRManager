using System;
using System.Collections.Generic;
using System.ComponentModel;
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


        public QuestionaireViewModel(ISystemProcess process,
            List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions,
            List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications,
            List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo, Type orientation)
            : base(
                new ObservableListViewModel<IPatientResponseInfo>(eventSubscriptions, eventPublications, commandInfo,
                    process, orientation))
        {
            this.WireEvents();
            this.WhenAny(x => x.RowState.Value, x => x.Value).Subscribe(x => test(x));
        }

        private void test(RowState reactiveProperty)
        {
           // throw new NotImplementedException();
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
