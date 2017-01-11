using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using SystemInterfaces;
using Actor.Interfaces;
using ViewModel.Interfaces;

namespace RevolutionEntities.Process
{
    public class ComplexEventAction: IComplexEventAction
    {
        public ComplexEventAction(string key, int processId, IList<IProcessExpectedEvent> events, Type expectedMessageType, IProcessAction action, IProcessStateInfo processInfo)
        {
            ProcessId = processId;
            Events = events;
            ExpectedMessageType = expectedMessageType;
            ProcessInfo = processInfo;
            Key = key;
            Action = action;
        }

        public string Key { get; }
        public IList<IProcessExpectedEvent> Events { get; }
        public int ProcessId { get; }
        public Type ExpectedMessageType { get; }
        public IProcessStateInfo ProcessInfo { get; }
        public IProcessAction Action { get; }
    }

    public static class ComplexEventActionExtensions
    {
      
        //private static ConcurrentDictionary<IComplexEvent, bool> RaisedEvents { get; } = new ConcurrentDictionary<IComplexEvent, bool>();

        //private static ConcurrentDictionary<IComplexEvent, Action<IComplexEventParameters>> ComplexEventActions { get; } = new ConcurrentDictionary<IComplexEvent, Action<IComplexEventParameters>>();

        //private static Action<IComplexEvent> RaiseEventAction { get; } = x => RaisedEvents.AddOrUpdate(x, true, (k, c) => true);

        //public static ComplexEventAction RegisterAction(this ComplexEventAction complexEvent, Action<IComplexEventParameters> action)
        //{
        //    ComplexEventActions.AddOrUpdate(complexEvent, action, (k, v) => action);
        //    return complexEvent;
        //}

        //private static void CheckExpectedEvents(this IComplexEvent complexEvent, IComplexEventParameters paramArray)
        //{

        //    complexEvent.Events.ForEach(expectedEvent => paramArray.Messages.Where(x => x.GetType().GetInterfaces().Any(z => z == expectedEvent.EventType))
        //                                                                    .Where(x => expectedEvent.EventPredicate.Invoke(x))
        //                                                                    .Where(x => !x.IsValid())
        //                                                                    //
        //                                                       .ForEach(x => expectedEvent.Validate(x)));
           

        //}

        //public static void Execute(this IComplexEvent complexEvent, IComplexEventParameters paramArray)
        //{
        //    //CheckExpectedEvents(complexEvent,paramArray);
        //    if (!complexEvent.Events.All(x => x.Raised())) return;
        //    ComplexEventActions[complexEvent].Invoke(paramArray);
        //    RaiseEventAction(complexEvent);
        //    complexEvent.Events.ForEach(x => x.Drop());
            
        //}


        //public static bool Raised(this IComplexEvent complexEvent) 
        //{
        //    return RaisedEvents.ContainsKey(complexEvent);
        //}
    }
}