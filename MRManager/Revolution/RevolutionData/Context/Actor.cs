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
            public static IStateCommand StartActor => new StateCommand("CreateService", "Create Actor Service",Events.ActorStarted);
            public static IStateCommand StopActor => new StateCommand("ShutActorDown", "Shut Actor Down",Events.ActorStopped);
            public static IStateCommand CreateActor { get; set; }
        }
        public class Events
        {
            public static IStateEvent ActorStarted => new StateEvent("ServiceCreated", "Service Created", "");
            public static IStateEvent ActorStopped => new StateEvent("ActorShutDown", "Actor Terminated", "");
            public static IStateEvent ActionCreated => new StateEvent("ActionCreated", "Action Created", "");
        }

    }
}
