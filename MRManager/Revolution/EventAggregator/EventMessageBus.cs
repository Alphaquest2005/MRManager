using System;
using System.Diagnostics.Contracts;
using SystemInterfaces;
using log4netWrapper;

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

        public IObservable<TEvent> GetEvent<TEvent>(ISourceMessage caller) where TEvent : IMessage
        {
            Contract.Requires(caller != null);
            Logger.Log( LoggingLevel.Info ,"Caller:" + caller.Source + " | GetEvent : " + typeof(TEvent).ToString());
            return ea.GetEvent<TEvent>();
        }


        public void Publish<TEvent>(TEvent sampleEvent, ISourceMessage sender) where TEvent : IMessage
        {
            Contract.Requires(sender != null || sampleEvent != null);
            Logger.Log(LoggingLevel.Info, "Sender:" + sender.Source + " | PublishEvent : " + typeof(TEvent));
            ea.Publish(sampleEvent);
        }
    }
}
