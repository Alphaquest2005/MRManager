using System;
using System.Diagnostics.Contracts;
using SystemInterfaces;
using RevolutionLogger;

namespace EventAggregator
{
    public class EventMessageBus//: Reactive.EventAggregator.EventAggregator
    {
        static Reactive.EventAggregator.EventAggregator ea = new Reactive.EventAggregator.EventAggregator();
        static EventMessageBus()
        {
            Current = new EventMessageBus();
        }

        public static EventMessageBus Current { get; }

        public IObservable<TEvent> GetEvent<TEvent>(ISource caller) where TEvent : IProcessSystemMessage
        {
            Contract.Requires(caller != null);
            Logger.Log( LoggingLevel.Info ,$"Caller:{caller.SourceName} | GetEvent : {typeof(TEvent).Name}|| ProcessId-{caller.Process?.Id}");
            return ea.GetEvent<TEvent>();
        }


        public void Publish<TEvent>(TEvent sampleEvent, ISource sender) where TEvent : IProcessSystemMessage
        {
            Contract.Requires(sender != null || sampleEvent != null);
            Logger.Log(LoggingLevel.Info, $"Sender:{sender.SourceName} | PublishEvent : {typeof(TEvent).Name}| ProcessInfo:Status-{sampleEvent?.ProcessInfo?.State?.Status}|| ProcessId-{sampleEvent?.Process.Id}");
            ea.Publish(sampleEvent);
        }
    }
}
