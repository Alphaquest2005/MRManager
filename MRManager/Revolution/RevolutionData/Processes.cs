using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
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



        public static List<IComplexEventAction> ProcessComplexEvents = new List<IComplexEventAction>()
        {
            new ComplexEventAction(
                key:"101",
                processId:1,
                events: new List<IProcessExpectedEvent>()
                {
                    new ProcessExpectedEvent (key: "ProcessStarted", processId: 1, eventType: typeof (ISystemProcessStarted), eventPredicate: (e) => e != null, processInfo: new ProcessStateDetailedInfo("Process Started","First Step"),expectedSourceType: new SourceType(typeof(IProcessService))),
                    new ProcessExpectedEvent (key: "ViewCreated", processId: 1, eventType: typeof (IViewModelCreated<IScreenModel>),eventPredicate: (e) => e != null, processInfo: new ProcessStateDetailedInfo("ScreenView Created","This view contains all views"),expectedSourceType: new SourceType(typeof(IViewModelService) )),
                    new ProcessExpectedEvent (key: "ViewLoaded", processId: 1, eventType: typeof (IViewModelLoaded<IMainWindowViewModel,IScreenModel>),eventPredicate: (e) => e != null, processInfo: new ProcessStateDetailedInfo("ScreenView Model loaded in MainWindowViewModel","Only ViewModel in Body"),expectedSourceType: new SourceType(typeof(IViewModelService) )),
                },
                expectedMessageType:typeof(ISystemProcessCompleted),
                processInfo:new ProcessStateDetailedInfo("Process Completed","Change Process State to Process Completed" ),
                action: ProcessActions.ProcessCompleted),


            new ComplexEventAction(
                key:"102",
                processId:1,
                events: new List<IProcessExpectedEvent>()
                {
                    new ProcessExpectedEvent (key: "ProcessCompleted",processId: 1, eventType: typeof (ISystemProcessCompleted), eventPredicate: (e) => e != null, processInfo: new ProcessStateDetailedInfo("System Initalization complete","Should Close Process Actor"), )
                }, 
                expectedMessageType: typeof(ISystemProcessTerminated),
                action: ProcessActions.ShutActorDown), 
            new ComplexEventAction(
                key: "201",
                processId:2,
                events: new List<IProcessExpectedEvent>()
                {
                    new ProcessExpectedEvent (key: "ProcessStarted", processId: 2, eventPredicate: (e) => e != null,eventInfo: ProcessExpectedEventInfos.ProcessStarted)
                    
                },
                expectedMessageType: typeof(IProcessStateMessage<ISignInInfo>),
                action: ProcessActions.IntializeSigninProcessState,
                processInfo: new ProcessStateDetailedInfo("Intialize Process Start for Signin process","")),
            new ComplexEventAction(
                key:"202",
                processId: 2,
                events: new List<IProcessExpectedEvent>()
                {
                    new ProcessExpectedEvent<IEntityViewWithChangesFound<ISignInInfo>> (
                        key:"UserNameFound",
                        processId: 2, eventPredicate: (e) => e.Entity != null && e.Changes.Count == 1 && e.Changes.ContainsKey(nameof(ISignInInfo.Usersignin)), expectedSourceType: new SourceType(typeof(IEntityViewRepository)), processInfo: new ProcessStateDetailedInfo("User Name Found","Not Verified"))
                },
                expectedMessageType:typeof(IProcessStateMessage<ISignInInfo>),
                action:).RegisterAction((cp) =>
                {
                    try
                    {
                        
                       
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
                    new ProcessExpectedEvent<IEntityViewWithChangesFound<ISignInInfo>> (processId: 2, eventPredicate: (e) => e.Entity != null && e.Changes.Count == 2 && e.Changes.ContainsKey(nameof(ISignInInfo.Password)), processInfo: TODO, expectedSourceType: TODO, key: TODO)
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