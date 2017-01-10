using System.Collections.Generic;
using SystemInterfaces;
using Actor.Interfaces;
using Domain.Interfaces;
using Interfaces;
using RevolutionEntities.Process;
using StartUp.Messages;
using ViewModel.Interfaces;

namespace RevolutionData
{
    public static class Processes
    {
        public static readonly IEnumerable<IProcessInfo> ProcessInfos = new List<IProcessInfo>
        {
            //new Process(0,0, "Uknown Process", "Unknown Process", "Unknown"),
            new ProcessInfo(1, 0, "Starting System", "Prepare system for Intial Use", "Start"),
            new ProcessInfo<ISignInInfo>(2, 1, "User SignOn", "User Login", "User"),
            new ProcessInfo<ISignInInfo>(3, 2, "Load User Screen", "User Screen", "UserScreen")
        };



        public static List<IComplexEventAction> ProcessComplexEvents = new List<IComplexEventAction>
        {
            new ComplexEventAction(
                "101",
                1, new List<IProcessExpectedEvent>
                {
                    new ProcessExpectedEvent ("ProcessStarted", 1, typeof (ISystemProcessStarted), e => e != null, new ProcessStateInfo("Process Started","First Step"), new SourceType(typeof(IProcessService))),
                    new ProcessExpectedEvent ("ViewCreated", 1, typeof (IViewModelCreated<IScreenModel>), e => e != null, new ProcessStateInfo("ScreenView Created","This view contains all views"), new SourceType(typeof(IViewModelService) )),
                    new ProcessExpectedEvent ("ViewLoaded", 1, typeof (IViewModelLoaded<IMainWindowViewModel,IScreenModel>), e => e != null, new ProcessStateInfo("ScreenView Model loaded in MainWindowViewModel","Only ViewModel in Body"), new SourceType(typeof(IViewModelService) ))
                },
                typeof(ISystemProcessCompleted),
                processInfo:new ProcessStateInfo("Process Completed","Change Process State to Process Completed" ),
                action: ProcessActions.ProcessCompleted),


            new ComplexEventAction(
                key:"102",
                processId:1,
                events: new List<IProcessExpectedEvent>
                {
                    new ProcessExpectedEvent (key: "ProcessCompleted",processId: 1, eventType: typeof (ISystemProcessCompleted), eventPredicate: e => e != null, processInfo: new ProcessStateInfo("System Initalization complete","Should Close Process Actor"), )
                }, 
                expectedMessageType: typeof(ISystemProcessTerminated),
                action: ProcessActions.ShutActorDown), 
            new ComplexEventAction("201",
                2, new List<IProcessExpectedEvent>
                {
                    new ProcessExpectedEvent ("ProcessStarted", 2, e => e != null, ProcessExpectedEventInfos.ProcessStarted)
                    
                }, typeof(IProcessStateMessage<ISignInInfo>), ProcessActions.IntializeSigninProcessState, new ProcessStateInfo("Intialize Process Start for Signin process","")),
            new ComplexEventAction(
                "202", 2, new List<IProcessExpectedEvent>
                {
                    new ProcessExpectedEvent<IEntityViewWithChangesFound<ISignInInfo>> (
                        "UserNameFound", 2, e => e.Entity != null && e.Changes.Count == 1 && e.Changes.ContainsKey(nameof(ISignInInfo.Usersignin)), expectedSourceType: new SourceType(typeof(IEntityViewRepository)), processInfo: new ProcessStateInfo("User Name Found","Not Verified"))
                },
                typeof(IProcessStateMessage<ISignInInfo>),
                ProcessActions.UserNameFound,
                new ProcessStateInfo("Welcome User","")),
            new ComplexEventAction(
                "203", 2, new List<IProcessExpectedEvent>
                {
                    new ProcessExpectedEvent<IEntityViewWithChangesFound<ISignInInfo>> (processId: 2, eventPredicate: e => e.Entity != null && e.Changes.Count == 2 && e.Changes.ContainsKey(nameof(ISignInInfo.Password)), processInfo: new ProcessStateInfo("User Found with Both Username and Password",""), expectedSourceType: new SourceType(typeof(IEntityViewRepository)), key: "ValidatedUser")
                },
                typeof(IProcessStateMessage<ISignInInfo>),
                ProcessActions.SetProcessStatetoValidatedUser, new ProcessStateInfo("Set User Validated ProcessState","")),
            new ComplexEventAction(
                "204", 2, new List<IProcessExpectedEvent>(),
                typeof(IUserValidated),
                processInfo:new ProcessStateInfo("Inform Domain User Validated",""),
                action: ProcessActions.UserValidated)
            
           
        };

        
    }


}