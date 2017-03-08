using System;
using System.Collections.Generic;
using SystemInterfaces;
using Common;
using EventAggregator;
using EventMessages;
using EventMessages.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RevolutionEntities.Process;
using Utilities;

namespace MRManager_UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        public ISystemSource Source => new Source(Guid.NewGuid(), "TestCase" + typeof(UnitTest1).GetFriendlyName(), new SourceType(typeof(UnitTest1)), new SystemProcess(new RevolutionEntities.Process.Process(1, 0, "Starting System", "Prepare system for Intial Use", "", new Agent("System")), new MachineInfo(Environment.MachineName, Environment.ProcessorCount)), new MachineInfo(Environment.MachineName, Environment.ProcessorCount));
        [TestMethod]
        public void TestMethod1()
        {
            ISystemProcess testProcess =
            new SystemProcess(new RevolutionEntities.Process.Process(-1, -1, "Testing", "Testing", "T", new Agent("UnitTest")),
                new MachineInfo("TestMachine", 1));
        EventMessageBus.Current.GetEvent<IComplexEventLogCreated>(Source).Subscribe(x => handlelog(x));
            EventMessageBus.Current.Publish(new ComplexEventLogCreated(new List<IComplexEventLog>(),new StateEventInfo(1, RevolutionData.Context.Process.Events.ComplexEventLogCreated),testProcess,Source), Source);
        }

        private void handlelog(IComplexEventLogCreated complexEventLogCreated)
        {
            Assert.IsNotNull(complexEventLogCreated);
        }
    }
}
