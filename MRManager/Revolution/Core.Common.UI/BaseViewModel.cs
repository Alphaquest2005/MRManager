using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive;
using SystemInterfaces;
using Common;
using Reactive.Bindings;
using ReactiveUI;
using RevolutionEntities.Process;
using Utilities;
using ViewModel.Interfaces;


namespace Core.Common.UI
{
    public abstract class BaseViewModel<TViewModel> : ReactiveObject, IViewModel
    {
        public ISystemSource Source => new Source(Guid.NewGuid(), "ViewModel:" + typeof(TViewModel).GetFriendlyName(),new SourceType(typeof(BaseViewModel<TViewModel>)), new MachineInfo(Environment.MachineName, Environment.ProcessorCount));
        protected BaseViewModel(ISystemProcess process, List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications, List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo, Type orientation)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            Process = process;
            EventSubscriptions = eventSubscriptions;
            EventPublications = eventPublications;
            CommandInfo = commandInfo;
            Orientation = orientation;
            ViewModelType = typeof(TViewModel);
            Name = process.Name;
            Description = process.Description;
            Symbol = process.Symbol;
        }

        
        public List<IViewModelEventSubscription<IViewModel, IEvent>> EventSubscriptions { get;}
        public List<IViewModelEventPublication<IViewModel, IEvent>> EventPublications { get; }
        public List<IViewModelEventCommand<IViewModel, IEvent>> CommandInfo { get; }
        public ReactiveProperty<RowState> RowState { get; }
        public Type Orientation { get; }
        public Type ViewModelType { get; }

        public Dictionary<string, ReactiveCommand<IViewModel, Unit>> Commands { get; } = new Dictionary<string, ReactiveCommand<IViewModel, Unit>>();

        public ISystemProcess Process { get; set; }
        public string Name { get; }
        public string Symbol { get; }
        public string Description { get; }

    }
}