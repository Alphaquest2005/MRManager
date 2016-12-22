using System;
using System.Collections.Generic;
using SystemInterfaces;
using ViewModel.Interfaces;

namespace RevolutionEntities.ViewModels
{
    public class ViewModelInfo : IViewModelInfo
    {
        public ViewModelInfo(int processId, List<IEventSubscription<IViewModel, IEvent>> viewEventSubscriptions, List<IEventPublication<IViewModel, IEvent>> viewEventPublications, Type viewModelType)
        {
            ProcessId = processId;
            ViewEventSubscriptions = viewEventSubscriptions;
            ViewModelType = viewModelType;
            ViewEventPublications = viewEventPublications;
        }

        public int ProcessId { get; }
        public List<IEventSubscription<IViewModel, IEvent>> ViewEventSubscriptions { get; }
        public List<IEventPublication<IViewModel, IEvent>> ViewEventPublications { get; }
        public Type ViewModelType { get; }
    }
}