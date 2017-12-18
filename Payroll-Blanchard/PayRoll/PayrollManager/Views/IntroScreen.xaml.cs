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

namespace PayrollManager.Views
{
	/// <summary>
	/// Interaction logic for IntroScreen.xaml
	/// </summary>
	public partial class IntroScreen : UserControl
	{
		public IntroScreen()
		{
			this.InitializeComponent();
			
			// Insert code required on object creation below this point.
		}

        private void StartJobBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BaseViewModel.slider.MoveTo("PayrollJobBDR");
        }

        private void ConfigPayItemsBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BaseViewModel.slider.MoveTo("PayrollEmployeeSetupDetailsBDR");
        }

        private void PayrollBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BaseViewModel.slider.MoveTo("PayrollItemDetailsEXP");
        }

        private void GenPayrollBtn_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
             
           BaseViewModel.Instance.GeneratePayrollItems();
        }

        private void PostPayrollBtn_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            BaseViewModel.Instance.PostToAccounts();
        }

	    private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
	    {
	        MessageBox.Show($"Payroll Job Selected - {BaseViewModel.Instance.CurrentPayrollJob.Name}");
	    }
	}
}