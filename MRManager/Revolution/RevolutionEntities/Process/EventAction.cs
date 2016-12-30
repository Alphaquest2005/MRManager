using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;
using Actor.Interfaces;
using CommonMessages;
using Utilities;

namespace RevolutionEntities.Process
{
    public class EventAction : IEventAction
    {
        public EventAction(int processId, Action<IProcessActor, IProcessSystemMessage> action,
            IList<IProcessExpectedEvent> events)
        {
            Events = events;
            ProcessId = processId;
            Action = action;
        }

        public int ProcessId { get; }
        public IList<IProcessExpectedEvent> Events { get; }
        public Action<IProcessActor, IProcessSystemMessage> Action { get; }
        public bool Raised { get; set; } = false;
    }

    public class EventAction<TEvent> : EventAction, IEventAction<TEvent> where TEvent : IProcessSystemMessage
    {
        public EventAction(int processId, Action<IProcessActor, TEvent> action, IList<IProcessExpectedEvent> events) : base(processId, (Action<IProcessActor, IProcessSystemMessage>)action.Convert(typeof(IProcessActor), typeof(IProcessSystemMessage)), events)
        {
        }
    }
}
