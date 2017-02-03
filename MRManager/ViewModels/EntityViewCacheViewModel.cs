using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;
using Core.Common.UI;
using ViewModel.Interfaces;

namespace ViewModels
{
    [Export(typeof(IEntityViewCacheViewModel<>))]
    public class EntityViewCacheViewModel<TEntityView> : ObservableListViewModel<TEntityView>, IEntityViewCacheViewModel<TEntityView> where TEntityView : IEntityView
    {
        public EntityViewCacheViewModel(ISystemProcess process, List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications, List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo, Type orientation) : base(eventSubscriptions, eventPublications, commandInfo, process, orientation)
        {
            this.WireEvents();
        }

    }
}