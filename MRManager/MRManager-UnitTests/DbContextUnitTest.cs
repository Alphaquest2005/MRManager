using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using SystemInterfaces;
using Common;
using DataServices.Actors;
using EF.Entities;
using EventAggregator;
using EventMessages;
using EventMessages.Commands;
using Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RevolutionEntities.Process;
using Utilities;
using Process = RevolutionEntities.Process.Process;

namespace MRManager_UnitTests
{
    [TestClass]
    public class DbContextUnitTest
    {
        private ISystemProcess testProcess =
            new SystemProcess(new RevolutionEntities.Process.Process(-1, -1, "Testing", "Testing", "T", new Agent("UnitTest")),
                new MachineInfo("TestMachine", 1));

        protected ISystemSource Source
            =>
                new Source(Guid.NewGuid(), typeof(DbContextUnitTest).GetFriendlyName(), new SourceType(typeof(DbContextUnitTest)), new SystemProcess(new RevolutionEntities.Process.Process(1, 0, "Starting System", "Prepare system for Intial Use", "", new Agent("System")), new MachineInfo(Environment.MachineName, Environment.ProcessorCount)), new MachineInfo(Environment.MachineName, Environment.ProcessorCount));

        static IEntityCreated<IPersons> createdPerson;

        [TestMethod]
        public void EntityViewGetEntityById()
        {
            IEntityFound<ISignInInfo> signonInfo = null;
            EventMessageBus.Current.GetEvent<IEntityFound<ISignInInfo>>(Source).Subscribe(x => signonInfo = x);
            EventMessageBus.Current.GetEvent<IProcessEventFailure>(Source)
                .Subscribe(x => Debugger.Log(0, "Test", x.Exception.Message + ":-:" + x.Exception.StackTrace));
            var stateCommandInfo = new StateCommandInfo(testProcess.Id,RevolutionData.Context.EntityView.Commands.GetEntityView);
            var msg = new GetEntityViewById<ISignInInfo>(1,stateCommandInfo, testProcess, Source);
            msg.GetEntity();
            Thread.Sleep(2);
            Assert.IsNotNull(signonInfo);
        }

        [TestMethod]
        public void EntityViewGetEntityWithChanges()
        {
            IEntityViewWithChangesFound<ISignInInfo> signonInfo = null;
            EventMessageBus.Current.GetEvent<IEntityViewWithChangesFound<ISignInInfo>>(Source).Subscribe(x => signonInfo = x);
            EventMessageBus.Current.GetEvent<IProcessEventFailure>(Source)
                .Subscribe(x => Debugger.Log(0, "Test", x.Exception.Message + ":-:" + x.Exception.StackTrace));
            var msg = new GetEntityViewWithChanges<ISignInInfo>(new Dictionary<string, dynamic>() { { "Usersignin", "joe" } }, new StateCommandInfo(testProcess.Id, RevolutionData.Context.EntityView.Commands.GetEntityView), testProcess, Source);
            msg.GetEntityViewWithChanges();
            Thread.Sleep(2);
            Assert.IsNotNull(signonInfo);
        }

        [TestMethod]
        public void LoadEntityViewSetWithChanges_With_Changes()
        {
            IEntityViewSetWithChangesLoaded<ISignInInfo> signonInfo = null;
            EventMessageBus.Current.GetEvent<IEntityViewSetWithChangesLoaded<ISignInInfo>>(Source).Subscribe(x => signonInfo = x);
            EventMessageBus.Current.GetEvent<IProcessEventFailure>(Source)
                .Subscribe(x => Debugger.Log(0, "Test", x.Exception.Message + ":-:" + x.Exception.StackTrace));
            var msg = new LoadEntityViewSetWithChanges<ISignInInfo,IExactMatch>(new Dictionary<string, dynamic>() { { "Usersignin", "joe" } }, new StateCommandInfo(testProcess.Id, RevolutionData.Context.EntityView.Commands.GetEntityView), testProcess, Source);
            msg.LoadEntityViewSetWithChanges();
            Thread.Sleep(2);
            Assert.IsNotNull(signonInfo);
        }

        [TestMethod]
        public void LoadEntityViewSetWithChanges_With_No_Changes()
        {
            IEntityViewSetWithChangesLoaded<IPatientInfo> signonInfo = null;
            EventMessageBus.Current.GetEvent<IEntityViewSetWithChangesLoaded<IPatientInfo>>(Source).Subscribe(x => signonInfo = x);
            EventMessageBus.Current.GetEvent<IProcessEventFailure>(Source)
                .Subscribe(x => Debugger.Log(0, "Test", x.Exception.Message + ":-:" + x.Exception.StackTrace));
            var msg = new LoadEntityViewSetWithChanges<IPatientInfo,IExactMatch>(new Dictionary<string, dynamic>(), new StateCommandInfo(testProcess.Id, RevolutionData.Context.EntityView.Commands.GetEntityView), testProcess, Source);
            msg.LoadEntityViewSetWithChanges();
            Thread.Sleep(2);
            Assert.IsNotNull(signonInfo);
        }


        [TestMethod]
        public void Create()
        {

            EventMessageBus.Current.GetEvent<IEntityCreated<IPersons>>(Source).Subscribe(x => createdPerson = x);
            EventMessageBus.Current.GetEvent<IProcessEventFailure>(Source)
                .Subscribe(x => Debugger.Log(0, "Test", x.Exception.Message + ":-:" + x.Exception.StackTrace));
            var msg = new CreateEntity<IPersons>(new Persons(), new StateCommandInfo(testProcess.Id, RevolutionData.Context.Entity.Commands.CreateEntity), testProcess, Source);
            msg.CreateEntity();
            Thread.Sleep(2);
            Assert.IsNotNull(createdPerson);
        }



        [TestMethod]
        public void Update()
        {
            IEntityUpdated<IPersons> updatedPerson = null;
            EventMessageBus.Current.GetEvent<IEntityUpdated<IPersons>>(Source).Subscribe(x => updatedPerson = x);
            EventMessageBus.Current.GetEvent<IProcessEventFailure>(Source)
                .Subscribe(x => Debugger.Log(0, "Test", x.Exception.Message + ":-:" + x.Exception.StackTrace));
            var msg = new UpdateEntityWithChanges<IPersons>(createdPerson.Entity.Id, new Dictionary<string, dynamic>() {{"Name", "TestJoe"}}, new StateCommandInfo(testProcess.Id, RevolutionData.Context.Entity.Commands.UpdateEntity),
                testProcess, Source);
            msg.UpdateEntity();
            Thread.Sleep(2);
            Assert.IsNotNull(updatedPerson);
        }

        [TestMethod]
        public void GetEntityById()
        {
            IEntityFound<IPersons> getPerson = null;
            EventMessageBus.Current.GetEvent<IEntityFound<IPersons>>(Source).Subscribe(x => getPerson = x);
            EventMessageBus.Current.GetEvent<IProcessEventFailure>(Source)
                .Subscribe(x => Debugger.Log(0, "Test", x.Exception.Message + ":-:" + x.Exception.StackTrace));
            var msg = new GetEntityById<IPersons>(createdPerson.Entity.Id, new StateCommandInfo(testProcess.Id, RevolutionData.Context.Entity.Commands.FindEntity), testProcess, Source);
            msg.GetEntity();
            Thread.Sleep(2);
            Assert.IsNotNull(getPerson);
        }

        [TestMethod]
        public void GetEntityWithChanges()
        {
            IEntityWithChangesFound<IPersons> updatedPerson = null;
            EventMessageBus.Current.GetEvent<IEntityWithChangesFound<IPersons>>(Source)
                .Subscribe(x => updatedPerson = x);
            EventMessageBus.Current.GetEvent<IProcessEventFailure>(Source)
                .Subscribe(x => Debugger.Log(0, "Test", x.Exception.Message + ":-:" + x.Exception.StackTrace));
            var msg = new GetEntityWithChanges<IPersons>(new Dictionary<string, dynamic>() {{"Name", "TestJoe"}}, new StateCommandInfo(testProcess.Id, RevolutionData.Context.Entity.Commands.FindEntity),
                testProcess, Source);
            msg.GetEntity();
            Thread.Sleep(3);
            Assert.IsNotNull(updatedPerson);
        }

        [TestMethod]
        public void Delete()
        {
            IEntityDeleted<IPersons> deletedPerson = null;
            EventMessageBus.Current.GetEvent<IEntityDeleted<IPersons>>(Source).Subscribe(x => deletedPerson = x);
            EventMessageBus.Current.GetEvent<IProcessEventFailure>(Source)
                .Subscribe(x => Debugger.Log(0, "Test", x.Exception.Message + ":-:" + x.Exception.StackTrace));
            var msg = new DeleteEntity<IPersons>(createdPerson.Entity.Id, new StateCommandInfo(testProcess.Id, RevolutionData.Context.Entity.Commands.DeleteEntity), testProcess, Source);
            msg.DeleteEntity();
            Thread.Sleep(10);
            Assert.IsNotNull(deletedPerson);
        }

        [TestMethod]
        public void LoadEntitySet()
        {
            IEntitySetLoaded<IPersons> updatedPerson = null;
            EventMessageBus.Current.GetEvent<IEntitySetLoaded<IPersons>>(Source)
                .Subscribe(x => updatedPerson = x);
            EventMessageBus.Current.GetEvent<IProcessEventFailure>(Source)
                .Subscribe(x => Debugger.Log(0, "Test", x.Exception.Message + ":-:" + x.Exception.StackTrace));
            var msg = new LoadEntitySet<IPersons>(new StateCommandInfo(testProcess.Id, RevolutionData.Context.Entity.Commands.LoadEntitySet), testProcess, Source);
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
