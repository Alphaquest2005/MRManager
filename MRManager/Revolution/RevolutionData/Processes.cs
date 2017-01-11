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
                    new ProcessExpectedEvent ("ProcessStarted", 1, typeof (ISystemProcessStarted), e => e != null, new StateEventInfo(1, Context.Process.Events.ProcessStarted), new SourceType(typeof(IProcessService))),
                    new ProcessExpectedEvent ("ViewCreated", 1, typeof (IViewModelCreated<IScreenModel>), e => e != null, new StateEventInfo(1,"ScreenViewCreated", "ScreenView Created","This view contains all views", Context.ViewModel.Commands.CreateViewModel), new SourceType(typeof(IViewModelService) )),
                    new ProcessExpectedEvent ("ViewLoaded", 1, typeof (IViewModelLoaded<IMainWindowViewModel,IScreenModel>), e => e != null, new StateEventInfo(1,"ScreenViewLoaded","ScreenView Model loaded in MainWindowViewModel","Only ViewModel in Body", Context.ViewModel.Commands.LoadViewModel), new SourceType(typeof(IViewModelService) ))
                },
                typeof(ISystemProcessCompleted),
                processInfo:new StateCommandInfo(1,Context.Process.Commands.CompleteProcess ),
                action: ProcessActions.ProcessCompleted),


            new ComplexEventAction(
                key:"102",
                processId:1,
                events: new List<IProcessExpectedEvent>
                {
                    new ProcessExpectedEvent (key: "ProcessCompleted",
                                              processId: 1,
                                              eventPredicate: e => e != null,
                                              eventType: typeof (ISystemProcessCompleted),
                                              processInfo: new StateEventInfo(1,Context.Process.Events.ProcessCompleted),
                                              expectedSourceType:new SourceType(typeof(IComplexEventService)))
                }, 
                expectedMessageType: typeof(ISystemProcessTerminated),
                action: ProcessActions.ShutActorDown,
                processInfo:new StateCommandInfo(1,Context.Actor.Commands.TerminateActor )), 
            new ComplexEventAction(
                key:"201",
                processId:2,
                events:new List<IProcessExpectedEvent>
                {
                    new ProcessExpectedEvent (key: "ProcessStarted",
                                              processId: 2,
                                              eventPredicate: e => e != null,
                                              eventType: typeof (ISystemProcessCompleted),
                                              processInfo: new StateEventInfo(1,Context.Process.Events.ProcessCompleted),
                                              expectedSourceType:new SourceType(typeof(IComplexEventService)))
                    
                },
                expectedMessageType:typeof(IProcessStateMessage<ISignInInfo>),
                action:ProcessActions.IntializeSigninProcessState,
                processInfo:new StateCommandInfo(2, Context.Process.Commands.CreateState)),
            new ComplexEventAction(
                key:"202",
                processId:2,
                events:new List<IProcessExpectedEvent>
                {
                    new ProcessExpectedEvent<IEntityViewWithChangesFound<ISignInInfo>> (
                        "UserNameFound", 2, e => e.Entity != null && e.Changes.Count == 1 && e.Changes.ContainsKey(nameof(ISignInInfo.Usersignin)), expectedSourceType: new SourceType(typeof(IEntityViewRepository)), processInfo: new StateEventInfo(2, Context.User.Events.UserNameFound))
                },
                expectedMessageType:typeof(IProcessStateMessage<ISignInInfo>),
                action: ProcessActions.UserNameFound,
                processInfo: new StateCommandInfo(2, Context.Process.Commands.UpdateState)),
            new ComplexEventAction(
                key:"203",
                processId: 2,
                events: new List<IProcessExpectedEvent>
                {
                    new ProcessExpectedEvent<IEntityViewWithChangesFound<ISignInInfo>> (processId: 2,
                                                        eventPredicate: e => e.Entity != null && e.Changes.Count == 2 && e.Changes.ContainsKey(nameof(ISignInInfo.Password)),
                                                        processInfo: new StateEventInfo(2, Context.User.Events.UserFound),
                                                        expectedSourceType: new SourceType(typeof(IEntityViewRepository)),
                                                        key: "ValidatedUser")
                },
                expectedMessageType: typeof(IProcessStateMessage<ISignInInfo>),
                action: ProcessActions.SetProcessStatetoValidatedUser,
                processInfo: new StateCommandInfo(2, Context.Process.Commands.UpdateState)),
            new ComplexEventAction(
                key:"204",
                processId: 2,
                events: new List<IProcessExpectedEvent>(),
                expectedMessageType:typeof(IUserValidated),
                processInfo:new StateCommandInfo(2, Context.Domain.Commands.PublishDomainEvent),
                action: ProcessActions.UserValidated)
            
           
        };

        
    }


}

