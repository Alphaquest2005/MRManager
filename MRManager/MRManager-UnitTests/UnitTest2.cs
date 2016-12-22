using DataServices.Actors;
using EventMessages;
using Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utilities;

namespace MRManager_UnitTests
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestMethod1()
        {
           var str = typeof (EntityDataServiceActor<LoadEntityView<IAddressCities>>).GetFriendlyName();

        }
    }
}
