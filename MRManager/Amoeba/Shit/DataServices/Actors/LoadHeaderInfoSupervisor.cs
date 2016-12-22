using System;
using Akka.Actor;
using Akka.Routing;
using CommonMessages;
using DataInterfaces;
using EventAggregator;
using EventMessages;

namespace DataServices.Actors
{
    public class LoadHeaderInfoSupervisor<T> : ReceiveActor where T : class, IEntity
    {
        // Dictionary<int,IActorRef> childActors = new Dictionary<int, IActorRef>(); 
        private IActorRef childActor;
        public LoadHeaderInfoSupervisor(IDataContext dbContext)
        {
            childActor = Context.ActorOf(Props.Create<LoadHeaderInfoActor<T>>(dbContext).WithRouter(new RoundRobinPool(1, new DefaultResizer(1, Environment.ProcessorCount, 1, .2, .3, .1, Environment.ProcessorCount))), "LoadEntitySetRouter");
            EventMessageBus.Current.GetEvent<LoadHeaderInfo<T>>(new MessageSource(this.ToString())).Subscribe(HandleLoadHeaderInfo);



        }

        private void HandleLoadHeaderInfo(LoadHeaderInfo<T> msg) 
        {
            childActor.Tell(msg);
        }


    }
}
