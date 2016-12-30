using System;
using System.Collections.Generic;
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
using ViewMessages;
using ViewModel.Interfaces;
using ViewModels;


namespace MRManager_UnitTests
{
    [TestClass]
    public class SystemIntializationProcess
    {

        protected ISourceMessage SourceMessage => new SourceMessage(new MessageSource(this.ToString()), new MachineInfo(Environment.MachineName, Environment.ProcessorCount));
        [TestMethod]
        public void DataContextStarted()
        {
         

            IServiceStarted<IServiceManager> msgRecieved = null;
            EventMessageBus.Current.GetEvent<IServiceStarted<IServiceManager>>(SourceMessage)
                .Subscribe(x => msgRecieved = x);
            StartSystem();
            Thread.Sleep(TimeSpan.FromSeconds(60));
            Assert.IsNotNull(msgRecieved);
        }

        private void GetBodyView(IViewModelCreated<IViewModel> vm)
        {
            Assert.AreEqual(typeof(LoginViewModel), vm.ViewModel.GetType());
        }

        private void StartSystem()
        {

            var t = new MRManagerDBContext().GetType().Assembly;
            var x = new EFEntity<IEntity>().GetType().Assembly;
            var d = new NHDBContext();
            BootStrapper.BootStrapper.Instance.StartUp(d, t, x);

        }
    }
}
