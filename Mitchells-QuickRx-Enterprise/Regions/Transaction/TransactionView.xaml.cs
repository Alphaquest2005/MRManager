
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using SalesRegion;


namespace Transaction
{
    /// <summary>
    /// Interaction logic for TransactionView.xaml
    /// </summary>
    public partial class TransactionView : UserControl
    {
        public TransactionView()
        {
            InitializeComponent();

            // Setup the view model context
            DataContext = SalesVM.Instance;
            if (SalesVM.Instance.ServerMode == true) this.DownloadQB.Visibility = Visibility.Hidden;

        }


        private async void DownloadQB_OnClick(object sender, RoutedEventArgs e)
        {
            await Task.Run(() => SalesVM.Instance.DownloadAllQBItems()).ConfigureAwait(false);
        }
    }
}
