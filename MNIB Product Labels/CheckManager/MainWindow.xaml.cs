using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using MNIB_Distribution_Manager;

namespace CheckManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
            InitializeComponent();
            im = FindResource("CheckViewModelDataSource") as CheckViewModel;
        }

        private void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            var exception = e.Exception;
            do
                exception = exception.InnerException;
            while (exception.InnerException != null);

            MessageBox.Show(exception.Message + "|" + Properties.Settings.Default.CheckManagerConnectionString);
        }

        private CheckViewModel im;
        private void MoveWindow(object sender, MouseButtonEventArgs e)
        {
            DragMove();

        }
        private void SwitchWindowState(object sender, MouseButtonEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
                return;
            }
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;

            }
        }
        private void MinimizeWindow(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void CloseWindow(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
           
        }

       

        private void PrintAll(object sender, RoutedEventArgs e)
        {
            im.Print();
        }

        private void SearchTxt_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                im.Search(SearchTxt.Text);
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
                ((ListBox)sender).ScrollIntoView(e.AddedItems[0]);
        }


        private void TextBlock_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
          //  im.Print((sender as FrameworkElement).DataContext as Cheque);
        }

       
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
         
        }

       

   

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //im?.SetExport(new MNIBDBDataContext());
        }

        

      

     

        private void ProcessCheque(object sender, MouseButtonEventArgs e)
        {
           
            im.ProcessCheque();
            //FrameworkElement rpt = (FrameworkElement)DailyReportGD;
            //PrintClass.Print(ref rpt);
            //ReportViewer.Visibility = Visibility.Hidden;
        }

       

        private void Grid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ReportViewer.Visibility = Visibility.Hidden;
        }


        private void ListBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            
          
        }

        private void ChequeDateChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void SavePayee(object sender, RoutedEventArgs e)
        {
            im.SavePayee();
        }

        private void RefreshCheques(object sender, RoutedEventArgs e)
        {
            im.GetCheques();
        }

        private void UpdateSignatures(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            if (im.CurrentCheque == null || im.CurrentCheque.Voucher.Prepared == null) return;
            if (!(sender is TextBox textBox)) return;
            if(textBox.Text == im.CurrentCheque.Voucher.Prepared.Signatures.ToString()) return;
            textBox.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
            im.UpdateSignatures();

        }

        private void ClearFilters(object sender, RoutedEventArgs e)
        {
            im.ChequeDate = DateTime.Parse("1/1/1753 12:00:00 AM");
            im.ChequeStatus = null;
            im.SearchTxt = "";
            im.DistributionAccount = "";
            im.CashAccount = "";
        }

        private void SaveVoucher(object sender, TextChangedEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;
            textBox.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
            im.SaveNotes();
        }
    }
}
