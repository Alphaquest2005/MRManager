using System;
using System.Collections.Generic;
using SystemInterfaces;
using ViewModel.Interfaces;

namespace RevolutionEntities.ViewModels
{
    public class ViewModelInfo : IViewModelInfo
    {
        public ViewModelInfo(int processId, List<IEventSubscription<IViewModel, IEvent>> subscriptions, List<IEventPublication<IViewModel, IEvent>> publications, List<IEventCommand<IViewModel, IEvent>> commands, Type viewModelType)
        {
            ProcessId = processId;
            Subscriptions = subscriptions;
            ViewModelType = viewModelType;
            Commands = commands;
            Publications = publications;
        }

        public int ProcessId { get; }
        public List<IEventSubscription<IViewModel, IEvent>> Subscriptions { get; }
        public List<IEventPublication<IViewModel, IEvent>> Publications { get; }
        public List<IEventCommand<IViewModel, IEvent>> Commands { get; }
        public Type ViewModelType { get; }
    }
}