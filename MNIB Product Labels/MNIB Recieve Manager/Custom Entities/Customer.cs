namespace MNIB_Distribution_Manager
{
   public partial class Customer
    {
        public string Info
        {
            get { return CustomerName + " - " + CustomerAddress; }
        }
    }
}
