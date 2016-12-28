using System;
using System.Collections.Generic;
using SystemInterfaces;
using SystemMessages;
using Akka.Actor;
using CommonMessages;
using Core.Common.UI;
using DataEntites;
using EF.Entities;
using EventAggregator;
using RevolutionEntities.Process;
using ViewMessages;
using ViewModels;

namespace DataServices.Actors
{
    public static class Processes
    {
        public static readonly IEnumerable<IProcessInfo> ProcessInfos = new List<IProcessInfo>()
        {
            //new Process(0,0, "Uknown Process", "Unknown Process", "Unknown"),
            new ProcessInfo(1, 0, "Starting System", "Prepare system for Intial Use", "Start"),
            new ProcessInfo<UserSignIn>(2, 1, "User SignOn", "User Login", "User"),
            new ProcessInfo<UserSignIn>(3, 2, "Load User Screen", "User Screen", "UserScreen")
        };



        public static List<EventAction> ProcessComplexEvents = new List<EventAction>()
                {
                    new EventAction(
                        processId:1,
                        action:
                            (actor) =>
                                EventMessageBus.Current.Publish(new SystemProcessCompleted(actor.Process, actor.SourceMessage),
                                    actor.SourceMessage),
                        events: new List<ProcessExpectedEvent>()
                        {
                            new ProcessExpectedEvent (processId: 1, eventType: typeof (SystemProcessStarted), eventPredicate: (e) => e != null),
                            new ProcessExpectedEvent (processId: 1, eventType: typeof (ViewModelCreated<ScreenModel>),eventPredicate: (e) => e != null),
                            new ProcessExpectedEvent (processId: 1,eventType: typeof (ViewLoadedViewModel<ScreenModel>),eventPredicate: (e) => e != null),
                        }),
                    new EventAction(
                        processId:1,
                        action:
                            (actor) => actor.ActorRef().GracefulStop(TimeSpan.FromSeconds(10)),
                        events: new List<ProcessExpectedEvent>()
                        {
                            new ProcessExpectedEvent (processId: 1, eventType: typeof (SystemProcessCompleted), eventPredicate: (e) => e != null)
                        }),
                };

        public class EventAction
        {
            public EventAction(int processId, Action<ProcessActor> action, IList<ProcessExpectedEvent> events)
            {
                Events = events;
                ProcessId = processId;
                Action = action;
            }
            public int ProcessId { get; }
            public IList<ProcessExpectedEvent> Events { get; }
            public Action<ProcessActor> Action { get; }
            public bool Raised { get; set; } = false;
        }

        
    }
}