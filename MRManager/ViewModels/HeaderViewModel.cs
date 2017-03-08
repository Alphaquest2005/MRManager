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

    [Export(typeof(IHeaderViewModel))]
    public class HeaderViewModel : DynamicViewModel<ObservableViewModel>, IHeaderViewModel
    {
        [ImportingConstructor]
        public HeaderViewModel(ISystemProcess process, IViewInfo viewInfo, List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications, List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo, Type orientation, int priority) : base(new ObservableViewModel(viewInfo,eventSubscriptions, eventPublications, commandInfo, process, orientation, priority))
        {
            this.WireEvents();
        }
    }
}
