
using RMSDataAccessLayer;
using SalesRegion;
using SalesRegion.Messages;
using SimpleMvvmToolkit;

namespace Transaction
{
    public class TransactionVM : ViewModelBase<TransactionVM>
    {
      private static readonly TransactionVM _instance;
      static TransactionVM()
        {
            _instance = new TransactionVM();
          
        }

      public static TransactionVM Instance
        {
            get { return _instance; }
        }

        public TransactionVM()
        {
            RegisterToReceiveMessages<TransactionBase>(MessageToken.TransactionDataChanged, OnTransactionDataChanged);
        }

        private void OnTransactionDataChanged(object sender, NotificationEventArgs<TransactionBase> e)
        {
         //   TransactionData = e.Data;
        }

        //+ ToDo: Replace this with your own data fields
        private RMSDataAccessLayer.TransactionBase transactionData;
        public RMSDataAccessLayer.TransactionBase TransactionData
        {
            get { return SalesVM.Instance.TransactionData; }
            //get { return transactionData; }
            //set
            //{
            //    if (!object.Equals(transactionData, value))
            //    {
            //        transactionData = value;
            //        NotifyPropertyChanged(x => x.TransactionData);
            //    }
            //}
        }
    }
}
