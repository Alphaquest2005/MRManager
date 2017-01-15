using System;
using System.Diagnostics.Contracts;
using SystemInterfaces;
using RevolutionLogger;
using Newtonsoft.Json;

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

        public IObservable<TEvent> GetEvent<TEvent>(ISource caller) where TEvent : IMessage
        {
            Contract.Requires(caller != null);
            Logger.Log( LoggingLevel.Info ,"Caller:" + caller.SourceName + " | GetEvent : " + typeof(TEvent).ToString());
            return ea.GetEvent<TEvent>();
        }


        public void Publish<TEvent>(TEvent sampleEvent, ISource sender) where TEvent : IMessage
        {
            Contract.Requires(sender != null || sampleEvent != null);
            Logger.Log(LoggingLevel.Info, $"Sender:{sender.SourceName} | PublishEvent : {typeof(TEvent)} | Message : {JsonConvert.SerializeObject(sampleEvent.GetDerivedProperties(), Formatting.Indented, new JsonSerializerSettings() {ReferenceLoopHandling = ReferenceLoopHandling.Ignore})}");
            ea.Publish(sampleEvent);
        }
    }
}
