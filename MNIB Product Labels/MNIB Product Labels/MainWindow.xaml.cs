using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MNIB_Product_Labels
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            im = FindResource("LabelViewModelDataSource") as LabelViewModel;
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

        private LabelViewModel im;
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

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
           im.Search();
        }

        private void PrintAll(object sender, RoutedEventArgs e)
        {
            im.Print(im.PurchaseOrderDetails);
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var textBox = sender as TextBox;
                if (textBox != null)
                    textBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                im.Search();
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
                ((ListBox)sender).ScrollIntoView(e.AddedItems[0]);
        }


        private void TextBlock_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            im.Print((sender as FrameworkElement).DataContext as PurchaseOrderDetail);
        }
    }
}
