using Akka.Actor;
using CommonMessages;

namespace DataServices.Actors
{
    public class BaseSupervisor<T> : ReceiveActor 
    {
      protected MessageSource MsgSource => new MessageSource(this.ToString());


    }
}