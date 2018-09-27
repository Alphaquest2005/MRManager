using System.Collections.Generic;
using System.Windows.Documents;

namespace CashSummaryManager
{
    public partial class DrawerCashDetail
    {
        
        public double Total
        {
            get
            {
                if (CashTypeComponent == null) return 0;
                return Quantity * CashTypeComponent.CashComponent.UnitValue;
            }
        }

        partial void OnQuantityChanged()
        {
            SendPropertyChanged("Total");
        }
    }

    public partial class DrawSessionDetail
    {
        public List<DrawerCashDetail> CashDetails { get; set; }
    }

}