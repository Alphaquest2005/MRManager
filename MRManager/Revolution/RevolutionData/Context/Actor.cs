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
            public static IStateCommand CreateAction => new StateCommand("CreateAction", "Action to be Executed",Events.ActionCreated);
            public static IStateCommand StartService => new StateCommand("CreateService", "Create Actor Service",Events.ServiceStarted);
            public static IStateCommand TerminateActor => new StateCommand("ShutActorDown", "Shut Actor Down",Events.ActorTerminated);
        }
        public class Events
        {
            public static IStateEvent ServiceStarted => new StateEvent("ServiceCreated", "Service Created", "");
            public static IStateEvent ActorTerminated => new StateEvent("ActorShutDown", "Actor Terminated", "");
            public static IStateEvent ActionCreated => new StateEvent("DomainEventPublished", "Domain Event Published", "");
        }

    }
}
