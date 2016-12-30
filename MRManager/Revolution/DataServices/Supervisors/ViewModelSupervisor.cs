﻿using System;
using System.Linq;
using SystemInterfaces;
using SystemMessages;
using Akka.Actor;
using Akka.Routing;
using CommonMessages;
using EventAggregator;
using RevolutionData;
using StartUp.Messages;
using ViewMessages;
using ViewModel.Interfaces;

namespace DataServices.Actors
{
  
    public class ViewModelSupervisor : BaseSupervisor<ViewModelSupervisor>, IViewModelSupervisor
    {

        private IActorRef _childActor;
       

        public ViewModelSupervisor(ISystemProcess process)
        {

            _childActor = Context.ActorOf(Props.Create<ViewModelActor>(process).WithRouter(new RoundRobinPool(1, new DefaultResizer(1, Environment.ProcessorCount, 1, .2, .3, .1, Environment.ProcessorCount))),
                    "ViewModelActorEntityActor");

            EventMessageBus.Current.GetEvent<ISystemProcessStarted>(SourceMessage).Subscribe(x => HandleProcessViews(x));
            Receive<ISystemStarted>(x => HandleProcessViews(x));
             EventMessageBus.Current.Publish(new ServiceStarted<IViewModelSupervisor>(this,process, SourceMessage), SourceMessage);
        }

        private void HandleProcessViews(IProcessSystemMessage pe)
        {
            foreach (var v in ProcessViewModels.ProcessViewModelInfos.Where(x => x.ProcessId == pe.Process.Id))
            {
               PublishViewModel(v, pe);
            }
        }

        public void PublishViewModel(IViewModelInfo viewModelInfo, IProcessSystemMessage pe)
        {
            var msg = new LoadViewModel(viewModelInfo, pe.Process, SourceMessage);
            _childActor.Tell(msg);
        }
    }

}