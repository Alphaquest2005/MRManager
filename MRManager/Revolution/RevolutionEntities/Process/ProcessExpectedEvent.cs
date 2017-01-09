﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using SystemInterfaces;
using Utilities;

namespace RevolutionEntities.Process
{
    public class ProcessExpectedEvent : IProcessExpectedEvent
    {
        public int ProcessId { get; }
        public IProcessStateDetailedInfo ProcessInfo { get; }
        public ISource ExpectedSource { get; }
        public ISourceType ExpectedSourceType { get; }

        public Type EventType { get; }
        public string Key { get; }
        public Func<IProcessSystemMessage, bool> EventPredicate { get; }

        public ProcessExpectedEvent(int processId, Type eventType, Func<IProcessSystemMessage, bool> eventPredicate, IProcessStateDetailedInfo processInfo, ISourceType expectedSourceType)
        {
            ProcessId = processId;
            EventType = eventType;
            EventPredicate = eventPredicate;
            ProcessInfo = processInfo;
            ExpectedSourceType = expectedSourceType;
        }

       
    }

    public class SourceType:ISourceType
    {
        public SourceType(Type sourceType)
        {
            Source_Type = sourceType;
        }

        public Type Source_Type { get; }
    }


    public class ProcessExpectedEvent<TEvent>: ProcessExpectedEvent where TEvent:IProcessSystemMessage
    {
        public ProcessExpectedEvent(int processId, Func<TEvent, bool> eventPredicate, IProcessStateDetailedInfo processInfo, ISourceType expectedSourceType) 
            : base(processId,
            typeof(TEvent),
            (Func<IProcessSystemMessage,bool>) eventPredicate.Convert(typeof(IProcessSystemMessage),typeof(bool)),processInfo, expectedSourceType)
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

        public static bool Drop(this IProcessExpectedEvent expectedEvent)
        {
            if (!RaisedExpectedEvents.ContainsKey(expectedEvent)) return true;
            bool wasDropped;
            RaisedExpectedEvents.TryRemove(expectedEvent, out wasDropped);
            return wasDropped;
        }
    }


}
