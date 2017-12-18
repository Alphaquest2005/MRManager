
using System;
using System.Collections.Generic;
using System.Data;
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
	/// Interaction logic for PayrollEmployeeSetupDetails.xaml
	/// </summary>
	public partial class PayrollEmployeeSetupDetails : UserControl
	{
		public PayrollEmployeeSetupDetails()
		{
			this.InitializeComponent();

            im = (PayrollEmployeeSetupDetailsModel)this.FindResource("PayrollEmployeeSetupDetailsModelDataSource");
   
         
            // Insert code required on object creation below this point.
        }

       


        PayrollEmployeeSetupDetailsModel im;

        //private void NewBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    im.NewItem();
        //}

        //private void SaveBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    im.SaveItem();
        //}

        private void PayrollEmpDG_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {

                DataLayer.PayrollEmployeeSetup r = (DataLayer.PayrollEmployeeSetup) e.Row.Item;
                r.EmployeeId = ((DataLayer.Employee) EmployeeCmb.SelectedItem)?.EmployeeId ?? BaseViewModel.Instance.CurrentEmployee.EmployeeId;
                im.SavePayrollEmployeeSetup();
               // im.SetEmployeeSetupBaseAmounts(r.EmployeeId);
           
        }

       
      
        private void xgrid_InitializingNewItem(object sender, InitializingNewItemEventArgs e)
        {
            ((DataLayer.PayrollEmployeeSetup)(e.NewItem)).EmployeeId = ((DataLayer.Employee)EmployeeCmb.SelectedItem).EmployeeId;
            ((DataLayer.PayrollEmployeeSetup)(e.NewItem)).StartDate = DateTime.Now;
        }

        private void xgrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            DataLayer.PayrollEmployeeSetup r = (DataLayer.PayrollEmployeeSetup)e.Row.Item;

           
                if (e.Column.Header.ToString() == "Payroll Item")
                {
                    ComboBox cb = (ComboBox) e.EditingElement;
                    DataLayer.PayrollSetupItem ps = (DataLayer.PayrollSetupItem) cb.SelectedItem;
                    if (ps == null)
                    {
                        e.Cancel = true;
                        return;
                    }
                    if (ps.Amount == null || ps.Amount == 0)
                    {
                        r.ChargeType = "Rate";
                        r.Rate = Convert.ToSingle(ps.Rate);
                        r.RateRounding = ps.RateRounding;
                        if (ps.CompanyLineItemDescription != null || ps.CompanyLineItemDescription == "")
                        {
                            r.CompanyRate = Convert.ToSingle(ps.CompanyRate);
                        }
                    }
                    else
                    {
                        r.ChargeType = "Amount";
                        r.Amount = ps.Amount;
                        if (ps.CompanyLineItemDescription != null || ps.CompanyLineItemDescription == "")
                        {
                            r.CompanyAmount = ps.CompanyAmount;

                        }
                    }


                }

                if (e.Column.Header.ToString() == "Amount")
                {
                    TextBox t = (TextBox) (e.EditingElement);
                    if (t.Text != "$0.00") r.ChargeType = "Amount";
                }
                if (e.Column.Header.ToString() == "Rate")
                {
                    TextBox t = (TextBox) (e.EditingElement);
                    if (t.Text != "$0.00") r.ChargeType = "Rate";
                }
          
                //im.SavePayrollEmployeeSetup();
        }



        private void DeleteBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var res = MessageBox.Show("Are you sure you want to delete this item?", "Delete Item", MessageBoxButton.YesNo);

            if (res == MessageBoxResult.Yes)
            {
                foreach (var item in xgrid.SelectedItems.OfType<DataLayer.PayrollEmployeeSetup>().ToList())
                {
                    im.DeletePayrollEmployeeSetup(item);
                }
                im.UpdateProperties();
            }
        }

       
       
	}
}