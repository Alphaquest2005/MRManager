using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;
using Akka.Actor;
using CommonMessages;

namespace Actor.Interfaces
{
    [InheritedExport]
    public interface IProcessActor:IAgent
    {
        ISystemProcess Process { get; }
        ConcurrentDictionary<Type, dynamic> ProcessStateMessages { get; }
        IActorRef ActorRef { get; }
        
    }
}
