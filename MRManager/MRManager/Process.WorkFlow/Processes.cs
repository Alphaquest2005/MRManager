using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SystemInterfaces;
using Actor.Interfaces;
using Domain.Interfaces;
using EventMessages.Commands;
using Interfaces;
using RevolutionData;
using RevolutionEntities.Process;
using Utilities;
using ViewModel.Interfaces;

namespace Process.WorkFlow
{
    

    public static class Processes
    {
        public static readonly List<IProcessInfo> ProcessInfos = new List<IProcessInfo>
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
                    new ProcessExpectedEvent ("ServiceManagerStarted", 1, typeof (IServiceStarted<IServiceManager>), e => e != null, new StateEventInfo(1, RevolutionData.Context.Actor.Events.ActorStarted), new SourceType(typeof(IServiceManager))),
                    
                },
                typeof(ISystemProcessStarted),
                processInfo:new StateCommandInfo(1,RevolutionData.Context.Process.Commands.StartProcess ),
                action: ProcessActions.ProcessStarted),

             

            new ComplexEventAction(
                "102",
                1, new List<IProcessExpectedEvent>
                {
                    new ProcessExpectedEvent ("ProcessStarted", 1, typeof (ISystemProcessStarted), e => e != null, new StateEventInfo(1, RevolutionData.Context.Process.Events.ProcessStarted), new SourceType(typeof(IProcessService))),
                    new ProcessExpectedEvent ("ViewCreated", 1, typeof (IViewModelCreated<IScreenModel>), e => e != null, new StateEventInfo(1,"ScreenViewCreated", "ScreenView Created","This view contains all views", RevolutionData.Context.ViewModel.Commands.CreateViewModel), new SourceType(typeof(IViewModelService) )),
                    new ProcessExpectedEvent ("ViewLoaded", 1, typeof (IViewModelLoaded<IMainWindowViewModel,IScreenModel>), e => e != null, new StateEventInfo(1,"ScreenViewLoaded","ScreenView Model loaded in MainWindowViewModel","Only ViewModel in Body", RevolutionData.Context.ViewModel.Commands.LoadViewModel), new SourceType(typeof(IViewModelService) ))
                },
                typeof(ISystemProcessCompleted),
                processInfo:new StateCommandInfo(1,RevolutionData.Context.Process.Commands.CompleteProcess ),
                action: ProcessActions.CompleteProcess),

           new ComplexEventAction(
                "103",
                1, new List<IProcessExpectedEvent>
                {
                    new ProcessExpectedEvent ("ProcessCompleted", 1, typeof (ISystemProcessCompleted), e => e != null, new StateEventInfo(1, RevolutionData.Context.Process.Events.ProcessCompleted), new SourceType(typeof(IComplexEventService))),

                },
                typeof(ISystemProcessStarted),
                processInfo:new StateCommandInfo(1,RevolutionData.Context.Process.Commands.StartProcess),
                action: ProcessActions.StartProcess),

            new ComplexEventAction(
                "104",
                1, new List<IProcessExpectedEvent>
                {
                    new ProcessExpectedEvent ("ProcessCompleted", 1, typeof (ISystemProcessCompleted), e => e != null, new StateEventInfo(1, RevolutionData.Context.Process.Events.ProcessCompleted), new SourceType(typeof(IComplexEventService))),

                },
                typeof(ISystemProcessCleanedUp),
                processInfo:new StateCommandInfo(1,RevolutionData.Context.Process.Commands.CleanUpProcess ),
                action: ProcessActions.CleanUpProcess),

            
            //new ComplexEventAction(
            //    "106",
            //    1, new List<IProcessExpectedEvent>
            //    {
            //        new ProcessExpectedEvent ("ProcessEventError", 1, typeof (IProcessEventFailure), e => e != null, new StateEventInfo(1, Context.Process.Events.Error), new SourceType(typeof(IComplexEventService))),

            //    },
            //    typeof(IProcessEventFailure),
            //    processInfo:new StateCommandInfo(1,Context.Process.Commands.Error ),
            //    action: ProcessActions.ShutDownApplication),

            new ComplexEventAction(
                "200",
                2, new List<IProcessExpectedEvent>
                {
                    new ProcessExpectedEvent ("ProcessServiceStarted", 2, typeof (IServiceStarted<IProcessService>), e => e != null, new StateEventInfo(2, RevolutionData.Context.Actor.Events.ActorStarted), new SourceType(typeof(IProcessService))),

                },
                typeof(ISystemProcessStarted),
                processInfo:new StateCommandInfo(2,RevolutionData.Context.Process.Commands.StartProcess ),
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
                                              processInfo: new StateEventInfo(2,RevolutionData.Context.Process.Events.ProcessStarted),
                                              expectedSourceType:new SourceType(typeof(IComplexEventService)))
                    
                },
                expectedMessageType:typeof(IProcessStateMessage<ISignInInfo>),
                action:ProcessActions.SignIn.IntializeSigninProcessState,
                processInfo:new StateCommandInfo(2, RevolutionData.Context.Process.Commands.CreateState)),
            new ComplexEventAction(
                key:"202",
                processId:2,
                events:new List<IProcessExpectedEvent>
                {
                    new ProcessExpectedEvent<IEntityViewWithChangesFound<ISignInInfo>> (
                        "UserNameFound", 2, e => e.Entity != null && e.Changes.Count == 1 && e.Changes.ContainsKey(nameof(ISignInInfo.Usersignin)), expectedSourceType: new SourceType(typeof(IEntityViewRepository)), processInfo: new StateEventInfo(2, RevolutionData.Context.User.Events.UserNameFound))
                },
                expectedMessageType:typeof(IProcessStateMessage<ISignInInfo>),
                action: ProcessActions.SignIn.UserNameFound,
                processInfo: new StateCommandInfo(2, RevolutionData.Context.Process.Commands.UpdateState)),
            new ComplexEventAction(
                key:"203",
                processId: 2,
                events: new List<IProcessExpectedEvent>
                {
                    new ProcessExpectedEvent<IEntityViewWithChangesFound<ISignInInfo>> (processId: 2,
                                                        eventPredicate: e => e.Entity != null && e.Changes.Count == 2 && e.Changes.ContainsKey(nameof(ISignInInfo.Password)),
                                                        processInfo: new StateEventInfo(2, RevolutionData.Context.User.Events.UserFound),
                                                        expectedSourceType: new SourceType(typeof(IEntityViewRepository)),
                                                        key: "ValidatedUser")
                },
                expectedMessageType: typeof(IProcessStateMessage<ISignInInfo>),
                action: ProcessActions.SignIn.SetProcessStatetoValidatedUser,
                processInfo: new StateCommandInfo(2, RevolutionData.Context.Process.Commands.UpdateState)),
            new ComplexEventAction(
                key:"204",
                processId: 2,
                events: new List<IProcessExpectedEvent>
                {
                    new ProcessExpectedEvent<IEntityViewWithChangesFound<ISignInInfo>> (processId: 2,
                                                        eventPredicate: e => e.Entity != null && e.Changes.Count == 2 && e.Changes.ContainsKey(nameof(ISignInInfo.Password)),
                                                        processInfo: new StateEventInfo(2, RevolutionData.Context.User.Events.UserFound),
                                                        expectedSourceType: new SourceType(typeof(IEntityViewRepository)),
                                                        key: "ValidatedUser")
                },
                expectedMessageType:typeof(IUserValidated),
                processInfo:new StateCommandInfo(2, RevolutionData.Context.Domain.Commands.PublishDomainEvent),
                action: ProcessActions.SignIn.UserValidated),

            new ComplexEventAction(
                "205",
                2, new List<IProcessExpectedEvent>
                {
                    new ProcessExpectedEvent ("ValidatedUser", 2, typeof (IUserValidated), e => e != null, new StateEventInfo(2, RevolutionData.Context.User.Events.UserFound), new SourceType(typeof(IComplexEventService))),
                    
                },
                typeof(ISystemProcessCompleted),
                processInfo:new StateCommandInfo(2,RevolutionData.Context.Process.Commands.CompleteProcess ),
                action: ProcessActions.CompleteProcess),

             new ComplexEventAction(
                "206",
                2, new List<IProcessExpectedEvent>
                {
                    new ProcessExpectedEvent ("ValidatedUser", 2, typeof (IUserValidated), e => e != null, new StateEventInfo(2, RevolutionData.Context.User.Events.UserFound), new SourceType(typeof(IComplexEventService))),

                },
                typeof(ISystemProcessStarted),
                processInfo:new StateCommandInfo(2,RevolutionData.Context.Process.Commands.StartProcess ),
                action: ProcessActions.StartProcessWithValidatedUser),

            new ComplexEventAction(
                "207",
                2, new List<IProcessExpectedEvent>
                {
                    new ProcessExpectedEvent ("ProcessCompleted", 2, typeof (ISystemProcessCompleted), e => e != null, new StateEventInfo(2, RevolutionData.Context.Process.Events.ProcessCompleted), new SourceType(typeof(IComplexEventService))),

                },
                typeof(ISystemProcessCleanedUp),
                processInfo:new StateCommandInfo(2,RevolutionData.Context.Process.Commands.CleanUpProcess ),
                action: ProcessActions.CleanUpProcess),

             new ComplexEventAction(
                "300",
                3, new List<IProcessExpectedEvent>
                {
                    new ProcessExpectedEvent ("ProcessServiceStarted", 3, typeof (IServiceStarted<IProcessService>), e => e != null, new StateEventInfo(3, RevolutionData.Context.Actor.Events.ActorStarted), new SourceType(typeof(IProcessService))),

                },
                typeof(ISystemProcessStarted),
                processInfo:new StateCommandInfo(3,RevolutionData.Context.Process.Commands.StartProcess ),
                action: ProcessActions.ProcessStarted),



            ComplexActions.IntializePulledProcessState<IPatientInfo>(3, "Patient"),
            ComplexActions.UpdateStateList<IPatientInfo>(3),
            


            ComplexActions.RequestPulledState<IPatientInfo, IPatientDetailsInfo>(3,  "Patient"),
            ComplexActions.UpdateState<IPatientDetailsInfo>(3),
            ComplexActions.UpdateStateWhenDataChanges<IPatientInfo,IPatientDetailsInfo>(3, c => c.Id, v => v.Id),
            ComplexActions.RequestState<IPatientInfo, IPatientAddressesInfo>(3, x => x.Id),
            ComplexActions.UpdateState<IPatientAddressesInfo>(3),
            ComplexActions.RequestState<IPatientInfo, IPatientPhoneNumbersInfo>(3, x => x.Id),
            ComplexActions.UpdateState<IPatientPhoneNumbersInfo>(3),
            ComplexActions.RequestState<IPatientInfo, IPatientNextOfKinsInfo>(3, x => x.Id),
            ComplexActions.UpdateState<IPatientNextOfKinsInfo>(3),
            ComplexActions.RequestPulledState<IPatientInfo, INonResidentInfo>(3, "NonResident"),
            ComplexActions.UpdateState<INonResidentInfo>(3),





            ComplexActions.RequestStateList<IPatientInfo, IPatientVisitInfo>(3, c => c.Id,x => x.PatientId),
            ComplexActions.UpdateStateList<IPatientVisitInfo>(3),
            ComplexActions.UpdateStateWhenDataChanges<IPatientInfo,IPatientVisitInfo>(3, c => c.Id, v => v.PatientId),

            ComplexActions.RequestStateList<IPatientVisitInfo, IPatientSyntomInfo>(3,c => c.Id, x => x.PatientVisitId),
            ComplexActions.RequestStateList<ISyntoms, IPatientSyntomInfo>(3,c => c.Id, x => x.SyntomId),
            ComplexActions.UpdateStateList<IPatientSyntomInfo>(3),
            ComplexActions.UpdateStateWhenDataChanges<IPatientSyntoms,IPatientSyntomInfo>(3, c => c.Id, v => v.Id),

            ComplexActions.RequestStateList<IPatientSyntomInfo, ISyntomMedicalSystemInfo>(3,c => c.SyntomId, x => x.SyntomId),
            ComplexActions.UpdateStateList<ISyntomMedicalSystemInfo>(3),
            ComplexActions.UpdateStateWhenDataChanges<ISyntomMedicalSystems,ISyntomMedicalSystemInfo>(3, c => c.Id, v => v.Id),
            ComplexActions.UpdateStateWhenDataChanges<IInterviews,IInterviewInfo>(3, c => c.Id, v => v.Id),


            ComplexActions.RequestStateList<IInterviewInfo, IQuestionResponseOptionInfo>(3,c => c.Id, x => x.InterviewId),
            ComplexActions.UpdateStateList<IQuestionResponseOptionInfo>(3),
            ComplexActions.UpdateStateWhenDataChanges<IQuestionInfo,IQuestionResponseOptionInfo>(3, c => c.Id, v => v.Id),
            ComplexActions.UpdateStateWhenDataChanges<IResponseInfo,IQuestionResponseOptionInfo>(3, c => c.QuestionId, v => v.Id),
            ComplexActions.UpdateStateWhenDataChanges<IResponseOptions,IQuestionResponseOptionInfo>(3, c => c.QuestionId, v => v.Id),
            

            ComplexActions.RequestStateList<IInterviewInfo, IQuestionInfo>(3,c => c.Id, x => x.InterviewId),
            ComplexActions.UpdateStateList<IQuestionInfo>(3),
            ComplexActions.UpdateStateWhenDataChanges<IQuestions,IQuestionInfo>(3, c => c.Id, v => v.Id),


            EntityComplexActions<ISyntomPriority>.IntializeCache(3),
            EntityComplexActions<ISyntoms>.IntializeCache(3),
            EntityComplexActions<ISyntomStatus>.IntializeCache(3),
            EntityComplexActions<IVisitType>.IntializeCache(3),
            EntityComplexActions<IPhase>.IntializeCache(3),
            EntityComplexActions<IMedicalCategory>.IntializeCache(3),
            EntityComplexActions<IMedicalSystems>.IntializeCache(3),
            EntityComplexActions<IQuestionResponseTypes>.IntializeCache(3),
            EntityComplexActions<ISex>.IntializeCache(3),

            EntityViewComplexActions<IDoctorInfo>.IntializeCache(3)
        };

        public static class ComplexActions
        {
            public static ComplexEventAction IntializePulledProcessState<TEntityView>(int processId, string entityName) where TEntityView : IEntityView
            {
                return new ComplexEventAction(

                    key: $"InitalizeProcessState-{typeof(TEntityView).GetFriendlyName()}",
                    processId: processId,
                    events: new List<IProcessExpectedEvent>
                    {
                        new ProcessExpectedEvent(key: "ProcessStarted",
                            processId: processId,
                            eventPredicate: e => e != null,
                            eventType: typeof (ISystemProcessStarted),
                            processInfo: new StateEventInfo(processId, RevolutionData.Context.Process.Events.ProcessStarted),
                            expectedSourceType: new SourceType(typeof (IComplexEventService))),
                        
                    },
                    expectedMessageType: typeof (IProcessStateMessage<TEntityView>),
                    action: ProcessActions.IntializePulledProcessState<TEntityView>(entityName),
                    processInfo: new StateCommandInfo(processId, RevolutionData.Context.Process.Commands.CreateState));
            }


            public static ComplexEventAction UpdateState<TEntityView>(int processId) where TEntityView : IEntityView
            {
                return new ComplexEventAction(
                    key: $"UpdateState-{typeof(TEntityView).GetFriendlyName()}",
                    processId: processId,
                    actionTrigger: ActionTrigger.Any, 
                    events: new List<IProcessExpectedEvent>
                    {
                                new ProcessExpectedEvent<IEntityViewWithChangesUpdated<TEntityView>> (processId: processId,
                            eventPredicate: e => e.Entity != null,
                            processInfo: new StateEventInfo(processId, RevolutionData.Context.Entity.Events.EntityUpdated),
                            expectedSourceType: new SourceType(typeof(IEntityRepository)),
                            key: "EntityView"),
                                   new ProcessExpectedEvent<IEntityViewWithChangesFound<TEntityView>> (processId: processId,
                            eventPredicate: e => e.Entity != null,
                            processInfo: new StateEventInfo(processId, RevolutionData.Context.Entity.Events.EntityFound),
                            expectedSourceType: new SourceType(typeof(IEntityRepository)),
                            key: "EntityView"),
                                   new ProcessExpectedEvent<IEntityFound<TEntityView>> (processId: processId,
                            eventPredicate: e => e.Entity != null,
                            processInfo: new StateEventInfo(processId, RevolutionData.Context.Entity.Events.EntityFound),
                            expectedSourceType: new SourceType(typeof(IEntityRepository)),
                            key: "EntityView")
                    },
                    expectedMessageType: typeof(IProcessStateMessage<TEntityView>),
                    action: ProcessActions.UpdateEntityViewState<TEntityView>(),
                    processInfo: new StateCommandInfo(processId, RevolutionData.Context.Process.Commands.UpdateState));
            }

            
            public static ComplexEventAction RequestState<TCurrentEntity, TEntityView>(int processId, Expression<Func<TEntityView, dynamic>> property) where TEntityView : IEntityView where TCurrentEntity : IEntityId
            {
                return new ComplexEventAction(
                    key: $"RequestState-{typeof(TEntityView).GetFriendlyName()}",
                    processId: processId,
                    actionTrigger: ActionTrigger.Any, 
                    events: new List<IProcessExpectedEvent>
                    {
                        new ProcessExpectedEvent<ICurrentEntityChanged<TCurrentEntity>>(
                            "CurrentEntity", processId, e => e.Entity != null,
                            expectedSourceType: new SourceType(typeof (IViewModel)),
                            //todo: check this cuz it comes from viewmodel
                            processInfo: new StateEventInfo(processId, RevolutionData.Context.Process.Events.CurrentEntityChanged)),

                        new ProcessExpectedEvent<IEntityFound<TCurrentEntity>>(
                            "CurrentEntity", processId, e => e.Entity != null,
                            expectedSourceType: new SourceType(typeof (IViewModel)),
                            //todo: check this cuz it comes from viewmodel
                            processInfo: new StateEventInfo(processId, RevolutionData.Context.Entity.Events.EntityFound)),
                        new ProcessExpectedEvent<IEntityUpdated<TCurrentEntity>>(
                            "CurrentEntity", processId, e => e.Entity != null,
                            expectedSourceType: new SourceType(typeof (IViewModel)),
                            //todo: check this cuz it comes from viewmodel
                            processInfo: new StateEventInfo(processId, RevolutionData.Context.Entity.Events.EntityUpdated)),
                        new ProcessExpectedEvent<IEntityViewWithChangesFound<TCurrentEntity>>(
                            "CurrentEntity", processId, e => e.Entity != null,
                            expectedSourceType: new SourceType(typeof (IViewModel)),
                            //todo: check this cuz it comes from viewmodel
                            processInfo: new StateEventInfo(processId, RevolutionData.Context.EntityView.Events.EntityViewFound))
                    },
                    expectedMessageType: typeof(IProcessStateMessage<TEntityView>),
                    action: ProcessActions.RequestState(property),
                    processInfo: new StateCommandInfo(processId, RevolutionData.Context.Process.Commands.UpdateState));
            }

            public static ComplexEventAction RequestPulledState<TCurrentEntity, TEntityView>(int processId, string entityName) where TEntityView : IEntityView where TCurrentEntity : IEntityId
            {
                return new ComplexEventAction(
                    key: $"RequestState-{typeof(TEntityView).GetFriendlyName()}",
                    processId: processId,
                    actionTrigger: ActionTrigger.Any,
                    events: new List<IProcessExpectedEvent>
                    {
                        new ProcessExpectedEvent<ICurrentEntityChanged<TCurrentEntity>>(
                            "CurrentEntity", processId, e => e.Entity != null,
                            expectedSourceType: new SourceType(typeof (IViewModel)),
                            //todo: check this cuz it comes from viewmodel
                            processInfo: new StateEventInfo(processId, RevolutionData.Context.Process.Events.CurrentEntityChanged)),

                        new ProcessExpectedEvent<IEntityFound<TCurrentEntity>>(
                            "CurrentEntity", processId, e => e.Entity != null,
                            expectedSourceType: new SourceType(typeof (IViewModel)),
                            //todo: check this cuz it comes from viewmodel
                            processInfo: new StateEventInfo(processId, RevolutionData.Context.Entity.Events.EntityFound)),
                        new ProcessExpectedEvent<IEntityUpdated<TCurrentEntity>>(
                            "CurrentEntity", processId, e => e.Entity != null,
                            expectedSourceType: new SourceType(typeof (IViewModel)),
                            //todo: check this cuz it comes from viewmodel
                            processInfo: new StateEventInfo(processId, RevolutionData.Context.Entity.Events.EntityUpdated)),
                        new ProcessExpectedEvent<IEntityViewWithChangesFound<TCurrentEntity>>(
                            "CurrentEntity", processId, e => e.Entity != null,
                            expectedSourceType: new SourceType(typeof (IViewModel)),
                            //todo: check this cuz it comes from viewmodel
                            processInfo: new StateEventInfo(processId, RevolutionData.Context.EntityView.Events.EntityViewFound))
                    },
                    expectedMessageType: typeof(IProcessStateMessage<TEntityView>),
                    action: ProcessActions.RequestPulledState<TEntityView>(entityName),
                    processInfo: new StateCommandInfo(processId, RevolutionData.Context.Process.Commands.UpdateState));
            }



            public static ComplexEventAction UpdateStateList<TEntityView>(int processId) where TEntityView : IEntityView
            {
                return new ComplexEventAction(
                    key: $"UpdateStateList-{typeof(TEntityView).GetFriendlyName()}",
                    processId: processId,
                    events: new List<IProcessExpectedEvent>
                    {
                            new ProcessExpectedEvent<IEntityViewSetWithChangesLoaded<TEntityView>> (
                        "EntityViewSet",processId, e => e.EntitySet != null, expectedSourceType: new SourceType(typeof(IEntityViewRepository)),
                        processInfo: new StateEventInfo(processId, RevolutionData.Context.EntityView.Events.EntityViewSetLoaded))
                    },
                    expectedMessageType: typeof(IProcessStateList<TEntityView>),
                    action: ProcessActions.UpdateEntityViewStateList<TEntityView>(),
                    processInfo: new StateCommandInfo(processId, RevolutionData.Context.Process.Commands.UpdateState));
            }
            public static ComplexEventAction RequestStateList<TCurrentEntity,TEntityView>(int processId, Expression<Func<TCurrentEntity, object>> currentProperty, Expression<Func<TEntityView, object>> viewProperty) where TEntityView : IEntityView where TCurrentEntity:IEntityId
            {
                return new ComplexEventAction(
                    key: $"RequestStateList-{typeof(TCurrentEntity).GetFriendlyName()}-{typeof(TEntityView).GetFriendlyName()}",
                    processId: processId,
                    actionTrigger: ActionTrigger.Any, 
                    events: new List<IProcessExpectedEvent>
                    {
                        new ProcessExpectedEvent<ICurrentEntityChanged<TCurrentEntity>>(
                            "CurrentEntity", processId, e => e.Entity != null,
                            expectedSourceType: new SourceType(typeof (IViewModel)),
                            //todo: check this cuz it comes from viewmodel
                            processInfo: new StateEventInfo(processId, RevolutionData.Context.Process.Events.CurrentEntityChanged)),
                        
                    },
                    expectedMessageType: typeof(IProcessStateMessage<TEntityView>),
                    action: ProcessActions.RequestStateList(currentProperty,viewProperty),
                    processInfo: new StateCommandInfo(processId, RevolutionData.Context.Process.Commands.UpdateState));
            }

            public static IComplexEventAction UpdateStateWhenDataChanges<TEntity, TView>(int processId, Expression<Func<TEntity, object>> currentProperty, Expression<Func<TView, object>> viewProperty) where TEntity : IEntityId where TView : IEntityView
            {
                return new ComplexEventAction(
                    key: $"Update{typeof(TEntity).Name}-{typeof(TView).Name}",
                    processId: 3,
                    actionTrigger:ActionTrigger.Any, 
                    events: new List<IProcessExpectedEvent>
                    {
                        new ProcessExpectedEvent<IEntityUpdated<TEntity>>(processId: processId,
                            eventPredicate: e => e.Entity != null,
                            processInfo: new StateEventInfo(processId, RevolutionData.Context.Entity.Events.EntityUpdated),
                            expectedSourceType: new SourceType(typeof (IEntityRepository)),
                            key: "UpdatedEntity"),
                        new ProcessExpectedEvent<IEntityViewWithChangesUpdated<TEntity>>(processId: processId,
                            eventPredicate: e => e.Entity != null,
                            processInfo: new StateEventInfo(processId, RevolutionData.Context.EntityView.Events.EntityViewUpdated),
                            expectedSourceType: new SourceType(typeof (IEntityRepository)),
                            key: "UpdatedEntity"),


                    },
                    expectedMessageType: typeof(IProcessStateMessage<TView>),
                    action: GetView(currentProperty, viewProperty),
                    processInfo: new StateCommandInfo(processId, RevolutionData.Context.Process.Commands.UpdateState));
            }

            public static IProcessAction GetView<TEntity,TView>(Expression<Func<TEntity, object>> currentProperty, Expression<Func<TView, object>> viewProperty) where TView : IEntityView
            {
                return new ProcessAction(
                    action:
                        async cp =>
                        {
                            var key = default(TView).GetMemberName(viewProperty);
                            var value = currentProperty.Compile().Invoke(cp.Messages["UpdatedEntity"].Entity);
                            var changes = new Dictionary<string, dynamic>() { { key, value } };

                            return await Task.Run(() => new GetEntityViewWithChanges<TView>(changes,
                                new StateCommandInfo(cp.Actor.Process.Id, RevolutionData.Context.EntityView.Commands.GetEntityView),
                                cp.Actor.Process, cp.Actor.Source));
                        },
                    processInfo: cp =>
                        new StateCommandInfo(cp.Actor.Process.Id,
                            RevolutionData.Context.EntityView.Commands.GetEntityView),
                    // take shortcut cud be IntialState
                    expectedSourceType: new SourceType(typeof(IComplexEventService))

                    );
            }


        }
    }


}

