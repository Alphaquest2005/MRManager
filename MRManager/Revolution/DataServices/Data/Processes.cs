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
using EventMessages;
using RevolutionEntities.Process;
using Utilities;
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
                            (actor, msg) =>
                                EventMessageBus.Current.Publish(new SystemProcessCompleted(actor.Process, actor.SourceMessage),
                                    actor.SourceMessage),
                        events: new List<ProcessExpectedEvent>()
                        {
                            new ProcessExpectedEvent (processId: 1, eventType: typeof (SystemProcessStarted), eventPredicate: (e) => e != null),
                            new ProcessExpectedEvent (processId: 1, eventType: typeof (ViewModelCreated<ScreenModel>),eventPredicate: (e) => e != null),
                            new ProcessExpectedEvent (processId: 1, eventType: typeof (ViewLoadedViewModel<ScreenModel>),eventPredicate: (e) => e != null),
                        }),
                    new EventAction(
                        processId:1,
                        action:
                            (actor, msg) => {}, //actor.ActorRef().GracefulStop(TimeSpan.FromSeconds(10)),TODO: figure out way to close actor without messing up command not working
                        events: new List<ProcessExpectedEvent>()
                        {
                            new ProcessExpectedEvent (processId: 1, eventType: typeof (SystemProcessCompleted), eventPredicate: (e) => e != null)
                        }),
                    new EventAction(
                        processId:2,
                        action:
                            (actor, msg) =>
                            {

                                var ps = new ProcessState<UserSignIn>(actor.Process.Id, NullEntity<UserSignIn>.Instance,ProcessStateInfo.WaitingOnUserName);
                                var psMsg = new ProcessStateMessage<UserSignIn>(ps, actor.Process,actor.SourceMessage);
                                actor.ProcessStateMessages.AddOrUpdate(typeof(UserSignIn),psMsg, (key,value) => ps);
                                EventMessageBus.Current.Publish(psMsg, actor.SourceMessage);
                            },
                        events: new List<ProcessExpectedEvent>()
                        {
                            new ProcessExpectedEvent (processId: 2, eventType: typeof (SystemProcessStarted), eventPredicate: (e) => e != null)
                        }),
                    new EventAction<EntityFound<UserSignIn>>(
                        processId:2,
                        action:
                            (actor, msg) =>
                            {
                                //TODO:Refactor how you deal with messages
                                var ps = new ProcessState<UserSignIn>(actor.Process.Id, msg.Entity, new ProcessStateDetailedInfo($"Welcome {msg.Entity.Username}", "Please Enter your Password"));
                                var psMsg = new ProcessStateMessage<UserSignIn>(ps, actor.Process,actor.SourceMessage);
                                actor.ProcessStateMessages.AddOrUpdate(typeof(UserSignIn),psMsg, (key,value) => ps);
                                EventMessageBus.Current.Publish(psMsg, actor.SourceMessage);
                            },
                        events: new List<ProcessExpectedEvent>()
                        {
                            new ProcessExpectedEvent<EntityFound<UserSignIn>> (processId: 2, eventPredicate: (e) => e.Entity != null)
                        }),
                    new EventAction(
                        processId:2,
                        action:
                            (actor, msg) =>
                            {
                                foreach (var ps in actor.ProcessStateMessages)
                                {
                                    EventMessageBus.Current.Publish(ps.Value, actor.SourceMessage);
                                }
                                
                            },
                        events: new List<ProcessExpectedEvent>()
                        {
                            new ProcessExpectedEvent (processId: 2, eventType: typeof(RequestProcessState), eventPredicate: (e) => e != null)
                        }),
                };

        public class EventAction
        {
            public EventAction(int processId, Action<ProcessActor, IProcessSystemMessage> action, IList<ProcessExpectedEvent> events)
            {
                Events = events;
                ProcessId = processId;
                Action = action;
            }
            public int ProcessId { get; }
            public IList<ProcessExpectedEvent> Events { get; }
            public Action<ProcessActor, IProcessSystemMessage> Action { get; }
            public bool Raised { get; set; } = false;
        }

        public class EventAction<TEvent>:EventAction where TEvent:IProcessSystemMessage
        {
            public EventAction(int processId, Action<ProcessActor, TEvent> action, IList<ProcessExpectedEvent> events) : base(processId, (Action<ProcessActor, IProcessSystemMessage>)action.Convert(typeof(ProcessActor),typeof(IProcessSystemMessage)), events)
            {
            }
        }


    }

    public class ProcessStateInfo
    {
        public static ProcessStateDetailedInfo WaitingOnUserName { get; } = new ProcessStateDetailedInfo("Waiting for User Name", "Please Enter your User Name. If this is your First Time Login In please Contact the Receptionist for your user info.");
        //public static ProcessStateDetailedInfo<EntityFound<UserSignIn>> UserFound { get; } = new ProcessStateDetailedInfo<EntityFound<UserSignIn>>(msg, Func<EntityFound<UserSignIn>,string>, "Please Enter your User Name. If this is your First Time Login In please Contact the Receptionist for your user info.");

    }
}