using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using SystemInterfaces;
using Utilities;

namespace RevolutionEntities.Process
{
    public class ProcessExpectedEvent : IProcessExpectedEvent
    {
        public int ProcessId { get; }
       

        public bool Raised { get; private set; }

        public Type EventType { get; }
        public Func<IProcessSystemMessage, bool> EventPredicate { get; }

        public ProcessExpectedEvent(int processId, Type eventType, Func<IProcessSystemMessage, bool> eventPredicate)
        {
            ProcessId = processId;
            EventType = eventType;
            EventPredicate = eventPredicate;
        }
    }

    public class ProcessExpectedEvent<TEvent>: ProcessExpectedEvent where TEvent:IProcessSystemMessage
    {
        public ProcessExpectedEvent(int processId, Func<TEvent, bool> eventPredicate) 
            : base(processId,
            typeof(TEvent),
            (Func<IProcessSystemMessage,bool>) eventPredicate.Convert(typeof(IProcessSystemMessage),typeof(bool)))
        {
        }
    }

    public static class ProcessExpectedEventExtensions
    {
        private static ConcurrentDictionary<IProcessExpectedEvent, bool> RaisedExpectedEvents { get; } = new ConcurrentDictionary<IProcessExpectedEvent, bool>();

        public static bool Validate(this IProcessExpectedEvent expectedEvent,IProcessSystemMessage msg)
        {
            var raised = expectedEvent.EventPredicate.Invoke(msg);
            if (!raised) return false;
            msg.ValidatedBy(expectedEvent);
            RaisedExpectedEvents.AddOrUpdate(expectedEvent, true, (k, c) => true);
            return true;
        }

        public static bool Raised(this IProcessExpectedEvent expectedEvent)
        {
            return RaisedExpectedEvents.ContainsKey(expectedEvent);
        }


    }


}
