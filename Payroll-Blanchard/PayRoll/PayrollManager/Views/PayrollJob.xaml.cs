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
	/// Interaction logic for PayrollJob.xaml
	/// </summary>
	public partial class PayrollJob : UserControl
	{
		public PayrollJob()
		{
			this.InitializeComponent();
             im = (PayrollJobModel)this.FindResource("PayrollJobModelDataSource");
			// Insert code required on object creation below this point.
		}
        PayrollJobModel im;
        //private void CreatePayrollBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
         
        //    im.CreatePayrollJob();
        //}

        private void SaveBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            im.SavePayrollJob();
        }

        private void DeleteBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            im.DeletePayrollJob();
        }

        private void NewBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            im.NewPayrollJob();
        }

       
	}
}