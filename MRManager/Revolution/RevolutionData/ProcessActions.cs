using System;
using SystemInterfaces;
using SystemMessages;
using Actor.Interfaces;
using Common;
using DataEntites;
using DomainMessages;
using EventMessages;
using EventMessages.Events;
using Interfaces;
using RevolutionEntities.Process;

namespace RevolutionData
{
    public static class ProcessActions
    {
        public static IProcessAction StartProcess = new ProcessAction(
                        action: cp => new SystemProcessStarted(
                            new StateEventInfo(cp.Actor.Process.Id, Context.Process.Events.ProcessStarted),
                            cp.Actor.Process, cp.Actor.Source),
                        processInfo: cp => new StateCommandInfo(cp.Actor.Process.Id, Context.Process.Commands.StartProcess),
                        expectedSourceType: new SourceType(typeof(IComplexEventService)));

        public static IProcessAction CompleteProcess = new ProcessAction(
                        action: cp => new SystemProcessCompleted(new StateEventInfo(cp.Actor.Process.Id, Context.Process.Events.ProcessCompleted),cp.Actor.Process, cp.Actor.Source),
                        processInfo: cp => new StateCommandInfo(cp.Actor.Process.Id, Context.Process.Commands.CompleteProcess),
                        expectedSourceType: new SourceType(typeof(IComplexEventService)));

        public static IProcessAction ShutActorDown = new ProcessAction(
                        action: cp => new ActorTerminated(cp.Actor, new StateEventInfo(cp.Actor.Process.Id, Context.Actor.Events.ActorTerminated), cp.Actor.Process, cp.Actor.Source),//actor.ActorRef().GracefulStop(TimeSpan.FromSeconds(10)),TODO: figure out way to close actor without messing up command not working),
                        processInfo: cp => new StateCommandInfo(cp.Actor.Process.Id, Context.Actor.Commands.TerminateActor),
                        expectedSourceType: new SourceType(typeof (IComplexEventService)));

        public static IProcessAction IntializeSigninProcessState = new ProcessAction(
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
                        processInfo: cp => new StateCommandInfo(cp.Actor.Process.Id, Context.Process.Commands.CreateState),// take shortcut cud be IntialState
                        expectedSourceType: new SourceType(typeof(IComplexEventService)));
        public static IProcessAction UserNameFound = new ProcessAction(
                        action: cp =>
                        {
                            var ps = new ProcessState<ISignInInfo>(cp.Actor.Process.Id, cp.Messages["UserNameFound"].Entity, new StateInfo(cp.Actor.Process.Id, "WelcomeUser", $"Welcome {cp.Messages["UserNameFound"].Entity.Usersignin}", "Please Enter your Password"));
                            return new UpdateProcessState<ISignInInfo>(ps,new StateCommandInfo(cp.Actor.Process.Id, Context.Process.Commands.UpdateState), cp.Actor.Process, cp.Actor.Source);
                        },
                        processInfo: cp => new StateCommandInfo(cp.Actor.Process.Id, Context.Process.Commands.UpdateState),
                        expectedSourceType:new SourceType(typeof(IComplexEventService))
                        );

        public static IProcessAction SetProcessStatetoValidatedUser = new ProcessAction(
                        action: cp =>
                        {
                            var ps = new ProcessState<ISignInInfo>(cp.Actor.Process.Id, cp.Messages["ValidatedUser"].Entity, new StateInfo(cp.Actor.Process.Id, "UserValidated",$"User: {cp.Messages["ValidatedUser"].Entity.Usersignin} Validated", "User Validated"));
                            return new UpdateProcessState<ISignInInfo>(ps,new StateCommandInfo(cp.Actor.Process.Id, Context.Process.Commands.UpdateState), cp.Actor.Process, cp.Actor.Source);
                        },
                        processInfo: cp => new StateCommandInfo(cp.Actor.Process.Id, Context.Process.Commands.UpdateState),
                        expectedSourceType:new SourceType(typeof(IComplexEventService)));


        public static IProcessAction UserValidated => new ProcessAction(
                        action: cp => new UserValidated(cp.Messages["ValidatedUser"].Entity,new StateEventInfo(cp.Actor.Process.Id, Context.Domain.Events.DomainEventPublished), cp.Actor.Process, cp.Actor.Source),
                        processInfo: cp => new StateCommandInfo(cp.Actor.Process.Id, Context.Domain.Commands.PublishDomainEvent),
                        expectedSourceType:new SourceType(typeof(IComplexEventService)) 
                        );
    }


}
