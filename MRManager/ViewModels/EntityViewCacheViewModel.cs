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
        public DoctorInfoCacheViewModel(ISystemProcess process, IViewInfo viewInfo, List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications, List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo, Type orientation, int priority) : base(viewInfo, eventSubscriptions, eventPublications, commandInfo, process, orientation, priority)
        {
            this.WireEvents();
        }

    }

    [Export(typeof(IEntityViewCacheViewModel<ISyntomMedicalSystemInfo>))]
    public class SystemInfoCacheViewModel : DynamicViewModel<ObservableListViewModel<ISyntomMedicalSystemInfo>>, IEntityViewCacheViewModel<ISyntomMedicalSystemInfo>
    {
        public SystemInfoCacheViewModel() { }
        public SystemInfoCacheViewModel(ISystemProcess process, IViewInfo viewInfo, List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications, List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo, Type orientation, int priority) : base(new ObservableListViewModel<ISyntomMedicalSystemInfo>(viewInfo,eventSubscriptions, eventPublications, commandInfo, process, orientation, priority))
        {
            this.WireEvents();
        }


        IEntityListViewModel<ISyntomMedicalSystemInfo> IEntityListViewModel<ISyntomMedicalSystemInfo>.Instance => (IEntityListViewModel<ISyntomMedicalSystemInfo>) SystemInfoCacheViewModel.Instance;

        ReactiveProperty<IProcessState<ISyntomMedicalSystemInfo>> IEntityViewModel<ISyntomMedicalSystemInfo>.State {
            get; }

        public ReactiveProperty<ISyntomMedicalSystemInfo> CurrentEntity => this.ViewModel.CurrentEntity;
        public ReactiveProperty<ObservableList<ISyntomMedicalSystemInfo>> EntitySet => this.ViewModel.EntitySet;
        public ReactiveProperty<ObservableList<ISyntomMedicalSystemInfo>> SelectedEntities => this.ViewModel.SelectedEntities;
        public ObservableBindingList<ISyntomMedicalSystemInfo> ChangeTrackingList => this.ViewModel.ChangeTrackingList;

        ReactiveProperty<IProcessStateList<ISyntomMedicalSystemInfo>> IEntityListViewModel<ISyntomMedicalSystemInfo>.
            State => this.ViewModel.State;

        public ObservableDictionary<string, dynamic> ChangeTracking => this.ViewModel.ChangeTracking;
    }
}