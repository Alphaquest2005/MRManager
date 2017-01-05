using System;
using System.Collections.Concurrent;
using System.ComponentModel.Composition;
using SystemInterfaces;
using Akka.Actor;
using StartUp.Messages;

namespace Actor.Interfaces
{
    [InheritedExport]
    public interface IProcessActor:IAgent, IProcessService
    {
        ISystemProcess Process { get; }
        ConcurrentDictionary<Type, IProcessStateMessage<IEntityId>> ProcessStateMessages { get; }
        IActorRef ActorRef { get; }
        
    }
}
