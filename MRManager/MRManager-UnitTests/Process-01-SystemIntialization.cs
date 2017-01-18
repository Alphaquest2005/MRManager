using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using SystemInterfaces;
using Actor.Interfaces;
using Common;
using CommonMessages;
using DataEntites;
using DataServices.Actors;
using Domain.Interfaces;
using EF.DBContexts;
using EF.Entities;
using EventAggregator;
using EventMessages;
using Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RevolutionEntities.Process;
using StartUp.Messages;
using Utilities;
using ViewMessages;
using ViewModel.Interfaces;
using ViewModels;
using RevolutionLogger;




namespace MRManager_UnitTests
{
    [TestClass]
    public class SystemIntializationProcess
    {

        public static ISystemSource Source => new Source(Guid.NewGuid(), "TestCase" + typeof(SystemIntializationProcess).GetFriendlyName(),new SourceType(typeof(SystemIntializationProcess)), new MachineInfo(Environment.MachineName, Environment.ProcessorCount));
        public void StartSystem()
        {
            if (File.Exists("MRManager-TEST-Logs.xml")) File.Delete("MRManager-TEST-Logs.xml");
            Logger.Initialize();
            var t = new MRManagerDBContext().GetType().Assembly;
            var x = new EFEntity<IEntity>().GetType().Assembly;
            BootStrapper.BootStrapper.Instance.StartUp(t, x, false);
            var mainWindow = MainWindowViewModel.Instance;

        }


        [TestMethod]
        public void SystemIntialization()
        {
            RegisterProcess1Events();
            StartSystem();
            Thread.Sleep(TimeSpan.FromSeconds(30));
            Process1Asserts();
            AssertProcessLogs();
        }


        public static void AssertProcessLogs()
        {
            foreach (var p in startedProcesses.ToArray())
            {
                EventMessageBus.Current.Publish(
                    new RequestProcessLog(
                        new StateCommandInfo(p.Process.Id, RevolutionData.Context.Process.Commands.CreateLog), p.Process, Source),
                    Source);
            }

            Thread.Sleep(TimeSpan.FromSeconds(5));
            Assert.IsTrue(ProcessLogs.Count == startedProcesses.Count);
        }

        private IProcessSystemMessage process2ServiceActorStarted;


       



        private static IProcessSystemMessage serviceManagerStarted;
        private static IProcessSystemMessage processServiceActorStarted;
        private static IProcessSystemMessage viewModelSupervisorStarted;
        private static IProcessSystemMessage processStarted;
        private static IProcessSystemMessage screenViewModelCreated;
        private static IProcessSystemMessage screenViewModelLoadedInMainWindowViewModel;
        private static IProcessSystemMessage processCompleted;
       // private IProcessSystemMessage mainWindowViewModelCreated;

        public static ConcurrentQueue<IProcessEventFailure> EventFailures = new ConcurrentQueue<IProcessEventFailure>();
        private static ConcurrentQueue<ISystemProcessStarted> startedProcesses = new ConcurrentQueue<ISystemProcessStarted>();
    


        public static void RegisterProcess1Events()
        {
            EventMessageBus.Current.GetEvent<ISystemProcessStarted>(Source).Subscribe(x => startedProcesses.Enqueue(x));
            EventMessageBus.Current.GetEvent<IProcessLogCreated>(Source).Subscribe(x => ProcessLogs.Enqueue(x));
            EventMessageBus.Current.GetEvent<IComplexEventLogCreated>(Source).Subscribe(x => ComplextEventLogs.Enqueue(x));
            EventMessageBus.Current.GetEvent<IProcessEventFailure>(Source).Subscribe(x => EventFailures.Enqueue(x));
            EventMessageBus.Current.GetEvent<IServiceStarted<IServiceManager>>(Source).Subscribe(x => serviceManagerStarted = x);
            
            Func<IProcessSystemMessage, bool> procesPredicate = x => x.Process.Id == 1;
            //IServiceStarted<IServiceManager> serviceStarted = null;
            EventMessageBus.Current.GetEvent<IServiceStarted<IProcessService>>(Source).Where(procesPredicate).Subscribe(x => processServiceActorStarted = x);
            EventMessageBus.Current.GetEvent<IServiceStarted<IViewModelSupervisor>>(Source).Where(procesPredicate).Subscribe(x => viewModelSupervisorStarted = x);
            EventMessageBus.Current.GetEvent<ISystemProcessStarted>(Source).Where(procesPredicate).Subscribe(x => processStarted = x);
            EventMessageBus.Current.GetEvent<IViewModelCreated<IScreenModel>>(Source).Where(procesPredicate).Subscribe(x => screenViewModelCreated = x);
            EventMessageBus.Current.GetEvent<IViewModelLoaded<IMainWindowViewModel, IScreenModel>>(Source).Subscribe(x => screenViewModelLoadedInMainWindowViewModel = x);
            EventMessageBus.Current.GetEvent<ISystemProcessCompleted>(Source).Where(procesPredicate).Subscribe(x => processCompleted = x);
        }

        public static ConcurrentQueue<IComplexEventLogCreated> ComplextEventLogs = new ConcurrentQueue<IComplexEventLogCreated>();

        public static ConcurrentQueue<IProcessLogCreated> ProcessLogs =new ConcurrentQueue<IProcessLogCreated>();

        public static void Process1Asserts()
        {
            Assert.IsTrue(EventFailures.Count == 0 && ProcessLogs.IsEmpty && ComplextEventLogs.Count == 0);
            Assert.IsNotNull(processServiceActorStarted);
            Assert.IsNotNull(processStarted);
            Assert.IsNotNull(viewModelSupervisorStarted);
            
            Assert.IsNotNull(screenViewModelCreated);
            Assert.IsNotNull(screenViewModelLoadedInMainWindowViewModel);
            Assert.IsNotNull(processCompleted);
        }




    }


}
