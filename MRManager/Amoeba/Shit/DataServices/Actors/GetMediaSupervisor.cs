using System;
using Akka.Actor;
using Akka.Routing;
using CommonMessages;
using DataInterfaces;
using EventAggregator;
using EventMessages;

namespace DataServices.Actors
{
    public class GetMediaSupervisor : ReceiveActor 
    {
        // Dictionary<int,IActorRef> childActors = new Dictionary<int, IActorRef>(); 
        private IActorRef childActor;
        public GetMediaSupervisor(IDataContext dbContext)
        {
            try
            {
                childActor = Context.ActorOf(Props.Create<GetMediaActor>().WithRouter(new RoundRobinPool(1, new DefaultResizer(1, Environment.ProcessorCount, 1, .2, .3, .1, Environment.ProcessorCount))), "GetMediaRouter");
                 EventMessageBus.Current.GetEvent<GetMedia>(new MessageSource(this.ToString())).Subscribe(HandleGetMedia);
            }
            catch (Exception)
            {
                
                throw;
            }
          
        }

        private void HandleGetMedia(GetMedia msg) 
        {
            childActor.Tell(msg);
        }


    }
}
