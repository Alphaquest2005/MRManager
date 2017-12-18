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
	/// Interaction logic for PayrollSetupItems.xaml
	/// </summary>
	public partial class PayrollSetupItems : UserControl
	{
		public PayrollSetupItems()
		{
			this.InitializeComponent();

            im = (PayrollSetupItemsModel)this.FindResource("PayrollSetupItemsModelDataSource");

            // Insert code required on object creation below this point.
        }


        PayrollSetupItemsModel im;
        private void NewBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            im.NewPayrollSetupItem();
        }

        private void SaveBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            im.SavePayrollSetupItem();
        }

        private void DeleteBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            im.DeletePayrollSetupItem();
        }

     
	}
}