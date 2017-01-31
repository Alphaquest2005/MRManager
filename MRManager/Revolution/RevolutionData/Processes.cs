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
        public const int NullProcess = -1;

        public static readonly IEnumerable<IProcessInfo> ProcessInfos = new List<IProcessInfo>
        {
            //new Process(0,0, "Uknown Process", "Unknown Process", "Unknown"),
            new ProcessInfo(1, 0, "Starting System", "Prepare system for Intial Use", "Start","System"),
            new ProcessInfo<ISignInInfo>(2, 1, "User SignOn", "User Login", "User","System"),
            new ProcessInfo<ISignInInfo>(3, 2, "Load User Screen", "User Screen", "UserScreen", "joe")
        };



        public static List<IComplexEventAction> ProcessComplexEvents = new List<IComplexEventAction>
        {
             new ComplexEventAction(
                "100",
                1, new List<IProcessExpectedEvent>
                {
                    new ProcessExpectedEvent ("ProcessServiceStarted", 1, typeof (IServiceStarted<IProcessService>), e => e != null, new StateEventInfo(1, Context.Actor.Events.ActorStarted), new SourceType(typeof(IProcessService))),
                    
                },
                typeof(ISystemProcessStarted),
                processInfo:new StateCommandInfo(1,Context.Process.Commands.StartProcess ),
                action: ProcessActions.ProcessStarted),

             

            new ComplexEventAction(
                "102",
                1, new List<IProcessExpectedEvent>
                {
                    new ProcessExpectedEvent ("ProcessStarted", 1, typeof (ISystemProcessStarted), e => e != null, new StateEventInfo(1, Context.Process.Events.ProcessStarted), new SourceType(typeof(IProcessService))),
                    new ProcessExpectedEvent ("ViewCreated", 1, typeof (IViewModelCreated<IScreenModel>), e => e != null, new StateEventInfo(1,"ScreenViewCreated", "ScreenView Created","This view contains all views", Context.ViewModel.Commands.CreateViewModel), new SourceType(typeof(IViewModelService) )),
                    new ProcessExpectedEvent ("ViewLoaded", 1, typeof (IViewModelLoaded<IMainWindowViewModel,IScreenModel>), e => e != null, new StateEventInfo(1,"ScreenViewLoaded","ScreenView Model loaded in MainWindowViewModel","Only ViewModel in Body", Context.ViewModel.Commands.LoadViewModel), new SourceType(typeof(IViewModelService) ))
                },
                typeof(ISystemProcessCompleted),
                processInfo:new StateCommandInfo(1,Context.Process.Commands.CompleteProcess ),
                action: ProcessActions.CompleteProcess),

           new ComplexEventAction(
                "103",
                1, new List<IProcessExpectedEvent>
                {
                    new ProcessExpectedEvent ("ProcessCompleted", 1, typeof (ISystemProcessCompleted), e => e != null, new StateEventInfo(1, Context.Process.Events.ProcessCompleted), new SourceType(typeof(IComplexEventService))),

                },
                typeof(ISystemProcessStarted),
                processInfo:new StateCommandInfo(1,Context.Process.Commands.StartProcess),
                action: ProcessActions.StartProcess),

            new ComplexEventAction(
                "104",
                1, new List<IProcessExpectedEvent>
                {
                    new ProcessExpectedEvent ("ProcessCompleted", 1, typeof (ISystemProcessCompleted), e => e != null, new StateEventInfo(1, Context.Process.Events.ProcessCompleted), new SourceType(typeof(IComplexEventService))),

                },
                typeof(ISystemProcessCleanedUp),
                processInfo:new StateCommandInfo(1,Context.Process.Commands.CleanUpProcess ),
                action: ProcessActions.CleanUpProcess),

            new ComplexEventAction(
                "200",
                2, new List<IProcessExpectedEvent>
                {
                    new ProcessExpectedEvent ("ProcessServiceStarted", 2, typeof (IServiceStarted<IProcessService>), e => e != null, new StateEventInfo(2, Context.Actor.Events.ActorStarted), new SourceType(typeof(IProcessService))),

                },
                typeof(ISystemProcessStarted),
                processInfo:new StateCommandInfo(2,Context.Process.Commands.StartProcess ),
                action: ProcessActions.ProcessStarted),
            new ComplexEventAction(
                
                key:"201",
                processId:2,
                events:new List<IProcessExpectedEvent>
                {
                    new ProcessExpectedEvent (key: "ProcessStarted",
                                              processId: 2,
                                              eventPredicate: e => e != null,
                                              eventType: typeof (ISystemProcessStarted),
                                              processInfo: new StateEventInfo(2,Context.Process.Events.ProcessStarted),
                                              expectedSourceType:new SourceType(typeof(IComplexEventService)))
                    
                },
                expectedMessageType:typeof(IProcessStateMessage<ISignInInfo>),
                action:ProcessActions.SignIn.IntializeSigninProcessState,
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
                action: ProcessActions.SignIn.UserNameFound,
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
                action: ProcessActions.SignIn.SetProcessStatetoValidatedUser,
                processInfo: new StateCommandInfo(2, Context.Process.Commands.UpdateState)),
            new ComplexEventAction(
                key:"204",
                processId: 2,
                events: new List<IProcessExpectedEvent>
                {
                    new ProcessExpectedEvent<IEntityViewWithChangesFound<ISignInInfo>> (processId: 2,
                                                        eventPredicate: e => e.Entity != null && e.Changes.Count == 2 && e.Changes.ContainsKey(nameof(ISignInInfo.Password)),
                                                        processInfo: new StateEventInfo(2, Context.User.Events.UserFound),
                                                        expectedSourceType: new SourceType(typeof(IEntityViewRepository)),
                                                        key: "ValidatedUser")
                },
                expectedMessageType:typeof(IUserValidated),
                processInfo:new StateCommandInfo(2, Context.Domain.Commands.PublishDomainEvent),
                action: ProcessActions.SignIn.UserValidated),

            new ComplexEventAction(
                "205",
                2, new List<IProcessExpectedEvent>
                {
                    new ProcessExpectedEvent ("ValidatedUser", 2, typeof (IUserValidated), e => e != null, new StateEventInfo(2, Context.User.Events.UserFound), new SourceType(typeof(IComplexEventService))),
                    
                },
                typeof(ISystemProcessCompleted),
                processInfo:new StateCommandInfo(2,Context.Process.Commands.CompleteProcess ),
                action: ProcessActions.CompleteProcess),

             new ComplexEventAction(
                "206",
                2, new List<IProcessExpectedEvent>
                {
                    new ProcessExpectedEvent ("ValidatedUser", 2, typeof (IUserValidated), e => e != null, new StateEventInfo(2, Context.User.Events.UserFound), new SourceType(typeof(IComplexEventService))),

                },
                typeof(ISystemProcessStarted),
                processInfo:new StateCommandInfo(2,Context.Process.Commands.StartProcess ),
                action: ProcessActions.StartProcessWithValidatedUser),

            new ComplexEventAction(
                "207",
                2, new List<IProcessExpectedEvent>
                {
                    new ProcessExpectedEvent ("ProcessCompleted", 2, typeof (ISystemProcessCompleted), e => e != null, new StateEventInfo(2, Context.Process.Events.ProcessCompleted), new SourceType(typeof(IComplexEventService))),

                },
                typeof(ISystemProcessCleanedUp),
                processInfo:new StateCommandInfo(2,Context.Process.Commands.CleanUpProcess ),
                action: ProcessActions.CleanUpProcess),

             new ComplexEventAction(
                "300",
                3, new List<IProcessExpectedEvent>
                {
                    new ProcessExpectedEvent ("ProcessServiceStarted", 3, typeof (IServiceStarted<IProcessService>), e => e != null, new StateEventInfo(3, Context.Actor.Events.ActorStarted), new SourceType(typeof(IProcessService))),

                },
                typeof(ISystemProcessStarted),
                processInfo:new StateCommandInfo(3,Context.Process.Commands.StartProcess ),
                action: ProcessActions.ProcessStarted),

            PatientSummaryListViewModelInfo.ComplexActions.IntializePatientInfoSummaryProcessState,
            PatientSummaryListViewModelInfo.ComplexActions.UpdatePatientInfoState,


            PatientDetailsViewModelInfo.ComplexActions.RequestPatientDetails,
            PatientDetailsViewModelInfo.ComplexActions.UpdatePatientDetailsState,


            
            InterviewListViewModelInfo.ComplexActions.IntializeInterviewInfoProcessState,
            InterviewListViewModelInfo.ComplexActions.UpdateInterviewInfoState,

             

            QuestionaireViewModelInfo.ComplexActions.RequestPatientResponses,
            QuestionaireViewModelInfo.ComplexActions.UpdatePatientResponseState,

          
            QuestionListViewModelInfo.ComplexActions.UpdateQuestionListState,
            QuestionListViewModelInfo.ComplexActions.RequestQuestionList,
        };
    }


}

