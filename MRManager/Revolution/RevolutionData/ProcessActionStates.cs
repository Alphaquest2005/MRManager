using SystemInterfaces;
using RevolutionEntities.Process;

namespace RevolutionData
{
    public static class ProcessActionStates
    {
        public static IState CreateProcessCompletedMessage => new State(name: "CreateProcessCompletedMessage", status: "Create Process Completed Message");
        public static IState ShutActorDown => new State(name:"ShutActorDown", status:"Shut Actor Down");
        public static IState CreateIntialState => new State("CreateIntialState","Create Intial State");
        public static IState UpdateState => new State("UpdateState", "Update State");
        public static IState PublishMessage => new State("PublishMessage", "Publish Message");
    }
}