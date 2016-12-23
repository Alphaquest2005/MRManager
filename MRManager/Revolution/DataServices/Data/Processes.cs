using System.Collections.Generic;
using SystemInterfaces;
using SystemMessages;
using Core.Common.UI;
using RevolutionEntities.Process;
using ViewMessages;
using ViewModels;

namespace DataServices.Actors
{
    public static class Processes
    {
        public static readonly IEnumerable<IProcess> MessageProcesses = new List<IProcess>()
        {
                //new Process(0,0, "Uknown Process", "Unknown Process", "Unknown"),
                new Process(1,0, "Starting System", "Prepare system for Intial Use", "Start"),
                new Process(2,1,"User SignOn", "User Login", "User"),
                new Process(3,2,"Load User Screen", "User Screen", "UserScreen")
        };

        public static IEnumerable<ProcessExpectedEvent> ExpectedEvents = new List<ProcessExpectedEvent>()
        {
            new ProcessExpectedEvent
            (
                processId: 1,
                eventType: typeof(SystemProcessStarted),
                eventPredicate: (e) => e != null
            ),
            new ProcessExpectedEvent
            (
                processId: 1,
                eventType: typeof(ViewModelCreated<DynamicViewModel<ScreenModel>>),
                eventPredicate: (e) => e != null
            ),
            new ProcessExpectedEvent
            (
                processId: 1,
                eventType: typeof(ViewLoadedViewModel<DynamicViewModel<ScreenModel>>),
                eventPredicate: (e) => e != null
            ),
        };
    }


}