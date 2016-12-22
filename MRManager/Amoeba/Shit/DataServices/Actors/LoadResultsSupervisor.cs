//using System;
//using Akka.Actor;
//using Akka.Routing;
//using DataInterfaces;
//using EventAggregator;
//using EventMessages;
//using Model.Interfaces;

//namespace DataServices.Actors
//{
//   public class LoadResultsSupervisor : ReceiveActor 
//   {
//       // Dictionary<int,IActorRef> childActors = new Dictionary<int, IActorRef>(); 
//       private IActorRef childActor;
//       public LoadResultsSupervisor(IDataContext dbContext)
//        {
//            childActor = Context.ActorOf(Props.Create<LoadResultsActor>(dbContext).WithRouter(new RoundRobinPool(1, new DefaultResizer(1, Environment.ProcessorCount, 1, .2, .3, .1, Environment.ProcessorCount))), "LoadEntitySetRouter");
//            EventMessageBus.Current.GetEvent<LoadResults<T>>().Subscribe(HandleLoadEntitySet);
            


//        }

//        private void HandleLoadEntitySet<T, TResults>(LoadResults<T,TResults> msg) where T : IEntity where TResults : class
//        {
//           childActor.Tell(msg);
//        }

       
//    }
//}
