using System.Collections.Generic;
using SystemInterfaces;
using SystemMessages;
using DataEntites;
using EventAggregator;
using EventMessages;
using Interfaces;
using RevolutionEntities.Process;

using ViewModel.Interfaces;

namespace RevolutionData
{
    public static class Processes
    {
        public static readonly IEnumerable<IProcessInfo> ProcessInfos = new List<IProcessInfo>()
        {
            //new Process(0,0, "Uknown Process", "Unknown Process", "Unknown"),
            new ProcessInfo(1, 0, "Starting System", "Prepare system for Intial Use", "Start"),
            new ProcessInfo<IUserSignIn>(2, 1, "User SignOn", "User Login", "User"),
            new ProcessInfo<IUserSignIn>(3, 2, "Load User Screen", "User Screen", "UserScreen")
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
                    new ProcessExpectedEvent (processId: 1, eventType: typeof (ISystemProcessStarted), eventPredicate: (e) => e != null),
                    new ProcessExpectedEvent (processId: 1, eventType: typeof (IViewModelCreated<IScreenViewModel>),eventPredicate: (e) => e != null),
                    new ProcessExpectedEvent (processId: 1, eventType: typeof (IViewModelLoaded<IMainWindowViewModel,IScreenViewModel>),eventPredicate: (e) => e != null),
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

                        var ps = new ProcessState<IUserSignIn>(actor.Process.Id, NullEntity<IUserSignIn>.Instance,ProcessStateInfo.WaitingOnUserName);
                        var psMsg = new ProcessStateMessage<IUserSignIn>(ps, actor.Process,actor.SourceMessage);
                        actor.ProcessStateMessages.AddOrUpdate(typeof(IUserSignIn),psMsg, (key,value) => ps);
                        EventMessageBus.Current.Publish(psMsg, actor.SourceMessage);
                    },
                events: new List<ProcessExpectedEvent>()
                {
                    new ProcessExpectedEvent (processId: 2, eventType: typeof (ISystemProcessStarted), eventPredicate: (e) => e != null),
                    new ProcessExpectedEvent (processId: 1, eventType: typeof (IViewModelCreated<ILoginViewModel>),eventPredicate: (e) => e != null),
                    new ProcessExpectedEvent (processId: 1, eventType: typeof (IViewModelLoaded<IScreenViewModel,ILoginViewModel>),eventPredicate: (e) => e != null),
                }),
            new EventAction<EntityFound<IUserSignIn>>(
                processId:2,
                action:
                    (actor, msg) =>
                    {
                        //TODO:Refactor how you deal with messages
                        var ps = new ProcessState<IUserSignIn>(actor.Process.Id, msg.Entity, new ProcessStateDetailedInfo($"Welcome {msg.Entity.Username}", "Please Enter your Password"));
                        var psMsg = new ProcessStateMessage<IUserSignIn>(ps, actor.Process,actor.SourceMessage);
                        actor.ProcessStateMessages.AddOrUpdate(typeof(IUserSignIn),psMsg, (key,value) => ps);
                        EventMessageBus.Current.Publish(psMsg, actor.SourceMessage);
                    },
                events: new List<ProcessExpectedEvent>()
                {
                    new ProcessExpectedEvent<EntityFound<IUserSignIn>> (processId: 2, eventPredicate: (e) => e.Entity != null)
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






    }
}