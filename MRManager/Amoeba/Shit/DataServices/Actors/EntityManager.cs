using Akka.Actor;
using DataInterfaces;

namespace DataServices.Actors
{
   public class EntityManager<T> : ReceiveActor where T : class, IEntity
    {
       public EntityManager()
       {
            Context.ActorOf(Props.Create<GetEntityByIdSupervisor<T>>(), "GetEntityByIdSupervisor");
            Context.ActorOf(Props.Create<UpdateEntityChangesSupervisor<T>>(), "UpdateEntityChangesSupervisor");
            Context.ActorOf(Props.Create<LoadHeaderInfoSupervisor<T>>(), "LoadHeaderInfoSupervisor");

        }

    }
}
