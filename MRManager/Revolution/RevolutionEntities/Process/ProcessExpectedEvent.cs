using System;
using CommonMessages;

namespace RevolutionEntities.Process
{
    public class ProcessExpectedEvent
    {
        public int ProcessId { get; }
        public Type EventType { get; }
        public Func<SystemProcessMessage, bool> EventPredicate { get; }

        public ProcessExpectedEvent(int processId, Type eventType, Func<SystemProcessMessage, bool> eventPredicate)
        {
            ProcessId = processId;
            EventType = eventType;
            EventPredicate = eventPredicate;
        }
    }
}
