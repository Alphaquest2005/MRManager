﻿using System.Collections.Generic;
using SystemInterfaces;
using SystemMessages;
using DataEntites;
using DomainMessages;
using EventAggregator;
using EventMessages;
using Interfaces;
using RevolutionEntities.Process;
using ViewMessages;
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



        public static List<ComplexEventAction> ProcessComplexEvents = new List<ComplexEventAction>()
        {
            new ComplexEventAction(
                processId:1,
                events: new List<IProcessExpectedEvent>()
                {
                    new ProcessExpectedEvent (processId: 1, eventType: typeof (ISystemProcessStarted), eventPredicate: (e) => e != null),
                    new ProcessExpectedEvent (processId: 1, eventType: typeof (IViewModelCreated<IScreenModel>),eventPredicate: (e) => e != null),
                    new ProcessExpectedEvent (processId: 1, eventType: typeof (IViewModelLoaded<IMainWindowViewModel,IScreenModel>),eventPredicate: (e) => e != null),
                }).RegisterAction((cp) => EventMessageBus.Current.Publish(new SystemProcessCompleted(cp.Actor.Process, cp.Actor.SourceMessage),cp.Actor.SourceMessage)),
            new ComplexEventAction(
                processId:1,
                events: new List<IProcessExpectedEvent>()
                {
                    new ProcessExpectedEvent (processId: 1, eventType: typeof (ISystemProcessCompleted), eventPredicate: (e) => e != null)
                }).RegisterAction((cp) => {}), //actor.ActorRef().GracefulStop(TimeSpan.FromSeconds(10)),TODO: figure out way to close actor without messing up command not working),
            new ComplexEventAction(
                processId:2,
                events: new List<IProcessExpectedEvent>()
                {
                    new ProcessExpectedEvent (processId: 2, eventType: typeof (ISystemProcessStarted), eventPredicate: (e) => e != null),
                    new ProcessExpectedEvent (processId: 2, eventType: typeof (IViewModelCreated<ILoginViewModel>),eventPredicate: (e) => e != null),
                    new ProcessExpectedEvent (processId: 2, eventType: typeof (IViewModelLoaded<IScreenModel,IViewModel>),eventPredicate: (e) => e != null),
                }).RegisterAction(cp => {

                        var ps = new ProcessState<IUserSignIn>(cp.Actor.Process.Id, NullEntity<IUserSignIn>.Instance,ProcessStateInfo.WaitingOnUserName);
                        var psMsg = new ProcessStateMessage<IUserSignIn>(ps, cp.Actor.Process,cp.Actor.SourceMessage);
                        cp.Actor.ProcessStateMessages.AddOrUpdate(ps.Entity.GetType(),ps, (key,value) => ps);
                        EventMessageBus.Current.Publish(psMsg, cp.Actor.SourceMessage);
                    }),
            new ComplexEventAction(
                processId: 2,
                events: new List<IProcessExpectedEvent>()
                {
                    new ProcessExpectedEvent<IEntityWithChangesFound<IUserSignIn>> (processId: 2, eventPredicate: (e) => e.Entity != null && e.Changes.Count == 1 && e.Changes.ContainsKey(nameof(IUserSignIn.Username)))
                }).RegisterAction((cp) =>
                {
                    var ps = new ProcessState<IUserSignIn>(cp.Actor.Process.Id, cp.Msg.Entity, new ProcessStateDetailedInfo($"Welcome {cp.Msg.Entity.Username}", "Please Enter your Password"));
                    var psMsg = new ProcessStateMessage<IUserSignIn>(ps, cp.Actor.Process,cp.Actor.SourceMessage);
                    cp.Actor.ProcessStateMessages.AddOrUpdate(typeof(IUserSignIn),psMsg, (key,value) => ps);
                    EventMessageBus.Current.Publish(psMsg, cp.Actor.SourceMessage);
                }),
            new ComplexEventAction(
                processId: 2,
                events: new List<IProcessExpectedEvent>()
                {
                    new ProcessExpectedEvent<IEntityWithChangesFound<IUserSignIn>> (processId: 2, eventPredicate: (e) => e.Entity != null && e.Changes.Count == 2 && e.Changes.ContainsKey(nameof(IUserSignIn.Password)))
                }).RegisterAction((cp) =>
                {
                    var ps = new ProcessState<IUserSignIn>(cp.Actor.Process.Id, cp.Msg.Entity, new ProcessStateDetailedInfo($"User: {cp.Msg.Entity.Username} Validated", "User Validated"));
                    var psMsg = new ProcessStateMessage<IUserSignIn>(ps, cp.Actor.Process,cp.Actor.SourceMessage);
                    cp.Actor.ProcessStateMessages.AddOrUpdate(typeof(IUserSignIn),psMsg, (key,value) => ps);
                    EventMessageBus.Current.Publish(psMsg, cp.Actor.SourceMessage);
                    EventMessageBus.Current.Publish(new UserValidated(cp.Msg.Entity, cp.Actor.Process, cp.Actor.SourceMessage), cp.Actor.SourceMessage);
                    // have to do it so cuz i dropping events 
                }),
            
            new ComplexEventAction(
                processId:2,
                events: new List<IProcessExpectedEvent>()
                {
                    new ProcessExpectedEvent (processId: 2, eventType: typeof(IRequestProcessState), eventPredicate: (e) => e != null)
                }).RegisterAction((cp) =>
                    {
                        foreach (var ps in cp.Actor.ProcessStateMessages)
                        {
                            EventMessageBus.Current.Publish(ps.Value, cp.Actor.SourceMessage);
                        }

                    }),
        };






    }
}