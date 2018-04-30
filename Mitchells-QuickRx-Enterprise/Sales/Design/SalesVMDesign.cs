using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using PrismMVVMLibrary.DesignTime;
using RMSDataAccessLayer;
using System.Linq;


namespace SalesRegion.Design
{
    public class SalesVMDesign : SalesVM
    {
        
        /// <summary>
        /// Design constructor.  Design Time data can be created here.
        /// </summary>
        /// 
        public SalesVMDesign()
            : base(new DesignUnityContainer(), new DesignEventAggregator(),new RegionManager())
        {
            //+ Set your design data here.  It will show up in expression blend and your designer.

            //Transaction testTrans = new Transaction { TransactionNumber = 1 };
            //testTrans.TransactionEntries.Add(new TransactionEntry { TransactionId = 1, TransactionEntryId = 1, ItemId = 4600, Price = 10, Quantity = 3 });
            //testTrans.TransactionEntries.Add(new TransactionEntry { TransactionId = 1, TransactionEntryId = 2, ItemId = 4601, Price = 50, Quantity = 1 });
            //TransactionData = testTrans;
        }
    }
}
