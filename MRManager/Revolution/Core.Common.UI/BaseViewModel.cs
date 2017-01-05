using System;
using System.Collections.Generic;
using SystemInterfaces;
using CommonMessages;

using Reactive.Bindings;
using ReactiveUI;
using RevolutionEntities.Process;
using ViewModel.Interfaces;


namespace Core.Common.UI
{
    public abstract class BaseViewModel<TViewModel> : ReactiveObject, IViewModel
    {
       
        protected BaseViewModel(ISystemProcess process, List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications, List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo, Type orientation)
        {
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

        public ISourceMessage SourceMessage => new SourceMessage(new MessageSource(this.ToString()), new MachineInfo(Environment.MachineName, Environment.ProcessorCount));
        public List<IViewModelEventSubscription<IViewModel, IEvent>> EventSubscriptions { get;}
        public List<IViewModelEventPublication<IViewModel, IEvent>> EventPublications { get; }
        public List<IViewModelEventCommand<IViewModel, IEvent>> CommandInfo { get; }
        public Type Orientation { get; }
        public Type ViewModelType { get; }

        public Dictionary<string, dynamic> Commands { get; } = new Dictionary<string, dynamic>();

        public ISystemProcess Process { get; set; }
        public string Name { get; }
        public string Symbol { get; }
        public string Description { get; }

    }
}