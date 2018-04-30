using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using RMSDataAccessLayer;
using System.Threading.Tasks;
using SalesRegion;


namespace LeftRegion
{
    /// <summary>
    /// Interaction logic for TransactionView.xaml
    /// </summary>
    public partial class SuppView : UserControl
    {
        public SuppView()
        {
            InitializeComponent();

            // Setup the view model context
            DataContext = SuppVM.Instance;
            tvm = SuppVM.Instance;
           

        }
        private SuppVM tvm;
        private void SearchPatient(object sender, RoutedEventArgs e)
        {

        }

        private void SearchMedication(object sender, RoutedEventArgs e)
        {

        }

        private void SearchDoctor(object sender, RoutedEventArgs e)
        {

        }

        private void SearchTransactions(object sender, RoutedEventArgs e)
        {

        }

        private async void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e == null) return;
            if (e.Key == Key.Enter)
            {
                if (tvm != null)
                {
                    tvm.SearchResults = new System.Collections.ObjectModel.ObservableCollection<Prescription>(){};
                
                    
                    Status.Text = "Searching";

                   
                        if (Application.Current != null)
                            Application.Current.Dispatcher.Invoke(
                                () => { if (SearchBox != null) tvm.SearchText = SearchBox.Text; });

                        await Task.Run(() => tvm.SearchTransactions()).ConfigureAwait(false);

                        if (Application.Current != null)
                            Application.Current.Dispatcher.Invoke(
                                () => { Status.Text = ""; });
                    
                   
                }
            }
        }

        //private Prescription GetTransactionData(Prescription searchView)
        //{
        //    using (var ctx = new RMSModel())
        //    {
        //        return ctx.TransactionBase.OfType<Prescription>()
        //              .Include(x => x.Transactions)
        //              .Include("ParentTransaction.Transactions.TransactionEntries.TransactionEntryItem")
        //              .Include("Transactions.TransactionEntries.TransactionEntryItem")
        //              .Include(x => x.TransactionEntries)
        //            .Include("TransactionEntries.TransactionEntryItem")
        //              .Include(x => x.Patient)
        //              .Include(x => x.Doctor)
        //            .FirstOrDefault(x => x.TransactionId == searchView.TransactionId);
        //    }
        //}


        private void AutoRepeatBtn_Click(object sender, RoutedEventArgs e)
        {
            if (tvm == null) return;
            var i = sender as FrameworkElement;
            if (i != null)
            {
                var p = (Prescription) i.DataContext; //GetTransactionData()
                var rp = p.TransactionId;
                SalesVM.Instance.GoToTransaction(rp);
                //tvm.TransactionData = rp;
               tvm.AutoRepeat(null);
            }

        }


        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {
            if (tvm != null)
            {
                var frameworkElement = sender as FrameworkElement;
                if (frameworkElement != null)
                    tvm.TransactionData = (Prescription)frameworkElement.DataContext;//GetTransactionData()
            }
            e.Handled = true;
        }

        private void NewPrescription(object sender, RoutedEventArgs e)
        {
            if (tvm != null)
            {
                var frameworkElement = sender as FrameworkElement;
                if (frameworkElement != null)
                    tvm.TransactionData = (Prescription)frameworkElement.DataContext;//GetTransactionData()
                tvm.CopyPrescription();
            }
        }


        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            SalesRegion.SalesVM.Instance.ShowInactiveItems = true;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            SalesRegion.SalesVM.Instance.ShowInactiveItems = false;
        }
       


       

       

    }
}
