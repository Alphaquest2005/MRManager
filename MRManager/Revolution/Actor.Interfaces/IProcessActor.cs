using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;
using Akka.Actor;
using CommonMessages;

namespace Actor.Interfaces
{
    public interface IProcessActor:IAgent
    {
        ISystemProcess Process { get; }
        ConcurrentDictionary<Type, dynamic> ProcessStateMessages { get; }
        IActorRef ActorRef { get; }
        
    }

    public interface IAgent : IUser
    {
        ISourceMessage SourceMessage { get; }
    }

}
