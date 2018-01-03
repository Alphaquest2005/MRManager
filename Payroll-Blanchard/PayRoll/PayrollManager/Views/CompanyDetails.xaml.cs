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
	/// Interaction logic for Company.xaml
	/// </summary>
	public partial class CompanyDetails : UserControl
	{
		public CompanyDetails()
		{
			this.InitializeComponent();
             im = (CompanyModel)this.FindResource("CompanyModelDataSource");
			// Insert code required on object creation below this point.
		}
        CompanyModel im;
        //private void CreatePayrollBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
         
        //    im.CreateCompany();
        //}

        private void SaveBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            im.SaveCompany();
        }

        private void DeleteBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            im.DeleteCompany();
        }

        private void NewBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            im.NewCompany();
        }

       
	}
}