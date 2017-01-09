using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;
using SystemMessages;
using Actor.Interfaces;
using DataEntites;
using DomainMessages;
using EventAggregator;
using EventMessages;
using Interfaces;
using RevolutionEntities.Process;

namespace RevolutionData
{
    public static class ProcessActions
    {
        public static IProcessAction ProcessCompleted = new ProcessAction(action: (cp) => new SystemProcessCompleted(cp.Actor.Process, cp.Actor.Source),
            processInfo: new ProcessStateDetailedInfo("Process Completed", "Create Process Completed Message"),
            expectedSourceType: new SourceType(typeof(IComplexEventService)));

        public static IProcessAction ShutActorDown = new ProcessAction(null,//actor.ActorRef().GracefulStop(TimeSpan.FromSeconds(10)),TODO: figure out way to close actor without messing up command not working),
            new ProcessStateDetailedInfo("Shut Actor Down", ""), new SourceType(typeof (IProcessService)));

        public static IProcessAction IntializeSigninProcessState = new ProcessAction(
                    action: cp =>
                    {
                        var ps = new ProcessState<ISignInInfo>(cp.Actor.Process.Id, NullEntity<ISignInInfo>.Instance, ProcessStateInfo.WaitingOnUserName);
                        return new ProcessStateMessage<ISignInInfo>(ps, cp.Actor.Process, cp.Actor.Source);
                    },
                    processInfo:new ProcessStateDetailedInfo("Intital SignIn ","Create Null SignInfo"),
                    expectedSourceType: new SourceType(typeof(IComplexEventService)));
        public static IProcessAction UserNameFound = new ProcessAction(
                    action: cp =>
                    {
                        var ps = new ProcessState<ISignInInfo>(cp.Actor.Process.Id, cp.Messages["UserNameFound"].Entity, new ProcessStateDetailedInfo($"Welcome {cp.Messages["UserNameFound"].Entity.Usersignin}", "Please Enter your Password"));
                        return new ProcessStateMessage<ISignInInfo>(ps, cp.Actor.Process, cp.Actor.Source);
                    },
                    processInfo: new ProcessStateDetailedInfo("Set process state to Welcome User",""),
                    expectedSourceType:new SourceType(typeof(IComplexEventService))
                    );

        public static IProcessAction SetProcessStatetoValidatedUser = new ProcessAction(
                    action: cp =>
                    {
                        var ps = new ProcessState<ISignInInfo>(cp.Actor.Process.Id, cp.Messages["ValidatedUser"].Entity, new ProcessStateDetailedInfo($"User: {cp.Messages["ValidatedUser"].Entity.Usersignin} Validated", "User Validated"));
                        return new ProcessStateMessage<ISignInInfo>(ps, cp.Actor.Process, cp.Actor.Source);
                    },
                    processInfo: new ProcessStateDetailedInfo("Set Process State to UserValidated",""),
                    expectedSourceType:new SourceType(typeof(IComplexEventService)));


        public static IProcessAction UserValidated => new ProcessAction(
                    action: cp => new UserValidated(cp.Messages["ValidatedUser"].Entity, cp.Actor.Process, cp.Actor.Source),
                    processInfo:new ProcessStateDetailedInfo("Domain Alert User Validated",""),
                    expectedSourceType:new SourceType(typeof(IComplexEventService)) 
                    );
    }
}
