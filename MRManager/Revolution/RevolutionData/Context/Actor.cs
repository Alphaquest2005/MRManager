using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;
using RevolutionEntities.Process;

namespace RevolutionData.Context
{
    public class Actor
    {
        public class Commands
        {
            public static IStateCommand CreateAction => new StateCommand("CreateAction", "Action to be Executed").RegisterExpectedEvent(Events.ActionCreated);
            public static IStateCommand StartService => new StateCommand("CreateService", "Create Actor Service").RegisterExpectedEvent(Events.ServiceStarted);
            public static IStateCommand TerminateActor => new StateCommand(name: "ShutActorDown", status: "Shut Actor Down").RegisterExpectedEvent(Events.ActorTerminated);
        }
        public class Events
        {
            public static IStateEvent ServiceStarted => new StateEvent("ServiceCreated", "Service Created", "", Commands.StartService);
            public static IStateEvent ActorTerminated => new StateEvent("ActorShutDown", "Actor Terminated", "", Commands.TerminateActor);
            public static IStateEvent ActionCreated => new StateEvent("DomainEventPublished", "Domain Event Published", "", Commands.CreateAction);
        }

    }
}
