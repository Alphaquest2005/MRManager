﻿// <autogenerated>
//   This file was generated by T4 code generator Amoeba-Master.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reactive;
using SystemInterfaces;
using Core.Common.UI;
using Interfaces;
using Reactive.Bindings;
using ReactiveUI;
using ViewModel.Interfaces;
using ViewModelInterfaces;

namespace ViewModels
{
    //[Export(typeof(IEntityCacheViewModel<>))]
    //public class EntityCacheViewModel<TEntity> : ObservableListViewModel<TEntity>, IEntityCacheViewModel<TEntity> where TEntity : IEntity
    //{
    //    public EntityCacheViewModel() { }
       
    //    public EntityCacheViewModel(ISystemProcess process, List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications, List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo, Type orientation) : base(eventSubscriptions, eventPublications, commandInfo, process, orientation)
    //    {
    //        this.WireEvents();
    //    }


    //}

    [Export(typeof(IEntityCacheViewModel<ISyntomPriority>))]
    public class SyntomPriorityCacheViewModel : ObservableListViewModel<ISyntomPriority>, IEntityCacheViewModel<ISyntomPriority> 
    {
        public SyntomPriorityCacheViewModel() { }

        public SyntomPriorityCacheViewModel(ISystemProcess process, List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications, List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo, Type orientation) : base(eventSubscriptions, eventPublications, commandInfo, process, orientation)
        {
            this.WireEvents();
        }

    }

    [Export(typeof(IEntityCacheViewModel<ISyntomStatus>))]
    public class SyntomStatusCacheViewModel : ObservableListViewModel<ISyntomStatus>, IEntityCacheViewModel<ISyntomStatus>
    {
        public SyntomStatusCacheViewModel() { }

        public SyntomStatusCacheViewModel(ISystemProcess process, List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications, List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo, Type orientation) : base(eventSubscriptions, eventPublications, commandInfo, process, orientation)
        {
            this.WireEvents();
        }

    }

    [Export(typeof(IEntityCacheViewModel<ISyntoms>))]
    public class SyntomCacheViewModel : ObservableListViewModel<ISyntoms>, IEntityCacheViewModel<ISyntoms>
    {
        public SyntomCacheViewModel() { }

        public SyntomCacheViewModel(ISystemProcess process, List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications, List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo, Type orientation) : base(eventSubscriptions, eventPublications, commandInfo, process, orientation)
        {
            this.WireEvents();
        }

    }

    [Export(typeof(IEntityCacheViewModel<IVisitType>))]
    public class VisitTypeCacheViewModel : ObservableListViewModel<IVisitType>, IEntityCacheViewModel<IVisitType>
    {
        public VisitTypeCacheViewModel() { }

        public VisitTypeCacheViewModel(ISystemProcess process, List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications, List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo, Type orientation) : base(eventSubscriptions, eventPublications, commandInfo, process, orientation)
        {
            this.WireEvents();
        }

    }

    [Export(typeof(IEntityCacheViewModel<IPhase>))]
    public class PhaseCacheViewModel : ObservableListViewModel<IPhase>, IEntityCacheViewModel<IPhase>
    {
        public PhaseCacheViewModel() { }

        public PhaseCacheViewModel(ISystemProcess process, List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications, List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo, Type orientation) : base(eventSubscriptions, eventPublications, commandInfo, process, orientation)
        {
            this.WireEvents();
        }

    }

    [Export(typeof(IEntityCacheViewModel<IMedicalCategory>))]
    public class MedicalCategoryCacheViewModel : ObservableListViewModel<IMedicalCategory>, IEntityCacheViewModel<IMedicalCategory>
    {
        public MedicalCategoryCacheViewModel() { }

        public MedicalCategoryCacheViewModel(ISystemProcess process, List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications, List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo, Type orientation) : base(eventSubscriptions, eventPublications, commandInfo, process, orientation)
        {
            this.WireEvents();
        }

    }

}