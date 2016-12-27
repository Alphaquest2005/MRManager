using System;
using Akka.Actor;
using CommonMessages;
using RevolutionEntities.Process;

namespace DataServices.Actors
{
    public class BaseSupervisor<T> : ReceiveActor 
    {
      protected SourceMessage SourceMessage => new SourceMessage(new MessageSource(this.ToString()), new MachineInfo(Environment.MachineName, Environment.ProcessorCount));


    }
}