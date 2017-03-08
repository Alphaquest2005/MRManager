using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;
using Core.Common.UI;
using EF.Entities;
using FluentValidation;
using Interfaces;
using JB.Collections.Reactive;
using Reactive.Bindings;
using ReactiveUI;
using ValidationSets;
using ViewModel.Interfaces;
using ViewModelInterfaces;

namespace ViewModels
{

    [Export(typeof(IFooterViewModel))]
    public class FooterViewModel : DynamicViewModel<ObservableViewModel>, IFooterViewModel
    {
        [ImportingConstructor]
        public FooterViewModel(ISystemProcess process, IViewInfo viewInfo, List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications, List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo, Type orientation, int priority) : base(new ObservableViewModel(viewInfo, eventSubscriptions, eventPublications, commandInfo, process, orientation, priority))
        {
            this.WireEvents();
        }

        
        public ReactiveProperty<IPatientInfo> CurrentPatient { get; } = new ReactiveProperty<IPatientInfo>();
        public ReactiveProperty<IPatientVisitInfo> CurrentPatientVisit { get; } = new ReactiveProperty<IPatientVisitInfo>();
        public ReactiveProperty<IPatientSyntomInfo> CurrentPatientSyntom { get; } = new ReactiveProperty<IPatientSyntomInfo>();
        public ReactiveProperty<IInterviewInfo> CurrentInterview { get; } = new ReactiveProperty<IInterviewInfo>();
    }
}
