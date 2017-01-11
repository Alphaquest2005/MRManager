using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;
using RevolutionEntities.Process;

namespace RevolutionData.Context
{
    public class Process
    {
        public class Commands
        {
            public static IStateCommand CompleteProcess => new StateCommand(name: "CreateProcessCompletedMessage", status: "Create Process Completed Message", expectedEvent: Events.ProcessCompleted);
            public static IStateCommand StartProcess => new StateCommand("StartProcess", "Start Process", Events.ProcessStarted);
            public static IStateCommand CreateState => new StateCommand("CreateIntialState", "Create Intial State", Events.StateUpdated);
            public static IStateCommand UpdateState => new StateCommand("UpdateState", "Update State", Events.StateUpdated);
            public static IStateCommand Error => new StateCommand("UpdateState", "Update State", Events.Error);
            public static IStateCommand CreateLog => new StateCommand("CreateLog", "Create Process Log", Events.LogCreated);
            public static IStateCommand CreateComplexEventLog => new StateCommand("CreateComplexEventLog", "Create ComplexEvent Log", Events.ComplexEventLogCreated);
            public static IStateCommand PublishState => new StateCommand("RequestState", "Request Process State", Events.StatePublished);
        }

        public class Events
        {
            public static IStateEvent ProcessStarted => new StateEvent("ProcessStarted", "Process Started", "", Commands.StartProcess);
            public static IStateEvent ProcessTimeOut => new StateEvent("TimeOut", "Process Timed Out", "", Commands.Error);
            public static IStateEvent ProcessCompleted => new StateEvent("ProcessCompleted", "Process Completed", "", Commands.CompleteProcess);
            public static IStateEvent StateUpdated => new StateEvent("StateUpdated", "StateUpdated", "", Commands.UpdateState);
            public static IStateEvent LogCreated => new StateEvent("LogCreated", "Log Created", "", Commands.CreateLog);
            public static IStateEvent Error => new StateEvent("Error", "Log Created", "", Commands.Error);
            public static IStateEvent ComplexEventLogCreated => new StateEvent("ComplexEventLogCreated", "ComplexEvent Log Created", "", Commands.CreateComplexEventLog);
            public static IStateEvent StatePublished => new StateEvent("StatePublished", "Process State Published", "", Commands.PublishState);

            //closed Loop
        }

    }
}
