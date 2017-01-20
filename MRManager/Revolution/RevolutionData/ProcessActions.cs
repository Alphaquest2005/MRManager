using System;
using System.Collections.Generic;
using SystemInterfaces;
using SystemMessages;
using Actor.Interfaces;
using Common;
using System.Linq;
using System.Reactive.Linq;
using DomainMessages;
using EventMessages;
using EventMessages.Events;
using Interfaces;
using RevolutionEntities;
using RevolutionEntities.Process;

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

        public static IProcessAction CompleteProcess => new ProcessAction(
                        action: cp => new SystemProcessCompleted(new StateEventInfo(cp.Actor.Process.Id, Context.Process.Events.ProcessCompleted),cp.Actor.Process, cp.Actor.Source),
                        processInfo: cp => new StateCommandInfo(cp.Actor.Process.Id, Context.Process.Commands.CompleteProcess),
                        expectedSourceType: new SourceType(typeof(IComplexEventService)));

        public static IProcessAction ShutActorDown => new ProcessAction(
                        action: cp =>
                        {
                           // ((dynamic)cp.Actor).ActorRef().GracefulStop(TimeSpan.FromSeconds(5));//,TODO: figure out way to close actor without messing up command not working),
                            return new ActorTerminated(cp.Actor,
                                new StateEventInfo(cp.Actor.Process.Id, Context.Actor.Events.ActorStopped),
                                cp.Actor.Process, cp.Actor.Source);
                        },//
                        processInfo: cp => new StateCommandInfo(cp.Actor.Process.Id, Context.Actor.Commands.StopActor),
                        expectedSourceType: new SourceType(typeof (IComplexEventService)));

       public class SignIn
        {
            public static IProcessAction IntializeSigninProcessState => new ProcessAction(
                action: cp =>
                {
                    var ps = new ProcessState<ISignInInfo>(
                        processId: cp.Actor.Process.Id,
                        entity: NullEntity<ISignInInfo>.Instance,
                        info:
                            new StateInfo(cp.Actor.Process.Id,
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
                    var ps = new ProcessState<ISignInInfo>(cp.Actor.Process.Id, cp.Messages["UserNameFound"].Entity,
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
                    var ps = new ProcessState<ISignInInfo>(cp.Actor.Process.Id, cp.Messages["ValidatedUser"].Entity,
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
                        new LoadEntityViewSetWithChanges<IPatientInfo>(new Dictionary<string, dynamic>(),
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
                            processId: 3,
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
        }


    }


}
