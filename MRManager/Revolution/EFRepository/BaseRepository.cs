using System;
using SystemInterfaces;
using Common;
using EventAggregator;
using EventMessages;
using EventMessages.Events;
using RevolutionEntities.Process;
using RevolutionLogger;
using Utilities;

namespace EFRepository
{
    public class BaseRepository<TRepository>:IProcessSource
    {
        public static ISystemSource Source => new Source(Guid.NewGuid(), $"EntityRepository:<{typeof(BaseRepository<TRepository>).GetFriendlyName()}>", new SourceType(typeof(BaseRepository<TRepository>)), new SystemProcess(new Process(1, 0, "Starting System", "Prepare system for Intial Use", "", new Agent("System")), new MachineInfo(Environment.MachineName, Environment.ProcessorCount)), new MachineInfo(Environment.MachineName, Environment.ProcessorCount));
        internal static void PublishProcesError(IProcessSystemMessage msg, Exception ex, Type expectedMessageType)
        {
            var outMsg = new ProcessEventFailure(failedEventType: msg.GetType(),
                failedEventMessage: msg,
                expectedEventType: expectedMessageType,
                exception: ex,
                source: Source, processInfo: new StateEventInfo(msg.Process.Id, RevolutionData.Context.Process.Events.Error));
            Logger.Log(LoggingLevel.Error, $"Error:ProcessId:{msg.ProcessInfo.ProcessId}, ProcessStatus:{msg.ProcessInfo.State.Status}, ExceptionMessage: {ex.Message}|||| {ex.StackTrace}");
            EventMessageBus.Current.Publish(outMsg, Source);
        }

        ISystemSource IProcessSource.Source => BaseRepository<TRepository>.Source;
    }
}