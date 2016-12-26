using System.Collections.Generic;
using SystemInterfaces;
using SystemMessages;
using Core.Common.UI;
using DataEntites;
using EF.Entities;
using RevolutionEntities.Process;
using ViewMessages;
using ViewModels;

namespace DataServices.Actors
{
    public static class Processes
    {
        public static readonly IEnumerable<IProcessInfo> ProcessInfos = new List<IProcessInfo>()
        {
                //new Process(0,0, "Uknown Process", "Unknown Process", "Unknown"),
                new ProcessInfo(1,0, "Starting System", "Prepare system for Intial Use", "Start"),
                new ProcessInfo<UserSignIn>(2,1,"User SignOn", "User Login", "User"),
                new ProcessInfo<UserSignIn>(3,2,"Load User Screen", "User Screen", "UserScreen")
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