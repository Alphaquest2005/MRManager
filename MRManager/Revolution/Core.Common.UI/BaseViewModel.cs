using System.Collections.Generic;
using SystemInterfaces;
using CommonMessages;
using ReactiveUI;


namespace Core.Common.UI
{
    public abstract class BaseViewModel<TViewModel> : ReactiveObject, IViewModel
    {
        public MessageSource MsgSource => new MessageSource(this.ToString());


        protected BaseViewModel(ISystemProcess process, List<IEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IEventPublication<IViewModel, IEvent>> eventPublications, List<IViewCommand<IViewModel, IEvent>> commands)
        {
            Process = process;
            EventSubscriptions = eventSubscriptions;
            EventPublications = eventPublications;
            Commands = commands;
            Name = process.Name;
            Description = process.Description;
            Symbol = process.Symbol;

            this.WireEvents();
        }

        public List<IEventSubscription<IViewModel, IEvent>> EventSubscriptions { get;}
        public List<IEventPublication<IViewModel, IEvent>> EventPublications { get; }
        public List<IViewCommand<IViewModel, IEvent>> Commands { get; }

        public ISystemProcess Process { get; set; }
        public string Name { get; }
        public string Symbol { get; }
        public string Description { get; }




    }
}