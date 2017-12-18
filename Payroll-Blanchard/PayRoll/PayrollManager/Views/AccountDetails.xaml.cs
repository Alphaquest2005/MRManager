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
	/// Interaction logic for AccountDetails.xaml
	/// </summary>
	public partial class AccountDetails : UserControl
	{
		public AccountDetails()
		{
			this.InitializeComponent();
            im = (AccountDetailsModel)this.FindResource("AccountDetailsModelDataSource");
            // Insert code required on object creation below this point.
        }

        AccountDetailsModel im;
        private void NewBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            im.NewInstitutionAccount();
        }

        private void SaveBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
          
                im.SaveInstitutionAccount();
            
        }

        private void DeleteBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            im.DeleteInstitionAccount();
        }

     
	}
}