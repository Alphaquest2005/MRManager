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
	/// Interaction logic for PayrollItemDetails.xaml
	/// </summary>
	public partial class PayrollItemDetails : UserControl
	{
		public PayrollItemDetails()
		{
			this.InitializeComponent();
            im = (PayrollItemDetailsModel)this.FindResource("PayrollItemDetailsModelDataSource");
		
			// Insert code required on object creation below this point.
		}

       
        PayrollItemDetailsModel im;
        private void NewBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {  
           
            im.NewPayrollItem();
        }

        private void SaveBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            im.SavePayrollItem();
        }

        private void DeleteBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            im.DeletePayrollItem();
        }


	}
}