using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using SystemInterfaces;
using Actor.Interfaces;
using CommonMessages;
using DataInterfaces;
using EF.DBContexts;
using EF.Entities;
using EventAggregator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NH.DBContext;
using RevolutionEntities.Process;
using StartUp.Messages;
using ViewMessages;
using ViewModel.Interfaces;
using ViewModels;


namespace MRManager_UnitTests
{
    [TestClass]
    public class SystemIntializationProcess
    {

        protected ISourceMessage SourceMessage => new SourceMessage(new MessageSource(this.ToString()), new MachineInfo(Environment.MachineName, Environment.ProcessorCount));
        private void StartSystem()
        {

            var t = new MRManagerDBContext().GetType().Assembly;
            var x = new EFEntity<IEntity>().GetType().Assembly;
            var d = new NHDBContext();
            BootStrapper.BootStrapper.Instance.StartUp(d, t, x);

        }

        private IProcessSystemMessage serviceActorStarted;
        private IProcessSystemMessage viewModelSupervisorStarted;
        private IProcessSystemMessage processStarted;
        private IProcessSystemMessage screenViewModelCreated;
        private IProcessSystemMessage screenViewModelLoadedInMainWindowViewModel;
        private IProcessSystemMessage processCompleted;
        private IProcessSystemMessage mainWindowViewModelCreated;

        [TestMethod]
        public void DataContextStarted()
        {
            var mainWindow = MainWindowViewModel.Instance;
            EventMessageBus.Current.GetEvent<IServiceStarted<IServiceManager>>(SourceMessage).Subscribe(x => ServiceManagerStarted(x));
            Func<IProcessSystemMessage, bool> procesPredicate = x => x.Process.Id == 1;
            //IServiceStarted<IServiceManager> serviceStarted = null;
            EventMessageBus.Current.GetEvent<IServiceStarted<IProcessService>>(SourceMessage).Where(procesPredicate).Subscribe(x => ProcessServiceActorStarted(x));
            EventMessageBus.Current.GetEvent<IServiceStarted<IViewModelSupervisor>>(SourceMessage).Where(procesPredicate).Subscribe(x => ViewModelSupervisorStarted(x));
            EventMessageBus.Current.GetEvent<ISystemProcessStarted>(SourceMessage).Where(procesPredicate).Subscribe(x => ProcessStarted(x));
            EventMessageBus.Current.GetEvent<IViewModelCreated<IScreenModel>>(SourceMessage).Where(procesPredicate).Subscribe(x => ScreenModelCreated(x));

            EventMessageBus.Current.GetEvent <IViewModelLoaded <IMainWindowViewModel, IScreenModel>>(SourceMessage).Subscribe(x => ScreenModelLoadedInMainWindowViewModel(x));
            EventMessageBus.Current.GetEvent <ISystemProcessCompleted>(SourceMessage).Where(procesPredicate).Subscribe(x => ProcessCompleted(x));

            
            StartSystem();
            Thread.Sleep(TimeSpan.FromSeconds(75));

            Assert.IsNotNull(processStarted);
            Assert.IsNotNull(viewModelSupervisorStarted);
            Assert.IsNotNull(serviceActorStarted);
            Assert.IsNotNull(screenViewModelCreated);
            Assert.IsNotNull(mainWindow);
            Assert.IsNotNull(screenViewModelLoadedInMainWindowViewModel);
            Assert.IsNotNull(processCompleted);
        }

        private void ProcessCompleted(IProcessSystemMessage processSystemMessage)
        {
            processCompleted = processSystemMessage;
        }

        private void ScreenModelLoadedInMainWindowViewModel(IProcessSystemMessage processSystemMessage)
        {
            screenViewModelLoadedInMainWindowViewModel = processSystemMessage;
        }

        private void ScreenModelCreated(IProcessSystemMessage processSystemMessage)
        {
            screenViewModelCreated = processSystemMessage;
        }

        [TestMethod]
        public void ProcessStarted(IProcessSystemMessage processSystemMessage)
        {
            processStarted = processSystemMessage;
        }
        [TestMethod]
        public void ViewModelSupervisorStarted(IProcessSystemMessage serviceStarted)
        {
            viewModelSupervisorStarted = serviceStarted;
        }
        [TestMethod]
        public void ProcessServiceActorStarted(IProcessSystemMessage serviceStarted)
        {
            serviceActorStarted = serviceStarted;
        }

        [TestMethod]
        public void ServiceManagerStarted(IServiceStarted<IServiceManager> serviceStarted)
        {
            Assert.IsNotNull(serviceStarted);
        }



    }
}
