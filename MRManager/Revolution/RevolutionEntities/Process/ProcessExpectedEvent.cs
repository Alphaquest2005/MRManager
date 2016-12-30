using System;
using SystemInterfaces;
using Utilities;

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

    public class ProcessExpectedEvent<TEvent>: ProcessExpectedEvent where TEvent:IProcessSystemMessage
    {
        public ProcessExpectedEvent(int processId, Func<TEvent, bool> eventPredicate) 
            : base(processId,
            typeof(TEvent),
            (Func<IProcessSystemMessage,bool>) eventPredicate.Convert(typeof(IProcessSystemMessage),typeof(bool)))
        {
        }
    }
}
