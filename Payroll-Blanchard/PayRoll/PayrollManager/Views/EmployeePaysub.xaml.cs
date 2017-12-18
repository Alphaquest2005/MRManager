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
	/// Interaction logic for Paysub.xaml
	/// </summary>
	public partial class EmployeePaysub : UserControl
	{
		public EmployeePaysub()
		{
			this.InitializeComponent();
		   im = (EmployeePayStubModel)this.FindResource("EmployeePayStubModelDataSource");
		   
			// Insert code required on object creation below this point.
		}
		EmployeePayStubModel im;
		private void PrintReport(object sender, MouseButtonEventArgs e)
		{
			FrameworkElement rpt = (FrameworkElement)DailyReportGD;
			PrintClass.Print(ref rpt);
		}

		private void EmailReport(object sender, MouseButtonEventArgs e)
		{
			//FrameworkElement rpt = (FrameworkElement);
			im.EmailReport(ref DailyReportGD);
			MessageBox.Show("Email Created. Please Check Drafts to review then send");
		}

		private void IncomeDataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
		{
			//var lrow = im.IncomePayrollLineItems.LastOrDefault();
			if ((e.Row.Item as EmployeePayStubModel.PayrollSummaryLineItem).LineItemDescription == "Total")
			{
			   // e.Row.Background = new SolidColorBrush(Colors.DodgerBlue);
			   // e.Row.FontSize = 14;
				e.Row.FontWeight = FontWeights.Bold;
			   
			}
		}

		private void EmailAllReport(object sender, MouseButtonEventArgs e)
		{

			using (var ctx = new PayrollDB())
			{
				var emplst = BaseViewModel.Instance.Employees;
				foreach (var employee in emplst)
				{
					im.CurrentEmployee = employee;
					DailyReportGD.UpdateLayout();
					im.EmailReport(ref DailyReportGD);
				}
				MessageBox.Show("Email Created. Please Check Drafts to review then send");
			}

		}



	}
}