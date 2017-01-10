using SystemInterfaces;
using RevolutionEntities.Process;

namespace RevolutionData
{
    public static class StateCommands
    {
        public static IStateCommand CompleteProcess => new StateCommand(name: "CreateProcessCompletedMessage", status: "Create Process Completed Message");
        public static IStateCommand TerminateActor => new StateCommand(name:"ShutActorDown", status:"Shut Actor Down");
        public static IStateCommand CreateIntialState => new StateCommand("CreateIntialState","Create Intial State");
        public static IStateCommand UpdateState => new StateCommand("UpdateState", "Update State");
        public static IStateCommand PublishMessage => new StateCommand("PublishMessage", "Publish Message");
        public static IStateCommand SetProcessStateToCompleted => new StateCommand("ProcessCompleted", "Set Process State to Completed");
        public static IStateCommand CreateAction => new StateCommand("CreateAction", "Action to be Executed");
        public static IStateCommand CreateService => new StateCommand("CreateService", "Create Actor Service");
        public static IStateCommand StartProcess => new StateCommand("StartProcess", "Start Process");

        public class Data
        {
            public static IStateCommand GetEntityView => new StateCommand("GetEntityView", "Get Entity View Item");
        }
    }
}