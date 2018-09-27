using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CashSummaryManager.ViewModels;
using MNIB_Distribution_Manager;

namespace CashSummaryManager
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
        }

        private void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            var exception = e.Exception;
            do
               if(exception.InnerException != null) exception = exception.InnerException;
            while (exception.InnerException != null);

            MessageBox.Show(exception.Message + "|" + Properties.Settings.Default.CashSummaryConnectionString);
        }

        
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



        private void Grid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ReportViewer.Visibility = Visibility.Hidden;
        }


        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = CashBreakDown.Instance;
        }

        private void ToDrawerSelector(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = DrawerSelector.Instance;
        }

        private void UpdateRow1(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            //find this checkbox parent until getting to listitem layer.
           
            CashBreakDown.Instance.SaveRow((sender as FrameworkElement).DataContext as DrawerCashDetail);
        }

        private void AddRow(object sender, RoutedEventArgs e)
        {
            CashBreakDown.Instance.AddRow();
        }

        private void DeleteRow(object sender, RoutedEventArgs e)
        {
            CashBreakDown.Instance.DeleteRow();
        }

        private void SelectCurrentItem(object sender, KeyboardFocusChangedEventArgs e)
        {
            ListViewItem item = (ListViewItem)sender;
            item.IsSelected = true;
        }

        private void UpdateRow(object sender, TextChangedEventArgs e)
        {
            CashBreakDown.Instance.SaveRow((sender as FrameworkElement).DataContext as DrawerCashDetail);
        }

        private void SelectCurrentItem(object sender, MouseButtonEventArgs e)
        {
            ListViewItem item = (ListViewItem)sender;
            item.IsSelected = true;
        }


        private void UpdateRow(object sender, RoutedEventArgs e)
        {
            CashBreakDown.Instance.SaveRow((sender as FrameworkElement).DataContext as DrawerCashDetail);
        }

        private void PostSession(object sender, RoutedEventArgs e)
        {
            CashBreakDown.Instance.PostSession();
            ContentControl.Content = CashSummary.Instance;
        }

        private void ToCashBreakDown(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = CashBreakDown.Instance;
        }

        private void NextDrawer(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = DrawerSelector.Instance;
        }

        private void PrintCashSummary(object sender, RoutedEventArgs e)
        {
            FrameworkElement dependencyObject = (FrameworkElement)((FrameworkElement)sender).FindName("CashSummaryGrd");
            PrintClass.Print(ref dependencyObject);
        }
    }
}
