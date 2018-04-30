using Microsoft.Practices.Unity;
using PrismMVVMLibrary.DesignTime;

namespace SalesTaskPad.Design
{
    public class SalesTaskPadVMDesign : SalesTaskPadVM
    {
        /// <summary>
        /// Design constructor.  Design Time data can be created here.
        /// </summary>
        public SalesTaskPadVMDesign()
            : base(new DesignUnityContainer(), new DesignEventAggregator())
        {
            //+ Set your design data here.  It will show up in expression blend and your designer.
            ExampleData = "Design Value";
        }
    }
}
