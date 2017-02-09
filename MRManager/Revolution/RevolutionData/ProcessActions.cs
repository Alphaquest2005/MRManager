using System;
using System.Collections.Generic;
using SystemInterfaces;
using Actor.Interfaces;
using Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Linq;
using System.Windows;
using DomainMessages;
using EventMessages;
using EventMessages.Commands;
using EventMessages.Events;
using Interfaces;
using RevolutionEntities;
using RevolutionEntities.Process;
using Utilities;
using ViewModel.Interfaces;

namespace RevolutionData
{
    public static class ProcessActions
    {


        



        public static IProcessAction ProcessStarted => new ProcessAction(
                        action: cp => new SystemProcessStarted(
                            new StateEventInfo(cp.Actor.Process.Id, Context.Process.Events.ProcessStarted),
                            cp.Actor.Process, cp.Actor.Source),
                        processInfo: cp => new StateCommandInfo(cp.Actor.Process.Id, Context.Process.Commands.StartProcess),
                        expectedSourceType: new SourceType(typeof(IComplexEventService)));

        public static IProcessAction StartProcess => new ProcessAction(
                        action: cp => new StartSystemProcess(Processes.NullProcess,//HACK: to keep this generic, the process that was used to create action will be used
                            new StateCommandInfo(cp.Actor.Process.Id, Context.Process.Commands.StartProcess),
                            cp.Actor.Process, cp.Actor.Source), 
                        processInfo: cp => new StateCommandInfo(cp.Actor.Process.Id, Context.Process.Commands.StartProcess),
                        expectedSourceType: new SourceType(typeof(IComplexEventService)));

        public static IProcessAction StartProcessWithValidatedUser => new ProcessAction(
                       action: cp => new StartSystemProcess(Processes.NullProcess,//HACK: to keep this generic, the process that was used to create action will be used
                           new StateCommandInfo(cp.Actor.Process.Id, Context.Process.Commands.StartProcess),
                           new SystemProcess(Processes.ProcessInfos.FirstOrDefault(x => x.Id == cp.Actor.Process.Id), cp.Messages["ValidatedUser"].UserSignIn, cp.Actor.Process.MachineInfo), cp.Actor.Source),
                       processInfo: cp => new StateCommandInfo(cp.Actor.Process.Id, Context.Process.Commands.StartProcess),
                       expectedSourceType: new SourceType(typeof(IComplexEventService)));

        public static IProcessAction CompleteProcess => new ProcessAction(
                        action: cp => new SystemProcessCompleted(new StateEventInfo(cp.Actor.Process.Id, Context.Process.Events.ProcessCompleted),cp.Actor.Process, cp.Actor.Source),
                        processInfo: cp => new StateCommandInfo(cp.Actor.Process.Id, Context.Process.Commands.CompleteProcess),
                        expectedSourceType: new SourceType(typeof(IComplexEventService)));



        public static IProcessAction CleanUpProcess => new ProcessAction(
                        action: cp => new CleanUpSystemProcess(cp.Actor.Process.Id,new StateCommandInfo(cp.Actor.Process.Id, Context.Process.Commands.CleanUpProcess), cp.Actor.Process, cp.Actor.Source), 
                        processInfo: cp => new StateCommandInfo(cp.Actor.Process.Id, Context.Process.Commands.CleanUpProcess),
                        expectedSourceType: new SourceType(typeof(IComplexEventService)));

        public static IProcessAction DisplayError => new ProcessAction(
                        action: cp =>
                        {
                            MessageBox.Show(cp.Messages["ProcessEventError"].Exception.Message + "-----" +
                                            cp.Messages["ProcessEventError"].Exception.StackTrace);

                            
                            return new FailedMessageData(cp, new StateEventInfo(cp.Actor.Process.Id,Context.Process.Events.Error), cp.Actor.Process,cp.Actor.Source);
                        }, 
                        processInfo: cp => new StateCommandInfo(cp.Actor.Process.Id, Context.Process.Commands.Error),
                        expectedSourceType: new SourceType(typeof(IComplexEventService)));
        public static IProcessAction ShutDownApplication => new ProcessAction(
                        action: cp =>
                        {
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                    MessageBox.Show(cp.Messages["ProcessEventError"].Exception.Message + "-----" +
                                            cp.Messages["ProcessEventError"].Exception.StackTrace);
                                        Application.Current?.Shutdown();
                            
                            });
                            return null;
                        },
                        processInfo: cp => new StateCommandInfo(cp.Actor.Process.Id, Context.Process.Commands.Error),
                        expectedSourceType: new SourceType(typeof(IComplexEventService)));

        public static IProcessAction UpdateEntityViewState<TEntityView>() where TEntityView : IEntityView
        {
            return new ProcessAction(
                action:
                    cp =>
                    {
                        var ps = new ProcessState<TEntityView>(
                             process: cp.Actor.Process,
                             entity: cp.Messages["EntityView"].Entity,
                             info: new StateInfo(cp.Actor.Process.Id, new State($"Loaded {typeof(TEntityView).Name} Data", $"Loaded{typeof(TEntityView).Name}", "")));
                        return new UpdateProcessState<TEntityView>(
                                    state: ps,
                                    process: cp.Actor.Process,
                                    processInfo: new StateCommandInfo(cp.Actor.Process.Id, Context.Process.Commands.UpdateState),
                                    source: cp.Actor.Source);
                    },
                processInfo:
                    cp =>
                        new StateCommandInfo(cp.Actor.Process.Id,
                            Context.Process.Commands.UpdateState),
                // take shortcut cud be IntialState
                expectedSourceType: new SourceType(typeof(IComplexEventService)));
        }

        public static IProcessAction UpdateEntityViewStateList<TEntityView>() where TEntityView : IEntityView
        {
            return new ProcessAction(
                action:
                    cp =>
                    {
                        var ps = new ProcessStateList<TEntityView>(
                             process: cp.Actor.Process,
                             entity: ((List<TEntityView>)cp.Messages["EntityViewSet"].EntitySet).FirstOrDefault(),
                             entitySet: cp.Messages["EntityViewSet"].EntitySet,
                             selectedEntities: new List<TEntityView>(),
                             stateInfo: new StateInfo(cp.Actor.Process.Id, new State($"Loaded {typeof(TEntityView).Name} Data", $"Loaded{typeof(TEntityView).Name}", "")));
                        return new UpdateProcessStateList<TEntityView>(
                                    state: ps,
                                    process: cp.Actor.Process,
                                    processInfo: new StateCommandInfo(cp.Actor.Process.Id, Context.Process.Commands.UpdateState),
                                    source: cp.Actor.Source);
                    },
                processInfo:
                    cp =>
                        new StateCommandInfo(cp.Actor.Process.Id,
                            Context.Process.Commands.UpdateState),
                // take shortcut cud be IntialState
                expectedSourceType: new SourceType(typeof(IComplexEventService)));
        }

        public static IProcessAction RequestState<TEntityView>(Expression<Func<TEntityView, object>> property) where TEntityView : IEntityView
        {
            return new ProcessAction(
                action: cp =>
                {

                    var key = default(TEntityView).GetMemberName(property);
                    var value = cp.Messages["CurrentEntity"].Entity.Id;
                    var changes = new Dictionary<string, dynamic>() { { key, value } };
                    return new GetEntityViewWithChanges<TEntityView>(changes,
                         new StateCommandInfo(cp.Actor.Process.Id, Context.EntityView.Commands.GetEntityView),
                         cp.Actor.Process, cp.Actor.Source);
                },
                processInfo: cp =>
                    new StateCommandInfo(cp.Actor.Process.Id,
                        Context.EntityView.Commands.GetEntityView),
                // take shortcut cud be IntialState
                expectedSourceType: new SourceType(typeof(IComplexEventService))

                );
        }

        public static IProcessAction RequestStateList<TEntityView>(Expression<Func<TEntityView, object>> property ) where TEntityView : IEntityView
        {
            return new ProcessAction(
                action: cp =>
                {
                    
                    var key = default(TEntityView).GetMemberName(property);
                    var value = cp.Messages["CurrentEntity"].Entity.Id;
                    var changes = new Dictionary<string, dynamic>() { {key,value} };
                   return new LoadEntityViewSetWithChanges<TEntityView, IExactMatch>(changes,
                        new StateCommandInfo(cp.Actor.Process.Id, Context.EntityView.Commands.GetEntityView),
                        cp.Actor.Process, cp.Actor.Source);
                },
                processInfo: cp =>
                    new StateCommandInfo(cp.Actor.Process.Id,
                        Context.EntityView.Commands.GetEntityView),
                // take shortcut cud be IntialState
                expectedSourceType: new SourceType(typeof (IComplexEventService))

                );
        }

        public class SignIn
        {
            public static IProcessAction IntializeSigninProcessState => new ProcessAction(
                action: cp =>
                {
                    var ps = new ProcessState<ISignInInfo>(
                        process: cp.Actor.Process,
                        entity: NullEntity<ISignInInfo>.Instance,
                        info: new StateInfo(cp.Actor.Process.Id,
                            new State(name: "AwaitUserName", status: "Waiting for User Name",
                                notes:
                                    "Please Enter your User Name. If this is your First Time Login In please Contact the Receptionist for your user info.")));
                    return new UpdateProcessState<ISignInInfo>(ps,
                        new StateCommandInfo(cp.Actor.Process.Id, Context.Process.Commands.UpdateState),
                        cp.Actor.Process, cp.Actor.Source);

                },
                processInfo: cp => new StateCommandInfo(cp.Actor.Process.Id, Context.Process.Commands.CreateState),
                // take shortcut cud be IntialState
                expectedSourceType: new SourceType(typeof (IComplexEventService)));

            public static IProcessAction UserNameFound => new ProcessAction(
                action: cp =>
                {
                    var ps = new ProcessState<ISignInInfo>(cp.Actor.Process, cp.Messages["UserNameFound"].Entity,
                        new StateInfo(cp.Actor.Process.Id, "WelcomeUser",
                            $"Welcome {cp.Messages["UserNameFound"].Entity.Usersignin}", "Please Enter your Password"));
                    return new UpdateProcessState<ISignInInfo>(ps,
                        new StateCommandInfo(cp.Actor.Process.Id, Context.Process.Commands.UpdateState),
                        cp.Actor.Process, cp.Actor.Source);
                },
                processInfo: cp => new StateCommandInfo(cp.Actor.Process.Id, Context.Process.Commands.UpdateState),
                expectedSourceType: new SourceType(typeof (IComplexEventService))
                );

            public static IProcessAction SetProcessStatetoValidatedUser => new ProcessAction(
                action: cp =>
                {
                    var ps = new ProcessState<ISignInInfo>(cp.Actor.Process, cp.Messages["ValidatedUser"].Entity,
                        new StateInfo(cp.Actor.Process.Id, "UserValidated",
                            $"User: {cp.Messages["ValidatedUser"].Entity.Usersignin} Validated", "User Validated"));
                    return new UpdateProcessState<ISignInInfo>(ps,
                        new StateCommandInfo(cp.Actor.Process.Id, Context.Process.Commands.UpdateState),
                        cp.Actor.Process, cp.Actor.Source);
                },
                processInfo: cp => new StateCommandInfo(cp.Actor.Process.Id, Context.Process.Commands.UpdateState),
                expectedSourceType: new SourceType(typeof (IComplexEventService)));


            public static IProcessAction UserValidated => new ProcessAction(
                action:
                    cp =>
                        new UserValidated(cp.Messages["ValidatedUser"].Entity,
                            new StateEventInfo(cp.Actor.Process.Id, Context.Domain.Events.DomainEventPublished),
                            cp.Actor.Process, cp.Actor.Source),
                processInfo: cp => new StateCommandInfo(cp.Actor.Process.Id, Context.Domain.Commands.PublishDomainEvent),
                expectedSourceType: new SourceType(typeof (IComplexEventService))
                );


        }


        

        public class PatientInfo
        {
            public static IProcessAction IntializePatientInfoSummaryProcessState => new ProcessAction(
                action:
                    cp =>
                        new LoadEntityViewSetWithChanges<IPatientInfo,IExactMatch>(new Dictionary<string, dynamic>(),
                            new StateCommandInfo(3, Context.EntityView.Commands.LoadEntityViewSetWithChanges),
                            cp.Actor.Process, cp.Actor.Source),
                processInfo:
                    cp =>
                        new StateCommandInfo(cp.Actor.Process.Id,
                            Context.EntityView.Commands.LoadEntityViewSetWithChanges),
                // take shortcut cud be IntialState
                expectedSourceType: new SourceType(typeof (IComplexEventService)));


            public static IProcessAction UpdatePatientInfoState => new ProcessAction(
                action:
                    cp =>
                    {
                       var ps = new ProcessStateList<IPatientInfo>(
                            process: cp.Actor.Process,
                            entity: ((List<IPatientInfo>)cp.Messages["EntityViewSet"].EntitySet).FirstOrDefault(),
                            entitySet: cp.Messages["EntityViewSet"].EntitySet,
                            selectedEntities: new List<IPatientInfo>(),
                            stateInfo: new StateInfo(3, new State("Loaded IPatientInfo Data", "LoadedIPatientData", "")));
                        return new UpdateProcessStateList<IPatientInfo>(
                                    state:ps,
                                    process:cp.Actor.Process,
                                    processInfo: new StateCommandInfo(cp.Actor.Process.Id, Context.Process.Commands.UpdateState), 
                                    source:cp.Actor.Source);
                    },
                processInfo:
                    cp =>
                        new StateCommandInfo(cp.Actor.Process.Id,
                            Context.Process.Commands.UpdateState),
                // take shortcut cud be IntialState
                expectedSourceType: new SourceType(typeof(IComplexEventService)));

           

           


            ////// Interview Info


            // patient response

            ////// QuestionList actions
            /// 
            public static IProcessAction IntializeQuestionListProcessState => new ProcessAction(
           action:
                   cp =>
                       new LoadEntityViewSetWithChanges<IQuestionInfo, IExactMatch>(new Dictionary<string, dynamic>(),
                           new StateCommandInfo(3, Context.EntityView.Commands.LoadEntityViewSetWithChanges),
                           cp.Actor.Process, cp.Actor.Source),
               processInfo:
                   cp =>
                       new StateCommandInfo(cp.Actor.Process.Id,
                           Context.EntityView.Commands.LoadEntityViewSetWithChanges),
               // take shortcut cud be IntialState
               expectedSourceType: new SourceType(typeof(IComplexEventService)));


            public static IProcessAction UpdateQuestionListState => new ProcessAction(
                action:
                    cp =>
                    {
                        var ps = new ProcessStateList<IQuestionInfo>(
                             process: cp.Actor.Process,
                             entity: ((List<IQuestionInfo>)cp.Messages["EntityViewSet"].EntitySet).FirstOrDefault(),
                             entitySet: cp.Messages["EntityViewSet"].EntitySet,
                             selectedEntities: new List<IQuestionInfo>(),
                             stateInfo: new StateInfo(3, new State("Loaded IQuestionInfo Data", "Loaded QuestionList Data", "")));
                        return new UpdateProcessStateList<IQuestionInfo>(
                                    state: ps,
                                    process: cp.Actor.Process,
                                    processInfo: new StateCommandInfo(cp.Actor.Process.Id, Context.Process.Commands.UpdateState),
                                    source: cp.Actor.Source);
                    },
                processInfo:
                    cp =>
                        new StateCommandInfo(cp.Actor.Process.Id,
                            Context.Process.Commands.UpdateState),
                // take shortcut cud be IntialState
                expectedSourceType: new SourceType(typeof(IComplexEventService)));

            public static IProcessAction RequestQuestionList => new ProcessAction(
                action:
                    cp =>
                        new LoadEntityViewSetWithChanges<IQuestionInfo, IExactMatch>(new Dictionary<string, dynamic>()
                                    {
                                        {nameof(IQuestionInfo.InterviewId), cp.Messages["CurrentInterview"].Entity.Id },
                                    },
                            new StateCommandInfo(3, Context.EntityView.Commands.LoadEntityViewSetWithChanges),
                            cp.Actor.Process, cp.Actor.Source),
                processInfo:
                    cp =>
                        new StateCommandInfo(cp.Actor.Process.Id,
                            Context.EntityView.Commands.LoadEntityViewSetWithChanges),
                // take shortcut cud be IntialState
                expectedSourceType: new SourceType(typeof(IComplexEventService)));

        }


    }


}
