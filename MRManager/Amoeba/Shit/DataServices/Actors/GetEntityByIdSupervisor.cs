using System;
using Akka.Actor;
using Akka.Routing;
using CommonMessages;
using DataInterfaces;
using EventAggregator;
using EventMessages;

namespace DataServices.Actors
{
   public class GetEntityByIdSupervisor<T> : ReceiveActor where T : IEntity
    {
       // Dictionary<int,IActorRef> childActors = new Dictionary<int, IActorRef>(); 
       private IActorRef childActor;
       public GetEntityByIdSupervisor(IDataContext dbContext)
        {
            childActor = Context.ActorOf(Props.Create<GetEntityByIdActor<T>>(dbContext).WithRouter(new RoundRobinPool(1, new DefaultResizer(1, Environment.ProcessorCount, 1, .2, .3, .1, Environment.ProcessorCount))), "GetEntityByIdActor"); 
            EventMessageBus.Current.GetEvent<GetEntityById<T>>(new MessageSource(this.ToString())).Subscribe(HandleGetEntityById);
            
        }

        private void HandleGetEntityById(GetEntityById<T> msg)
        {
           childActor.Tell(msg);
        }
    }
}
