﻿using System;
using CommonMessages;

namespace RevolutionEntities.Process
{
    public class ProcessExpectedEvent
    {
        public int ProcessId { get; }
        public Type EventType { get; }
        public Func<IProcessSystemMessage, bool> EventPredicate { get; }

        public ProcessExpectedEvent(int processId, Type eventType, Func<IProcessSystemMessage, bool> eventPredicate)
        {
            ProcessId = processId;
            EventType = eventType;
            EventPredicate = eventPredicate;
        }
    }
}
