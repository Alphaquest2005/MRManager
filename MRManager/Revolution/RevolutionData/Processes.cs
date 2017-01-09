using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using SystemInterfaces;
using SystemMessages;
using Actor.Interfaces;
using DataEntites;
using Domain.Interfaces;
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
                action:ProcessActions.UserNameFound,
                processInfo:new ProcessStateDetailedInfo("Welcome User","")),
            new ComplexEventAction(
                key:"203",
                processId: 2,
                events: new List<IProcessExpectedEvent>()
                {
                    new ProcessExpectedEvent<IEntityViewWithChangesFound<ISignInInfo>> (processId: 2, eventPredicate: (e) => e.Entity != null && e.Changes.Count == 2 && e.Changes.ContainsKey(nameof(ISignInInfo.Password)), processInfo: new ProcessStateDetailedInfo("User Found with Both Username and Password",""), expectedSourceType: new SourceType(typeof(IEntityViewRepository)), key: "ValidatedUser")
                },
                expectedMessageType:typeof(IProcessStateMessage<ISignInInfo>),
                action:ProcessActions.SetProcessStatetoValidatedUser,
                processInfo: new ProcessStateDetailedInfo("Set User Validated ProcessState","")),
            new ComplexEventAction(
                key:"204",
                processId: 2,
                events: new List<IProcessExpectedEvent>()
                {
                    
                },
                expectedMessageType:typeof(IUserValidated),
                processInfo:new ProcessStateDetailedInfo("Inform Domain User Validated",""),
                action: ProcessActions.UserValidated),
            
           
        };

        
    }


}