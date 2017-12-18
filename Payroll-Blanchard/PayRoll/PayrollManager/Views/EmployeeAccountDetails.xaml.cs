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
	public partial class EmployeeAccountDetails : UserControl
	{
        public EmployeeAccountDetails()
		{
			this.InitializeComponent();
            im = (EmployeeAccountDetailsModel)this.FindResource("EmployeeAccountDetailsModelDataSource");
            // Insert code required on object creation below this point.
        }

        EmployeeAccountDetailsModel im;
        private void NewBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            im.NewEmployeeAccount();
        }

        private void SaveBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            im.SaveEmployeeAccount();
           
        }

        private void DeleteBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            im.DeleteEmployeeAccount();
        }

        private void EmployeeCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (((ComboBox)sender).SelectedItem != null)
            //AccountNameTxt.Text = ((DataLayer.Employee)((ComboBox)sender).SelectedItem).FirstName;
            if (e.AddedItems.Count > 0 && e.AddedItems[0] != EmployeeCmb.SelectedItem)
                AccountNameTxt.Text += " " + ((DataLayer.Employee)e.AddedItems[0]).FirstName;
        }

        private void InstitutionCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0 && e.AddedItems[0] != InstitutionCmb.SelectedItem )
                AccountNameTxt.Text += " " + ((DataLayer.Institution)e.AddedItems[0]).Name;
        }

        private void AccountTypeCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (((ComboBox)sender).SelectedItem != null)
            //    AccountNameTxt.Text += " " + ((DataLayer.AccountType)((ComboBox)sender).SelectedItem).AccountTypeName;
            if (e.AddedItems.Count > 0 && e.AddedItems[0] != AccountTypeCmb.SelectedItem)
                AccountNameTxt.Text += " " + ((DataLayer.AccountType)e.AddedItems[0]).AccountTypeName;

        }



      
	}
}