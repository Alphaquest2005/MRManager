using System;
using SystemInterfaces;
using Akka.Actor;
using Common;
using CommonMessages;
using EventAggregator;
using EventMessages;
using RevolutionEntities.Process;
using Utilities;

namespace DataServices.Actors
{
    public class BaseSupervisor<T> : ReceiveActor, IProcessSource 
    {
      public ISystemSource Source => new Source(Guid.NewGuid(),$"Supervisor:{typeof(T).GetFriendlyName()}",new SourceType(typeof(BaseSupervisor<T>)), new MachineInfo(Environment.MachineName, Environment.ProcessorCount));

        internal void PublishProcesError(IProcessSystemMessage msg, Exception ex, Type expectedMessageType)
        {
            var outMsg = new ProcessEventFailure(failedEventType: msg.GetType(),
                failedEventMessage: msg,
                expectedEventType: expectedMessageType,
                exception: ex,
                source: Source, processInfo: new StateEventInfo(msg.Process.Id, RevolutionData.Context.Process.Events.Error));

            EventMessageBus.Current.Publish(outMsg, Source);
           
        }
    }
}