using System;
using System.Collections.Generic;
using SystemInterfaces;
using ViewModel.Interfaces;

namespace RevolutionEntities.ViewModels
{
    public class ViewModelInfo : IViewModelInfo
    {
        public ViewModelInfo(int processId, List<IViewModelEventSubscription<IViewModel, IEvent>> subscriptions, List<IViewModelEventPublication<IViewModel, IEvent>> publications, List<IViewModelEventCommand<IViewModel, IEvent>> commands, Type viewModelType, Type orientation)
        {
            ProcessId = processId;
            Subscriptions = subscriptions;
            ViewModelType = viewModelType;
            Orientation = orientation;
            Commands = commands;
            Publications = publications;
        }

        public int ProcessId { get; }
        public List<IViewModelEventSubscription<IViewModel, IEvent>> Subscriptions { get; }
        public List<IViewModelEventPublication<IViewModel, IEvent>> Publications { get; }
        public List<IViewModelEventCommand<IViewModel, IEvent>> Commands { get; }
        public Type ViewModelType { get; }
        public Type Orientation { get; }
    }



}