using System;
using System.Linq;
using SystemInterfaces;
using Akka.Actor;
using Akka.Routing;
using CommonMessages;
using EventAggregator;
using EventMessages.Events;
using RevolutionData;
using RevolutionEntities.Process;
using StartUp.Messages;
using ViewMessages;
using ViewModel.Interfaces;

namespace DataServices.Actors
{
  
    public class ViewModelSupervisor : BaseSupervisor<ViewModelSupervisor>, IViewModelSupervisor
    {

        private IActorRef _childActor;
        private IUntypedActorContext ctx = null;

        public ViewModelSupervisor(ISystemProcess process)
        {
            ctx = Context;
            _childActor = ctx.ActorOf(Props.Create<ViewModelActor>(process).WithRouter(new RoundRobinPool(1, new DefaultResizer(1, Environment.ProcessorCount, 1, .2, .3, .1, Environment.ProcessorCount))),
                    "ViewModelActorEntityActor");

            EventMessageBus.Current.GetEvent<ISystemProcessStarted>(Source).Subscribe(x => HandleProcessViews(x));
            
            Receive<ISystemStarted>(x => HandleProcessViews(x));

            EventMessageBus.Current.GetEvent<IServiceStarted<IViewModelService>>(Source).Subscribe(x =>
            {
                EventMessageBus.Current.Publish(new ServiceStarted<IViewModelSupervisor>(this,new StateEventInfo(process.Id, RevolutionData.Context.Actor.Events.ActorStarted), process, Source), Source);
            });

             
        }

       


        private void HandleProcessViews(IProcessSystemMessage pe)
        {
            try
            {
                foreach (var v in ProcessViewModels.ProcessViewModelInfos.Where(x => x.ProcessId == pe.Process.Id))
                {
                    var msg = new LoadViewModel(v,new StateCommandInfo(pe.Process.Id, RevolutionData.Context.ViewModel.Commands.LoadViewModel), pe.Process, Source);
                    //TODO: Some strange reason this thing not executing second time---process actor closed maybe
                    //_childActor.Tell(msg);
                    EventMessageBus.Current.Publish(msg, Source);
                    
                }
            }
            catch (Exception ex)
            {
                //todo: need  to fix this
                PublishProcesError(pe, ex, pe.GetType());
            }

        }
    }

}