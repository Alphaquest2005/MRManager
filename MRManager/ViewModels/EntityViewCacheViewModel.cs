using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;
using Core.Common.UI;
using Interfaces;
using JB.Collections.Reactive;
using Reactive.Bindings;
using ViewModel.Interfaces;
using ViewModelInterfaces;

namespace ViewModels
{
    //[Export(typeof(IEntityViewCacheViewModel<>))]
    //public class EntityViewCacheViewModel<TEntityView> : ObservableListViewModel<TEntityView>, IEntityViewCacheViewModel<TEntityView> where TEntityView : IEntityView
    //{
    //    public EntityViewCacheViewModel() { }
    //    public EntityViewCacheViewModel(ISystemProcess process, List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications, List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo, Type orientation) : base(eventSubscriptions, eventPublications, commandInfo, process, orientation)
    //    {
    //        this.WireEvents();
    //    }

    //}


    //HACK: Matching on interface... root cause XAML can't bind really take generic types
    [Export(typeof(IEntityViewCacheViewModel<IDoctorInfo>))]
    public class DoctorInfoCacheViewModel : ObservableListViewModel<IDoctorInfo>, IEntityViewCacheViewModel<IDoctorInfo>
    {
        public DoctorInfoCacheViewModel() { }
        public DoctorInfoCacheViewModel(ISystemProcess process, List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications, List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo, Type orientation) : base(eventSubscriptions, eventPublications, commandInfo, process, orientation)
        {
            this.WireEvents();
        }

    }

    [Export(typeof(IEntityViewCacheViewModel<ISyntomMedicalSystemInfo>))]
    public class SystemInfoCacheViewModel : DynamicViewModel<ObservableListViewModel<ISyntomMedicalSystemInfo>>, IEntityViewCacheViewModel<ISyntomMedicalSystemInfo>
    {
        public SystemInfoCacheViewModel() { }
        public SystemInfoCacheViewModel(ISystemProcess process, List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications, List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo, Type orientation) : base(new ObservableListViewModel<ISyntomMedicalSystemInfo>(eventSubscriptions, eventPublications, commandInfo, process, orientation))
        {
            this.WireEvents();
        }

        ReactiveProperty<IProcessState<ISyntomMedicalSystemInfo>> IEntityViewModel<ISyntomMedicalSystemInfo>.State {
            get; }

        public ReactiveProperty<ISyntomMedicalSystemInfo> CurrentEntity => this.ViewModel.CurrentEntity;
        public ObservableList<ISyntomMedicalSystemInfo> EntitySet => this.ViewModel.EntitySet;
        public ObservableList<ISyntomMedicalSystemInfo> SelectedEntities => this.ViewModel.SelectedEntities;
        public ObservableBindingList<ISyntomMedicalSystemInfo> ChangeTrackingList => this.ViewModel.ChangeTrackingList;

        ReactiveProperty<IProcessStateList<ISyntomMedicalSystemInfo>> IEntityListViewModel<ISyntomMedicalSystemInfo>.
            State => this.ViewModel.State;

        public ObservableDictionary<string, dynamic> ChangeTracking => this.ViewModel.ChangeTracking;
    }
}