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
}