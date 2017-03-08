
using System;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DataGrid2Excel;
using Microsoft.Win32;


namespace MNIB_Labels_Tracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            im = FindResource("TrackerViewModelDataSource") as TrackerViewModel;
            Dispatcher.UnhandledException += Dispatcher_UnhandledException;
        }

        private void Dispatcher_UnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            var exception = e.Exception;
            do
            {
                exception = exception.InnerException;
            }
            while (exception.InnerException == null);
            MessageBox.Show(exception.Message + "|" + exception.StackTrace);
        }

        private TrackerViewModel im;
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

       
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
                ((ListBox)sender).ScrollIntoView(e.AddedItems[0]);
        }

       
        private async void POSummary_Click(object sender, MouseButtonEventArgs e)
        {
            var lst = await im.GetPOSummary().ConfigureAwait(false);
            Grid.ItemsSource = null;
            Grid.ItemsSource = lst.DefaultView;
            
        }


        private void Send2Excel(object sender, RoutedEventArgs e)
        {
            var p = new ExportToExcel();
            p.GenerateReport(Grid);
        }

        private async void PODailyDetails(object sender, MouseButtonEventArgs e)
        {
            var lst = await im.GetPODetails().ConfigureAwait(false);
            Grid.ItemsSource = null;
            Grid.ItemsSource = lst.DefaultView;
        }

        private async void ImportCSV(object sender, RoutedEventArgs e)
        {
            try
            {
                var od = new OpenFileDialog();
                od.Title = "Import Entry Data";
                od.DefaultExt = ".csv";
                od.Filter = "CSV Files (.dat)|*.dat";
                od.Multiselect = true;
                var result = od.ShowDialog();
                if (result == true)
                {
                    bool overwrite = false;
                    var res =
                        MessageBox.Show(
                            "Do you want to Over Write Existing Items?, Click Yes to Replace or No to Skip or Cancel to Stop.",
                            "Existing Item Found", MessageBoxButton.YesNoCancel);
                    switch (res)
                    {
                        case MessageBoxResult.Yes:
                            overwrite = true;
                            break;
                        case MessageBoxResult.No:
                            break;
                        case MessageBoxResult.Cancel:
                            return;
                    }

                    foreach (var f in od.FileNames)
                    {
                        if (f.ToLower().EndsWith(".dat"))
                        {
                            await
                                SaveCSV.SaveCSVModel.Instance.ProcessDroppedFile(f, "GunData", overwrite)
                                    .ConfigureAwait(false);
                        }

                    }


                    MessageBox.Show("Complete");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void XferDailyDetails(object sender, MouseButtonEventArgs e)
        {
            var lst = await im.GetTransferDetails().ConfigureAwait(false);
            Grid.ItemsSource = null;
            Grid.ItemsSource = lst.DefaultView;
        }

        private async void XferSummary_Click (object sender, MouseButtonEventArgs e)
        {
            var lst = await im.GetTransferSummary().ConfigureAwait(false);
            Grid.ItemsSource = null;
            Grid.ItemsSource = lst.DefaultView;
        }

        private async void TransActivitySummary(object sender, MouseButtonEventArgs e)
        {
            var lst = await im.GetTransActivitySummary().ConfigureAwait(false);
            Grid.ItemsSource = null;
            Grid.ItemsSource = lst.DefaultView;
        }

        private async void TransActivityDetails(object sender, MouseButtonEventArgs e)
        {
            var lst = await im.GetTransActivityDetails().ConfigureAwait(false);
            Grid.ItemsSource = null;
            Grid.ItemsSource = lst.DefaultView;
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var dr = ((sender as FrameworkElement).DataContext as DataRowView).Row;
            if (dr.Table.TableName == "TransActivitySummary")
            {
                im.LotNumber = dr["LotNumber"].ToString();
                TransActivityDetails(null, null);
                im.LotNumber = "";
            }
            
        }

        private async void ViewOversShorts(object sender, MouseButtonEventArgs e)
        {
            var lst = await im.GetOversShorts().ConfigureAwait(false);
            Grid.ItemsSource = null;
            if (lst != null) Grid.ItemsSource = lst.DefaultView;
        }

        private async void ViewUnknownData(object sender, MouseButtonEventArgs e)
        {
            var lst = await im.GetUnknownGunData().ConfigureAwait(false);
            Grid.ItemsSource = null;
            if (lst != null) Grid.ItemsSource = lst.DefaultView;
        }

        private async void ViewGunData(object sender, MouseButtonEventArgs e)
        {
            var lst = await im.GetGunData().ConfigureAwait(false);
            Grid.ItemsSource = null;
            if (lst != null) Grid.ItemsSource = lst.DefaultView;
        }

        private async void ViewTransferOS(object sender, MouseButtonEventArgs e)
        {
            var lst = await im.GetTransferOS().ConfigureAwait(false);
            Grid.ItemsSource = null;
            if (lst != null) Grid.ItemsSource = lst.DefaultView;
        }

        private async void POReturnsSummary(object sender, MouseButtonEventArgs e)
        {
            var lst = await im.GetPOReturnsSummary().ConfigureAwait(false);
            Grid.ItemsSource = null;
            Grid.ItemsSource = lst.DefaultView;
        }

        private async void POReturnsDetails(object sender, MouseButtonEventArgs e)
        {
            var lst = await im.GetPOReturnDetails().ConfigureAwait(false);
            Grid.ItemsSource = null;
            Grid.ItemsSource = lst.DefaultView;
        }

        private async void ViewExportData(object sender, MouseButtonEventArgs e)
        {
            var lst = await im.GetReceiptReport().ConfigureAwait(false);
            Grid.ItemsSource = null;
            Grid.ItemsSource = lst.DefaultView;
        }
    }
}
