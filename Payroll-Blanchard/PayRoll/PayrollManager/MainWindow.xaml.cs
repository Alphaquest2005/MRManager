using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PayrollManager
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
    public partial class MainWindow : Window//Elysium.Controls.Window
	{
		public MainWindow()
		{
			this.InitializeComponent();
         
			// Insert code required on object creation below this point.
		}

        private void Window_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

        }

        private void Grid_PreviewMouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void PayrollSetupBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BaseViewModel.slider.MoveTo("PayrollSetupSummaryEXP");
        }

        private void EmployeeBtn_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            BaseViewModel.slider.MoveTo("EmployeeSummaryListEXP");
        }

        private void HomeBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BaseViewModel.slider.MoveTo("IntroEXP");
        }

        private void PaymentsBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BaseViewModel.slider.MoveTo("AccountsSummaryEXP");
        }
        bool collapseHome = false;
        private void Expander_Expanded_1(object sender, RoutedEventArgs e)
        {
            FrameworkElement p = FooterBar;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(p); i++)
            {
                var child = VisualTreeHelper.GetChild(p, i);
                if (typeof(Expander).IsInstanceOfType(child) && child != sender)
                {
                    if (child == homeexpand && collapseHome == false)
                    {
                        collapseHome = true;
                    }
                   
                        (child as Expander).IsExpanded = false;
                    
                }
                    
            }
            if (((Expander)sender).Name == "homeexpand" )
            {
                collapseHome = false;
            }
            else
            {
                collapseHome = true;
                homeexpand.IsExpanded = false;
            }
          

        }

        private void editEmp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BaseViewModel.slider.MoveTo("EmployeeDetailsEXP");
        }

        private void EmpAcc_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BaseViewModel.slider.MoveTo("EmployeeAccountSummaryListEXP");
        }

        private void emppaysetup_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BaseViewModel.slider.MoveTo("PayrollEmployeeSetupDetailsBDR");
        }

        private void editEmp_Copy1_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            BaseViewModel.slider.MoveTo("PayCheckViewEXP");
        }

        private void EmpSumTxt_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            BaseViewModel.slider.MoveTo("EmployeeSummaryListEXP");
        }

        private void EmpSumTxt_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            BaseViewModel.slider.MoveTo("AccountsSummaryEXP");
        }

        private void TextBlock_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            BaseViewModel.slider.MoveTo("PayrollSetupSummaryEXP");
        }

        private void TextBlock_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            BaseViewModel.slider.MoveTo("PayrollEmployeeSetupDetailsBDR");
        }

        private void TextBlock_MouseLeftButtonDown_3(object sender, MouseButtonEventArgs e)
        {
            BaseViewModel.slider.MoveTo("AccountDetailsEXP");
        }

        private void TextBlock_MouseLeftButtonDown_4(object sender, MouseButtonEventArgs e)
        {
            BaseViewModel.slider.MoveTo("AccountsSummaryEXP"); 
        }

        private void CreateInstAcc(object sender, MouseButtonEventArgs e)
        {
            BaseViewModel.slider.MoveTo("AccountDetailsEXP"); 
        }

        private void CreatePayrollJob(object sender, MouseButtonEventArgs e)
        {
            BaseViewModel.slider.MoveTo("IntroEXP"); 
        }

        private void GeneratePayrollItems(object sender, MouseButtonEventArgs e)
        {
             BaseViewModel.Instance.GeneratePayrollItems();
        }

        private void ReviewPayroll(object sender, MouseButtonEventArgs e)
        {
            BaseViewModel.slider.MoveTo("EmployeeSummaryListEXP");  
        }

        private void PostPayroll(object sender, MouseButtonEventArgs e)
        {
            BaseViewModel.Instance.PostToAccounts();
        }

        private void ReviewEmpChecks(object sender, MouseButtonEventArgs e)
        {
            BaseViewModel.slider.MoveTo("EmployeePayStubEXP");   
        }

        private void ReviewInstPayments(object sender, MouseButtonEventArgs e)
        {
            BaseViewModel.slider.MoveTo("AccountsSummaryEXP");     
        }

        private void HomeExpander_Expanded_1(object sender, RoutedEventArgs e)
        {
            Expander_Expanded_1(sender, e);
            //homeexpand.IsExpanded = true;
        }

        private void HomeExpander_Collapsed(object sender, RoutedEventArgs e)
        {
            if (collapseHome == false)
            {
                ((Expander)sender).UpdateLayout();
                ((Expander)sender).IsExpanded = true;
               // collapseHome = true;
            }
            
        }

        private void MinimizeWindow(object sender, MouseButtonEventArgs e)
        {
            WindowState = System.Windows.WindowState.Minimized;
        }

        private void SwitchWindowState(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState == System.Windows.WindowState.Maximized)
            {
                WindowState = System.Windows.WindowState.Normal;
                return;
            }
            if (WindowState == System.Windows.WindowState.Normal)
            {
                WindowState = System.Windows.WindowState.Maximized;

            }

        }

       

     
	}
}