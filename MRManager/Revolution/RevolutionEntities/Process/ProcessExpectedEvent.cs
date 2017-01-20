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
        public IProcessStateInfo ProcessInfo { get; }
        public ISourceType ExpectedSourceType { get; }

        public Type EventType { get; }
        public string Key { get; }
        public Func<IProcessSystemMessage, bool> EventPredicate { get; }

        public ProcessExpectedEvent(string key,int processId, Type eventType, Func<IProcessSystemMessage, bool> eventPredicate, IProcessStateInfo processInfo, ISourceType expectedSourceType)
        {
            ProcessId = processId;
            EventType = eventType;
            EventPredicate = eventPredicate;
            ProcessInfo = processInfo;
            ExpectedSourceType = expectedSourceType;
            Key = key;
        }

        public ProcessExpectedEvent(string key,int processId, Func<IProcessSystemMessage, bool> eventPredicate, ProcessExpectedEventInfo eventInfo)
        {
            ProcessId = processId;
            EventPredicate = eventPredicate;
            Key = key;
            EventType = eventInfo.EventType;
            ProcessInfo = eventInfo.ProcessInfo;
            ExpectedSourceType = eventInfo.ExpectedSourceType;
        }
    }




    public class ProcessExpectedEvent<TEvent>: ProcessExpectedEvent where TEvent:IProcessSystemMessage
    {
        public ProcessExpectedEvent(string key, int processId, Func<TEvent, bool> eventPredicate, IProcessStateInfo processInfo, ISourceType expectedSourceType) 
            : base(key,processId,
            typeof(TEvent),
            (Func<IProcessSystemMessage,bool>) eventPredicate.Convert(typeof(IProcessSystemMessage),typeof(bool)),processInfo, expectedSourceType)
        {
        }
    }

    public static class ProcessExpectedEventExtensions
    {
       // private static ConcurrentDictionary<IProcessExpectedEvent, bool> RaisedExpectedEvents { get; } = new ConcurrentDictionary<IProcessExpectedEvent, bool>();

        public static bool Validate(this IProcessExpectedEvent expectedEvent,IProcessSystemMessage msg)
        {
            var raised = expectedEvent.EventPredicate.Invoke(msg);
            if (!raised) return false;
            msg.ValidatedBy(expectedEvent);
           // RaisedExpectedEvents.AddOrUpdate(expectedEvent, true, (k, c) => true);
            return true;
        }

        //public static bool Raised(this IProcessExpectedEvent expectedEvent)
        //{
        //    return RaisedExpectedEvents.ContainsKey(expectedEvent);
        //}

        //public static bool Drop(this IProcessExpectedEvent expectedEvent)
        //{
        //    if (!RaisedExpectedEvents.ContainsKey(expectedEvent)) return true;
        //    bool wasDropped;
        //    RaisedExpectedEvents.TryRemove(expectedEvent, out wasDropped);
        //    return wasDropped;
        //}
    }


}
