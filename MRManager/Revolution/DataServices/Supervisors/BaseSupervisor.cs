using System;
using SystemInterfaces;
using Akka.Actor;
using Common;
using CommonMessages;
using RevolutionEntities.Process;
using Utilities;

namespace DataServices.Actors
{
    public class BaseSupervisor<T> : ReceiveActor, IProcessSource 
    {
      public ISystemSource Source => new Source(Guid.NewGuid(),$"Supervisor:{typeof(T).GetFriendlyName()}", new MachineInfo(Environment.MachineName, Environment.ProcessorCount));


    }
}