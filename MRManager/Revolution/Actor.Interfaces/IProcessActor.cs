using System;
using System.Collections.Concurrent;
using System.ComponentModel.Composition;
using SystemInterfaces;
using Akka.Actor;
using StartUp.Messages;

namespace Actor.Interfaces
{
    
    public interface IProcessService:IAgent, IService<IProcessService>
    {
        ISystemProcess Process { get; }
        ConcurrentDictionary<Type, IProcessStateMessage<IEntityId>> ProcessStateMessages { get; }
        IActorRef ActorRef { get; }
        
    }
}
