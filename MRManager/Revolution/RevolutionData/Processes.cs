using System;
using System.Collections.Generic;
using SystemInterfaces;
using SystemMessages;
using Actor.Interfaces;
using DataEntites;
using DomainMessages;
using EventAggregator;
using EventMessages;
using Interfaces;
using RevolutionEntities.Process;
using StartUp.Messages;
using ViewMessages;
using ViewModel.Interfaces;
using IProcessService = Actor.Interfaces.IProcessService;

namespace RevolutionData
{
    public static class Processes
    {
        public static readonly IEnumerable<IProcessInfo> ProcessInfos = new List<IProcessInfo>()
        {
            //new Process(0,0, "Uknown Process", "Unknown Process", "Unknown"),
            new ProcessInfo(1, 0, "Starting System", "Prepare system for Intial Use", "Start"),
            new ProcessInfo<ISignInInfo>(2, 1, "User SignOn", "User Login", "User"),
            new ProcessInfo<ISignInInfo>(3, 2, "Load User Screen", "User Screen", "UserScreen")
        };



        public static List<ComplexEventAction> ProcessComplexEvents = new List<ComplexEventAction>()
        {
            new ComplexEventAction(
                processId:1,
                events: new List<IProcessExpectedEvent>()
                {
                    new ProcessExpectedEvent (processId: 1, eventType: typeof (ISystemProcessStarted), eventPredicate: (e) => e != null, processInfo: new ProcessStateDetailedInfo("Process Started","First Step"),expectedSourceType: new SourceType(typeof(IProcessService))),
                    new ProcessExpectedEvent (processId: 1, eventType: typeof (IViewModelCreated<IScreenModel>),eventPredicate: (e) => e != null, processInfo: new ProcessStateDetailedInfo("ScreenView Created","This view contains all views"),expectedSourceType: new SourceType(typeof(IViewModelService) )),
                    new ProcessExpectedEvent (processId: 1, eventType: typeof (IViewModelLoaded<IMainWindowViewModel,IScreenModel>),eventPredicate: (e) => e != null, processInfo: new ProcessStateDetailedInfo("ScreenView Model loaded in MainWindowViewModel","Only ViewModel in Body"),expectedSourceType: new SourceType(typeof(IViewModelService) )),
                },
                expectedMessageType:typeof(ISystemProcessCompleted),
                processInfo:new ProcessStateDetailedInfo("Process Completed","Change Process State to Process Completed" ),
                action: new ProcessAction(action:(cp) => new SystemProcessCompleted(cp.Actor.Process, cp.Actor.Source),
                                           processInfo: new ProcessStateDetailedInfo("Process Completed","Create System Process Completed Message"),
                expectedSourceType: new SourceType(typeof(IComplexEventService))
                ),


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
                    //new ProcessExpectedEvent (processId: 2, eventType: typeof (IViewModelCreated<ILoginViewModel>),eventPredicate: (e) => e != null),
                    //new ProcessExpectedEvent (processId: 2, eventType: typeof (IViewModelLoaded<IScreenModel,IViewModel>),eventPredicate: (e) => e != null),
                }).RegisterAction(cp => {

                        var ps = new ProcessState<ISignInInfo>(cp.Actor.Process.Id, NullEntity<ISignInInfo>.Instance,ProcessStateInfo.WaitingOnUserName);
                        var psMsg = new ProcessStateMessage<ISignInInfo>(ps, cp.Actor.Process,cp.Actor.Source);
                        cp.Actor.ProcessStateMessages.AddOrUpdate(ps.Entity.GetType(),psMsg, (key,value) => psMsg);
                        EventMessageBus.Current.Publish(psMsg, cp.Actor.Source);
                    }),
            new ComplexEventAction(
                processId: 2,
                events: new List<IProcessExpectedEvent>()
                {
                    new ProcessExpectedEvent<IEntityViewWithChangesFound<ISignInInfo>> (processId: 2, eventPredicate: (e) => e.Entity != null && e.Changes.Count == 1 && e.Changes.ContainsKey(nameof(ISignInInfo.Usersignin)), expectedSourceType: TODO, processInfo: TODO)
                }).RegisterAction((cp) =>
                {
                    try
                    {
                        var ps = new ProcessState<ISignInInfo>(cp.Actor.Process.Id, cp.Msg.Entity,new ProcessStateDetailedInfo($"Welcome {cp.Msg.Entity.Usersignin}","Please Enter your Password"));
                        var psMsg = new ProcessStateMessage<ISignInInfo>(ps, cp.Actor.Process, cp.Actor.Source);
                        cp.Actor.ProcessStateMessages.AddOrUpdate(typeof (ISignInInfo), psMsg, (key, value) => psMsg);
                        EventMessageBus.Current.Publish(psMsg, cp.Actor.Source);
                    }
                    catch (Exception ex)
                    {

                        EventMessageBus.Current.Publish(new ProcessEventFailure(failedEventType: typeof(IProcessStateMessage<ISignInInfo>),
                        failedEventMessage: cp.Msg,
                        expectedEventType: typeof (ServiceStarted<>),
                        exception: ex,
                        source: cp.Actor.Source),cp.Actor.Source);
                    }

                }),
            new ComplexEventAction(
                processId: 2,
                events: new List<IProcessExpectedEvent>()
                {
                    new ProcessExpectedEvent<IEntityViewWithChangesFound<ISignInInfo>> (processId: 2, eventPredicate: (e) => e.Entity != null && e.Changes.Count == 2 && e.Changes.ContainsKey(nameof(ISignInInfo.Password)), expectedSourceType: TODO, processInfo: TODO)
                }).RegisterAction((cp) =>
                {
                    try
                    {
                        var ps = new ProcessState<ISignInInfo>(cp.Actor.Process.Id, cp.Msg.Entity,new ProcessStateDetailedInfo($"User: {cp.Msg.Entity.Usersignin} Validated", "User Validated"));
                        var psMsg = new ProcessStateMessage<ISignInInfo>(ps, cp.Actor.Process, cp.Actor.Source);
                        cp.Actor.ProcessStateMessages.AddOrUpdate(typeof (ISignInInfo), psMsg, (key, value) => psMsg);
                        EventMessageBus.Current.Publish(psMsg, cp.Actor.Source);
                        EventMessageBus.Current.Publish(new UserValidated(cp.Msg.Entity, cp.Actor.Process, cp.Actor.Source),cp.Actor.Source);
                    }
                    catch (Exception ex)
                    {

                        EventMessageBus.Current.Publish(new ProcessEventFailure(failedEventType: typeof(IProcessStateMessage<ISignInInfo>),
                        failedEventMessage: cp.Msg,
                        expectedEventType: typeof (ServiceStarted<>),
                        exception: ex,
                        source: cp.Actor.Source),cp.Actor.Source);
                    }

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
                                EventMessageBus.Current.Publish(ps.Value, cp.Actor.Source);
                            }
                    }),
        };






    }



}