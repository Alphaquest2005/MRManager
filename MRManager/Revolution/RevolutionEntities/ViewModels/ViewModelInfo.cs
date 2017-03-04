using System;
using System.Collections.Generic;
using SystemInterfaces;
using ViewModel.Interfaces;

namespace RevolutionEntities.ViewModels
{
    public class ViewModelInfo : IViewModelInfo
    {
        public ViewModelInfo(int processId, IViewInfo viewInfo, List<IViewModelEventSubscription<IViewModel, IEvent>> subscriptions, List<IViewModelEventPublication<IViewModel, IEvent>> publications, List<IViewModelEventCommand<IViewModel, IEvent>> commands, Type viewModelType, Type orientation, int priority)
        {
            ProcessId = processId;
            Subscriptions = subscriptions;
            ViewModelType = viewModelType;
            Orientation = orientation;
            Priority = priority;
            ViewInfo = viewInfo;
            Commands = commands;
            Publications = publications;
        }

        public int ProcessId { get; }
        public IViewInfo ViewInfo { get; }
        public List<IViewModelEventSubscription<IViewModel, IEvent>> Subscriptions { get; }
        public List<IViewModelEventPublication<IViewModel, IEvent>> Publications { get; }
        public List<IViewModelEventCommand<IViewModel, IEvent>> Commands { get; }
        public Type ViewModelType { get; }
        public Type Orientation { get; }
        public int Priority { get; }
    }

    public class ViewInfo : IViewInfo
    {
        public ViewInfo(string name, string symbol, string description)
        {
            Name = name;
            Symbol = symbol;
            Description = description;
        }

        public string Name { get; }
        public string Symbol { get; }
        public string Description { get; }
    }



}