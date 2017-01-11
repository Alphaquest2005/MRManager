using System;
using SystemInterfaces;
using Common;
using EventAggregator;
using EventMessages;
using RevolutionEntities.Process;
using Utilities;

namespace EFRepository
{
    public class BaseRepository<TRepository>:IProcessSource
    {
        public ISystemSource Source => new Source(Guid.NewGuid(), $"EntityRepository:<{typeof(BaseRepository<TRepository>).GetFriendlyName()}>", new SourceType(typeof(BaseRepository<TRepository>)), new MachineInfo(Environment.MachineName, Environment.ProcessorCount));
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