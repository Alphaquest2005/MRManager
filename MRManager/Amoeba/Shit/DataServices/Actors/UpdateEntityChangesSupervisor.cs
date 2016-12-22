using System;
using System.Collections.Generic;
using Akka.Actor;
using Akka.Routing;
using CommonMessages;
using DataInterfaces;
using EventAggregator;
using EventMessages;

namespace DataServices.Actors
{
   public class UpdateEntityChangesSupervisor<T> : ReceiveActor where T : IEntity
    {
       readonly Dictionary<int,IActorRef> childActors = new Dictionary<int, IActorRef>(); 
       private readonly IActorRef childActor;
       public UpdateEntityChangesSupervisor(IDataContext dbContext)
        {
           childActor = Context.ActorOf(Props.Create<UpdateEntityChangesActor<T>>(dbContext).WithRouter(new RoundRobinPool(1,new DefaultResizer(1,Environment.ProcessorCount,1,.2,.3,.1,Environment.ProcessorCount))), "UpdateEntityChangesActor");
            EventMessageBus.Current.GetEvent<EntityChanges<T>>(new MessageSource(this.ToString())).Subscribe(UpdateEntityChanges);

        }

        private void UpdateEntityChanges(EntityChanges<T> msg)
        {
           childActor.Tell(msg);
        }
    }
}
