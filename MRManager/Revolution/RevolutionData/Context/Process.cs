using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;
using Actor.Interfaces;
using RevolutionEntities.Process;

namespace RevolutionData.Context
{
    public class Process
    {
        public class Commands
        {
            public static IStateCommand CompleteProcess => new StateCommand(name: "CompleteProcess", status: "Create Process Completed Message", expectedEvent: Events.ProcessCompleted);
            public static IStateCommand StartProcess => new StateCommand("StartProcess", "Start Process", Events.ProcessStarted);
            public static IStateCommand CreateState => new StateCommand("CreateIntialState", "Create Intial State", Events.StateUpdated);
            public static IStateCommand UpdateState => new StateCommand("UpdateState", "Update State", Events.StateUpdated);
            public static IStateCommand Error => new StateCommand("UpdateState", "Update State", Events.Error);
            public static IStateCommand CreateLog => new StateCommand("CreateLog", "Create Process Log", Events.LogCreated);
            public static IStateCommand CreateComplexEventLog => new StateCommand("CreateComplexEventLog", "Create ComplexEvent Log", Events.ComplexEventLogCreated);
            public static IStateCommand PublishState => new StateCommand("RequestState", "Request Process State", Events.StatePublished);
            public static IStateCommand CleanUpProcess => new StateCommand("Cleanup Process", "Clean up Process", Events.ProcessCleanedUp);
            public static IStateCommand CurrentEntityChanged => new StateCommand("CurrentEntityChanged", "Process Current Entity Changed");
            public static IStateCommand ChangeCurrentEntity => new StateCommand("ChangeCurrentEntity", "Change Process CurrentEntity", Events.CurrentEntityChanged);
        }

        public class Events
        {
            public static IStateEvent ProcessStarted => new StateEvent("ProcessStarted", "Process Started","", new StateCommand("StartProcess", "Start Process"));
            public static IStateEvent ProcessTimeOut => new StateEvent("TimeOut", "Process Timed Out", "");
            public static IStateEvent ProcessCompleted => new StateEvent("ProcessCompleted", "Process Completed", "");
            public static IStateEvent StateUpdated => new StateEvent("StateUpdated", "StateUpdated", "");
            public static IStateEvent LogCreated => new StateEvent("LogCreated", "Log Created", "");
            public static IStateEvent Error => new StateEvent("Error", "Log Created", "");
            public static IStateEvent ComplexEventLogCreated => new StateEvent("ComplexEventLogCreated", "ComplexEvent Log Created", "");
            public static IStateEvent StatePublished => new StateEvent("StatePublished", "Process State Published", "");
            public static IStateEvent ProcessCleanedUp => new StateEvent("ProcessCleanUp", "Process CleanUp", "", new StateCommand("CleanupProcess", "Cleanup Process"));
            public static IStateEvent CurrentEntityChanged => new StateEvent("CurrentEntityChanged", "Process Current Entity Changed", "");
            //closed Loop
        }

    }
}
