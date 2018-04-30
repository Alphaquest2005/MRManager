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
using System.Linq;
    using PayrollManager.DataLayer;

namespace PayrollManager
{
	/// <summary>
	/// Interaction logic for EmployeeDetails.xaml
	/// </summary>
	public partial class EmployeeDetails : UserControl
	{
		public EmployeeDetails()
		{
			this.InitializeComponent();
            im = (EmployeeDetailsModel)this.FindResource("EmployeeDetailsModelDataSource");
			// Insert code required on object creation below this point.
		}

        EmployeeDetailsModel im; 
        private void NewBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            im.NewEmployee();
        }

        private void SaveBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            im.SaveEmployee();
        }

        private void DeleteBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            im.DeleteEmployee();
        }

        private void delbtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            im.DeleteEmployeeAccount(((FrameworkElement)sender).DataContext as DataLayer.EmployeeAccount);
        }

        private void Editbtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            im.EditAccount(((DataLayer.EmployeeAccount)((FrameworkElement)sender).DataContext).Account);
        }







	    private void DataGrid_RowEditEnding_1(object sender, DataGridRowEditEndingEventArgs e)
	    {
	        if (e.Row.IsNewItem == true && im.CurrentEmployee != null)
	        {
	            if (im.CurrentEmployee == null) return;
                var emp = im.CurrentEmployee;
                im.SaveEmployee();
	            using (var ctx = new PayrollDB(Properties.Settings.Default.PayrollDB))
	            {

	                DataLayer.EmployeeAccount ne = (DataLayer.EmployeeAccount) e.Row.Item;
	                
	                var inst = ctx.Institutions
	                    .FirstOrDefault(i => i.InstitutionId == ne.Account.InstitutionId);
	                if (inst != null)
	                {
	                    ne.EmployeeId = emp.EmployeeId;
	                    ne.Account.AccountName = emp.FirstName + " " +
	                                     inst.ShortName +
	                                     " Salary Account";
	                    ne.Account.AccountTypeId = ctx.AccountTypes.First(x => x.Name == "Salary").AccountTypeId;
	                    if (ne.AccountId == 0)
	                    {
	                        ctx.EmployeeAccounts.AddObject(ne);
	                    }
	                    else
	                    {
	                        var ritm = ctx.EmployeeAccounts.First(x => x.AccountId == ne.AccountId);
	                    ctx.EmployeeAccounts.Attach(ritm);
	                    ctx.EmployeeAccounts.ApplyCurrentValues(ne);
	                    }
	                    
	                    BaseViewModel.SaveDatabase(ctx);
	                }

	                im.LoadEmployees();

	            }
	        }
	        e.Cancel = true;
	        
        }

        private void GenerateItms_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            im.EmployeeAutoSetup();
        }




	    private void RowBeginEdit(object sender, DataGridBeginningEditEventArgs e)
	    {
	        DataLayer.EmployeeAccount ne = (DataLayer.EmployeeAccount)e.Row.Item;
	        if(ne?.AccountId == 0 && ne.Account == null) ne.Account = new Account();
        }
	}
}