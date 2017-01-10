using SystemInterfaces;
using RevolutionEntities.Process;

namespace RevolutionData
{
    public static class StateEvents
    {
        public static IStateEvent LogCreated => new StateEvent(name: "LogCreated", status: "Log Created", notes:"");
        public static IStateEvent ProcessCompleted => new StateEvent(name: "ProcessCompleted", status:"Process Completed", notes: "");
        public static IStateEvent Started => new StateEvent("Started", "Process Started", notes: "");
        public static IStateEvent UserNameFound => new StateEvent("UserNameFound", "User Name Found", notes: "Just UserName found");
        public static IStateEvent UserFound => new StateEvent("UserFound", "User Name & Password Found", notes: "Both UserName and PassWord");
        public static IStateEvent TimeOut => new StateEvent("TimeOut", "Process Timed Out", notes: "");
        public static IStateEvent ServiceCreated => new StateEvent("ServiceCreated", "Service Created", notes: "");
        public static IStateEvent ActorTerminated => new StateEvent("ActorShutDown", "Actor Terminated", notes: "");
        public static IStateEvent StateUpdated => new StateEvent("StateUpdated", "StateUpdated", notes: "");

        public class Data
        {
            public static IStateEvent EntityViewFound => new StateEvent("EntityViewFound", "Entity View Item Found", notes: "");
        }
    }
}
