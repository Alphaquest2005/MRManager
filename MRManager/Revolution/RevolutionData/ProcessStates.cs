using SystemInterfaces;
using RevolutionEntities.Process;

namespace RevolutionData
{
    public static class ProcessStates
    {
        public static IState LogCreated => new State(name: "LogCreated", status: "Log Created");
        public static IState Completed => new State(name: "ProcessCompleted", status:"Process Completed");
    }
}
