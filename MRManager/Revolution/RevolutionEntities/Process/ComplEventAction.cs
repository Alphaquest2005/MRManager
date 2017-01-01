using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using SystemInterfaces;
using Actor.Interfaces;
using NHibernate.Util;

namespace RevolutionEntities.Process
{
    public class ComplexEventParameters : IComplexEventParameters
    {
        public ComplexEventParameters(IProcessActor actor, IList<IProcessSystemMessage> messages, dynamic msg)
        {
            Actor = actor;
            Messages = messages;
            Msg = msg;
        }

        public IProcessActor Actor { get; }
        public IList<IProcessSystemMessage> Messages { get; }
        public dynamic Msg { get; }
    }
    public class ComplexEventAction: IComplexEvent
    {
        public ComplexEventAction(int processId, IList<IProcessExpectedEvent> events)
        {
            ProcessId = processId;
            Events = events;
        }

        public IList<IProcessExpectedEvent> Events { get; }
        public int ProcessId { get; }
    }

    public static class ComplexEventActionExtensions
    {
        private static ConcurrentDictionary<IComplexEvent, bool> RaisedEvents { get; } = new ConcurrentDictionary<IComplexEvent, bool>();

        private static ConcurrentDictionary<IComplexEvent, Action<IComplexEventParameters>> ComplexEventActions { get; } = new ConcurrentDictionary<IComplexEvent, Action<IComplexEventParameters>>();

        private static Action<IComplexEvent> RaiseEventAction { get; } = x => RaisedEvents.AddOrUpdate(x, true, (k, c) => true);

        public static ComplexEventAction RegisterAction(this ComplexEventAction complexEvent, Action<IComplexEventParameters> action)
        {
            ComplexEventActions.AddOrUpdate(complexEvent, action, (k, v) => action);
            return complexEvent;
        }

        private static void CheckExpectedEvents(this IComplexEvent complexEvent, IComplexEventParameters paramArray)
        {
            complexEvent.Events.ForEach(expectedEvent => paramArray.Messages.Where(x => x.GetType() == expectedEvent.EventType && !x.IsValid())
                                                               .ForEach(x => expectedEvent.Validate(x)));
            complexEvent.Execute(paramArray);

        }

        public static void Execute(this IComplexEvent complexEvent, IComplexEventParameters paramArray)
        {
            CheckExpectedEvents(complexEvent,paramArray);
            if (!complexEvent.Events.All(x => x.Raised())) return;
            ComplexEventActions[complexEvent].Invoke(paramArray);
            RaiseEventAction(complexEvent);
        }


        public static bool Raised(this IComplexEvent complexEvent) 
        {
            return RaisedEvents.ContainsKey(complexEvent);
        }
    }
}