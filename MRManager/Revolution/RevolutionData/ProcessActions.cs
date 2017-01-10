using SystemMessages;
using Actor.Interfaces;
using DataEntites;
using DomainMessages;
using EventMessages;
using Interfaces;
using RevolutionEntities.Process;

namespace RevolutionData
{
    public static class ProcessActions
    {
        public static IProcessAction ProcessCompleted = new ProcessAction(cp => new SystemProcessCompleted(new ProcessStateInfo(cp.Actor.Process,ProcessStates.Completed), cp.Actor.Source), cp => new ProcessStateInfo(cp.Actor.Process, ProcessActionStates.CreateProcessCompletedMessage), new SourceType(typeof(IComplexEventService)));

        public static IProcessAction ShutActorDown = new ProcessAction(null,//actor.ActorRef().GracefulStop(TimeSpan.FromSeconds(10)),TODO: figure out way to close actor without messing up command not working),
            cp => new ProcessStateInfo(cp.Actor.Process, ProcessActionStates.ShutActorDown), new SourceType(typeof (IProcessService)));

        public static IProcessAction IntializeSigninProcessState = new ProcessAction(cp =>
                    {
                        var ps = new ProcessState<ISignInInfo>(cp.Actor.Process.Id, NullEntity<ISignInInfo>.Instance, new ProcessStateInfo(cp.Actor.Process,ProcessStateInfos.AwaitUserName));
                        return new ProcessStateMessage<ISignInInfo>(ps, cp.Actor.Process, cp.Actor.Source);
                    }, cp => new ProcessStateInfo(cp.Actor.Process,ProcessActionStates.CreateIntialState), new SourceType(typeof(IComplexEventService)));
        public static IProcessAction UserNameFound = new ProcessAction(
                    action: cp =>
                    {
                        var ps = new ProcessState<ISignInInfo>(cp.Actor.Process.Id, cp.Messages["UserNameFound"].Entity, new DynamicProcessStateInfo(cp.Actor.Process,"WelcomeUser", $"Welcome {cp.Messages["UserNameFound"].Entity.Usersignin}", "Please Enter your Password"));
                        return new ProcessStateMessage<ISignInInfo>(ps, cp.Actor.Process, cp.Actor.Source);
                    },
                    processInfo: cp => new ProcessStateInfo(cp.Actor.Process, ProcessActionStates.UpdateState),
                    expectedSourceType:new SourceType(typeof(IComplexEventService))
                    );

        public static IProcessAction SetProcessStatetoValidatedUser = new ProcessAction(
                    action: cp =>
                    {
                        var ps = new ProcessState<ISignInInfo>(cp.Actor.Process.Id, cp.Messages["ValidatedUser"].Entity, new DynamicProcessStateInfo(cp.Actor.Process,"UserValidated",$"User: {cp.Messages["ValidatedUser"].Entity.Usersignin} Validated", "User Validated"));
                        return new ProcessStateMessage<ISignInInfo>(ps, cp.Actor.Process, cp.Actor.Source);
                    },
                    processInfo: cp => new ProcessStateInfo(cp.Actor.Process, ProcessActionStates.UpdateState),
                    expectedSourceType:new SourceType(typeof(IComplexEventService)));


        public static IProcessAction UserValidated => new ProcessAction(
                    action: cp => new UserValidated(cp.Messages["ValidatedUser"].Entity, cp.Actor.Process, cp.Actor.Source),
                    processInfo: cp => new ProcessStateInfo(cp.Actor.Process, ProcessActionStates.PublishMessage),
                    expectedSourceType:new SourceType(typeof(IComplexEventService)) 
                    );
    }
}
