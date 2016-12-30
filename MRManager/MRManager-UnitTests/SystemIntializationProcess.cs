using System;
using System.Collections.Generic;
using SystemInterfaces;
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
        protected SourceMessage SourceMessage => new SourceMessage(new MessageSource(this.ToString()), new MachineInfo(Environment.MachineName, Environment.ProcessorCount));
        [TestMethod]
        public void ScreenModelRecieveLoginViewModelInBody()
        {
            var process =
                new SystemProcess(new Process(2, 0, "Test Proces", "This is a Test", "T", new Agent("TestManager")),
                    SourceMessage.MachineInfo);
            var vm = new LoginViewModel(process: process,
               eventSubscriptions: new List<IViewModelEventSubscription<IViewModel, IEvent>>(),
               eventPublications: new List<IViewModelEventPublication<IViewModel, IEvent>>()
               {

               }, commandInfo: new List<IViewModelEventCommand<IViewModel, IEvent>>(), orientation: typeof(IBodyViewModel));
            EventMessageBus.Current.GetEvent<ViewModelCreated<IViewModel>>(SourceMessage)
                .Subscribe(x => GetBodyView(x));
            EventMessageBus.Current.Publish(new ViewModelCreated<IViewModel>(vm,process,SourceMessage),SourceMessage);
        }

        private void GetBodyView(ViewModelCreated<IViewModel> vm)
        {
            Assert.AreEqual(typeof(LoginViewModel), vm.ViewModel.GetType());
        }

        private void StartSystem()
        {
            
            var d = new NHDBContext();
            
        }
    }
}
