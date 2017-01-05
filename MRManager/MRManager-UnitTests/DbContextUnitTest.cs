using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using SystemInterfaces;
using CommonMessages;
using DataServices.Actors;
using EF.Entities;
using EventAggregator;
using EventMessages;
using Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RevolutionEntities.Process;
using Process = RevolutionEntities.Process.Process;

namespace MRManager_UnitTests
{
    [TestClass]
    public class DbContextUnitTest
    {
        private ISystemProcess testProcess =
            new SystemProcess(new Process(-1, -1, "Testing", "Testing", "T", new Agent("UnitTest")),
                new MachineInfo("TestMachine", 1));

        protected ISourceMessage SourceMessage
            =>
                new SourceMessage(new MessageSource(this.ToString()),
                    new MachineInfo(Environment.MachineName, Environment.ProcessorCount));

       static IEntityCreated<IPersons> createdPerson;

        [TestMethod]
        public void EntityViewGetEntityById()
        {
            IEntityFound<ISignInInfo> signonInfo = null;
            EventMessageBus.Current.GetEvent<IEntityFound<ISignInInfo>>(SourceMessage).Subscribe(x => signonInfo = x);
            EventMessageBus.Current.GetEvent<IProcessEventFailure>(SourceMessage)
                .Subscribe(x => Debugger.Log(0, "Test", x.Exception.Message + ":-:" + x.Exception.StackTrace));
            var msg = new GetEntityViewById<ISignInInfo>(1, testProcess, SourceMessage);
            msg.GetEntity();
            Thread.Sleep(2);
            Assert.IsNotNull(signonInfo);
        }

        [TestMethod]
        public void Create()
        {

            EventMessageBus.Current.GetEvent<IEntityCreated<IPersons>>(SourceMessage).Subscribe(x => createdPerson = x);
            EventMessageBus.Current.GetEvent<IProcessEventFailure>(SourceMessage)
                .Subscribe(x => Debugger.Log(0, "Test", x.Exception.Message + ":-:" + x.Exception.StackTrace));
            var msg = new CreateEntity<IPersons>(new Persons(), testProcess, SourceMessage);
            msg.CreateEntity();
            Thread.Sleep(2);
            Assert.IsNotNull(createdPerson);
        }



        [TestMethod]
        public void Update()
        {
            IEntityUpdated<IPersons> updatedPerson = null;
            EventMessageBus.Current.GetEvent<IEntityUpdated<IPersons>>(SourceMessage).Subscribe(x => updatedPerson = x);
            EventMessageBus.Current.GetEvent<IProcessEventFailure>(SourceMessage)
                .Subscribe(x => Debugger.Log(0, "Test", x.Exception.Message + ":-:" + x.Exception.StackTrace));
            var msg = new UpdateEntity<IPersons>(createdPerson.Entity.Id, new Dictionary<string, dynamic>() {{"Name", "TestJoe"}},
                testProcess, SourceMessage);
            msg.UpdateEntity();
            Thread.Sleep(2);
            Assert.IsNotNull(updatedPerson);
        }

        [TestMethod]
        public void GetEntityById()
        {
            IEntityFound<IPersons> getPerson = null;
            EventMessageBus.Current.GetEvent<IEntityFound<IPersons>>(SourceMessage).Subscribe(x => getPerson = x);
            EventMessageBus.Current.GetEvent<IProcessEventFailure>(SourceMessage)
                .Subscribe(x => Debugger.Log(0, "Test", x.Exception.Message + ":-:" + x.Exception.StackTrace));
            var msg = new GetEntityById<IPersons>(createdPerson.Entity.Id, testProcess, SourceMessage);
            msg.GetEntity();
            Thread.Sleep(2);
            Assert.IsNotNull(getPerson);
        }

        [TestMethod]
        public void GetEntityWithChanges()
        {
            IEntityWithChangesFound<IPersons> updatedPerson = null;
            EventMessageBus.Current.GetEvent<IEntityWithChangesFound<IPersons>>(SourceMessage)
                .Subscribe(x => updatedPerson = x);
            EventMessageBus.Current.GetEvent<IProcessEventFailure>(SourceMessage)
                .Subscribe(x => Debugger.Log(0, "Test", x.Exception.Message + ":-:" + x.Exception.StackTrace));
            var msg = new GetEntityWithChanges<IPersons>(createdPerson.Entity.Id, new Dictionary<string, dynamic>() {{"Name", "TestJoe"}},
                testProcess, SourceMessage);
            msg.GetEntity();
            Thread.Sleep(3);
            Assert.IsNotNull(updatedPerson);
        }

        [TestMethod]
        public void Delete()
        {
            IEntityDeleted<IPersons> deletedPerson = null;
            EventMessageBus.Current.GetEvent<IEntityDeleted<IPersons>>(SourceMessage).Subscribe(x => deletedPerson = x);
            EventMessageBus.Current.GetEvent<IProcessEventFailure>(SourceMessage)
                .Subscribe(x => Debugger.Log(0, "Test", x.Exception.Message + ":-:" + x.Exception.StackTrace));
            var msg = new DeleteEntity<IPersons>(createdPerson.Entity.Id, testProcess, SourceMessage);
            msg.DeleteEntity();
            Thread.Sleep(10);
            Assert.IsNotNull(deletedPerson);
        }

        [TestMethod]
        public void LoadEntitySet()
        {
            IEntitySetLoaded<IPersons> updatedPerson = null;
            EventMessageBus.Current.GetEvent<IEntitySetLoaded<IPersons>>(SourceMessage)
                .Subscribe(x => updatedPerson = x);
            EventMessageBus.Current.GetEvent<IProcessEventFailure>(SourceMessage)
                .Subscribe(x => Debugger.Log(0, "Test", x.Exception.Message + ":-:" + x.Exception.StackTrace));
            var msg = new LoadEntitySet<IPersons>(testProcess, SourceMessage);
            msg.LoadEntitySet();
            Thread.Sleep(2);
            Assert.IsNotNull(updatedPerson);
            Assert.IsFalse(updatedPerson.Entities.Count == 0);
        }

        [TestMethod]
        public void LoadEntitySetWithFilter()
        {
        }

        [TestMethod]
        public void LoadEntitySetWithFilterWithIncludes()
        {
        }


    }
}
