using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MNIB_Distribution_Manager
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
            
           this.Close();
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
           im.Search();
        }

        private void PrintAll(object sender, RoutedEventArgs e)
        {
            im.Print(im.ExportDetails.ToList());
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
            im.Print((sender as FrameworkElement).DataContext as ExportDetail);
        }

        private int m_OldProductSelectedIndex = 0;

        private void ProductComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (im != null)
            {
                var box = ((ComboBox)sender);
                if (m_OldProductSelectedIndex == box.SelectedIndex) return;
                im.ResetExport();
                if (CanNavigateExport())
                {
                    // 
                    m_OldProductSelectedIndex = box.SelectedIndex;
                }
                else
                {
                    box.SelectedIndex = m_OldProductSelectedIndex;

                }


            }
        }

        private int m_OldHarvesterSelectedIndex = 0;

        private void HarvesterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (im != null)
            {
                var box = ((ComboBox) sender);
                if (m_OldHarvesterSelectedIndex == box.SelectedIndex) return;
               // 
                if (CanNavigateExport())
                {
                   
                    m_OldHarvesterSelectedIndex = box.SelectedIndex;
                }
                else
                {
                    box.SelectedIndex = m_OldHarvesterSelectedIndex;

                }
               
                
            }
        }
        DateTime? m_OldSelectedDate = DateTime.Today;
        private void DatePicker_OnSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (im != null)
            {
                var box = ((DatePicker)sender);
                if (m_OldSelectedDate == box.SelectedDate) return;
                if (CanNavigateExport())
                {
                    im.ResetExport();
                    m_OldSelectedDate = box.SelectedDate;
                }
                else
                {
                    box.SelectedDate = m_OldSelectedDate;
                }


            }
        }

        private bool CanNavigateExport()
        {
            if (im.VeifyBoxWeight())
            {
               if(! im.SetExport(new MNIBDBDataContext())) im.ResetExport();
                return true;
            }
            return false;
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!im.VeifyBoxWeight())
            {
                e.Cancel = true;
            }
        }

        private void DeleteBtn_OnClick(object sender, RoutedEventArgs e)
        {
            InputBox.Visibility = Visibility.Visible;

            
        }

   

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //im?.SetExport(new MNIBDBDataContext());
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {

            if (!im.ValidateUser(InputTextBox.Password)) return;
            im.AmendExportDetail();
            InputTextBox.Password = "";
            InputBox.Visibility = Visibility.Hidden;
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            InputBox.Visibility = Visibility.Hidden;
        }

        private async void ViewSummary(object sender, RoutedEventArgs e)
        {
            ReportViewer.Visibility = Visibility.Visible;
            DailySummary res = null;
            await Task.Run(() =>
                             {
                                res = im.PrepareDailySummary();
                             }).ConfigureAwait(false);

            await Dispatcher.BeginInvoke(new Action(() =>
             {
                 DailyReportGD.DataContext = res;
             }));
           
            
        }

        private void PrintReport(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement rpt = (FrameworkElement)DailyReportGD;
            PrintClass.Print(ref rpt);
            ReportViewer.Visibility = Visibility.Hidden;
        }

       

        private void Grid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ReportViewer.Visibility = Visibility.Hidden;
        }

        private void AmendBtn_OnClickBtn_OnClick(object sender, RoutedEventArgs e)
        {
            InputBox.Visibility = Visibility.Visible;
        }

        private void ListBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            
            if (im != null) im.SetCurrentExportDetailToNull();
        }


        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (im.SourceTransaction == "Sales Order")
            {
                HavesterGrd.Visibility = Visibility.Visible;
                BarCodeGrd.Visibility = Visibility.Collapsed;
                ExportNumberTxt.Focus();
            }

            else
            {
                HavesterGrd.Visibility = Visibility.Collapsed;
                BarCodeGrd.Visibility = Visibility.Visible;
                ExportNumberTxt.Focus();
            }
            im.TransactionNumber = "";
            im.Barcode = "";
            im.Product = null;
            im.SetCurrentExportDetailToNull();
        }

        private void ExportNumberTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }

        private void ExportNumberTxt_TargetUpdated(object sender, DataTransferEventArgs e)
        {
 if (im.SourceTransaction == "Sales Order")
            {
                HavestorCbo.Focus();
            }
            else
            {
                BarcodeTxt.Focus();
            }
        }
    }
}
