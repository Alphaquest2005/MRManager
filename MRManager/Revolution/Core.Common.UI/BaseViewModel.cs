using System.Collections.Generic;
using SystemInterfaces;
using CommonMessages;
using DataInterfaces;
using Reactive.Bindings;
using ReactiveUI;
using ViewModel.Interfaces;


namespace Core.Common.UI
{
    public abstract class BaseViewModel : ReactiveObject, IViewModel
    {
       
        protected BaseViewModel(ISystemProcess process, List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications, List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo)
        {
            Process = process;
            EventSubscriptions = eventSubscriptions;
            EventPublications = eventPublications;
            CommandInfo = commandInfo;
            Name = process.Name;
            Description = process.Description;
            Symbol = process.Symbol;
            MsgSource = new MessageSource(this.ToString());

        }

        public IMessageSource MsgSource { get; }
        public List<IViewModelEventSubscription<IViewModel, IEvent>> EventSubscriptions { get;}
        public List<IViewModelEventPublication<IViewModel, IEvent>> EventPublications { get; }
        public List<IViewModelEventCommand<IViewModel, IEvent>> CommandInfo { get; }
        
        public Dictionary<string, dynamic> Commands { get; } = new Dictionary<string, dynamic>();

        public ISystemProcess Process { get; set; }
        public string Name { get; }
        public string Symbol { get; }
        public string Description { get; }

    }
}