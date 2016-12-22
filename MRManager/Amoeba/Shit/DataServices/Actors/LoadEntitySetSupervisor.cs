using System;
using SystemMessages;
using Akka.Actor;
using Akka.Routing;
using CommonMessages;
using DataInterfaces;
using EventAggregator;
using EventMessages;

namespace DataServices.Actors
{
   public class LoadEntitySetSupervisor<T> : ReceiveActor where T : IEntity
    {
       // Dictionary<int,IActorRef> childActors = new Dictionary<int, IActorRef>(); 
       private IActorRef childActor;

        MessageSource msgSource => new MessageSource(this.ToString());
        public LoadEntitySetSupervisor(IDataContext dbContext)
        {
            
            childActor = Context.ActorOf(Props.Create<LoadEntitySetActor<T>>(dbContext).WithRouter(new RoundRobinPool(1, new DefaultResizer(1, Environment.ProcessorCount, 1, .2, .3, .1, Environment.ProcessorCount))), "LoadEntitySetRouter");
            EventMessageBus.Current.GetEvent<LoadEntitySet<T>>(msgSource).Subscribe(HandleLoadEntitySet);
            EventMessageBus.Current.GetEvent<LoadEntitySetWithFilter<T>>(msgSource).Subscribe(HandleLoadEntitySet);
            EventMessageBus.Current.GetEvent<LoadEntitySetWithFilterWithIncludes<T>>(msgSource).Subscribe(HandleLoadEntitySetWithIncludes);
            
            EventMessageBus.Current.Publish(new LoadEntitySetDataServiceStarted<T>(msgSource), msgSource);

        }
        private void HandleLoadEntitySet(LoadEntitySet<T> msg)
        {
            childActor.Tell(msg);
        }
  
        private void HandleLoadEntitySet(LoadEntitySetWithFilter<T> msg)
        {
            childActor.Tell(msg);
        }
        private void HandleLoadEntitySetWithIncludes(LoadEntitySetWithFilterWithIncludes<T> msg)
        {
           childActor.Tell(msg);
        }

    }
}
