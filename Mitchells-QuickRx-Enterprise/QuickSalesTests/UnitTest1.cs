using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QuickSalesTests
{
    [TestClass]
    public class SearchSpeed
    {
        [TestMethod]
        public void Ten_Searches()
        {
            var vm = new SalesRegion.SalesVM();
            var now = DateTime.Now;
           
            vm.UpdateSearchList("a");
            vm.UpdateSearchList("b");
            vm.UpdateSearchList("c");
            vm.UpdateSearchList("d");
            vm.UpdateSearchList("e");
            vm.UpdateSearchList("f");
            vm.UpdateSearchList("g");
            vm.UpdateSearchList("h");
            vm.UpdateSearchList("i");
            Debug.WriteLine((now-DateTime.Now).TotalSeconds);
        }
    }
}
