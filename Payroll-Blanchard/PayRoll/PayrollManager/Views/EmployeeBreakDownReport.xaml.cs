using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PayrollManager.Converters;

namespace PayrollManager
{
	/// <summary>
	/// Interaction logic for EmployeeBreakDownReport.xaml
	/// </summary>
	public partial class EmployeeBreakDownReport : UserControl
	{
		public EmployeeBreakDownReport()
		{
			this.InitializeComponent();
            im = (EmployeeBreakDownReportModel)this.FindResource("EmployeeBreakDownReportModelDataSource");
            im.MyDataGrid = this.GridData;
			// Insert code required on object creation below this point.
            var dpd = DependencyPropertyDescriptor.FromProperty(ItemsControl.ItemsSourceProperty, typeof(DataGrid));
            if (dpd != null)
            {
                dpd.AddValueChanged(GridData, ItemSourceChanged);
            }
		}

        private void ItemSourceChanged(object sender, EventArgs e)
        {
            im.PopulateGrid();
        }
        EmployeeBreakDownReportModel im;

        private void PrintReport(object sender, MouseButtonEventArgs e)
        {
            //FrameworkElement rpt = (FrameworkElement)DailyReportGD;
            //PrintClass.Print(ref rpt);
            // WPF2PDF.CreateAndOpenPDF(ref DailyReportGD, "EmployeeBreakDownReport");
            FrameworkElement rpt = (FrameworkElement)DailyReportGD;
            PrintClass.Print(ref rpt);
        }

        private void GridData_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            
            if (e.Row.IsNewItem == false)
            {
                if ((e.Row.Item as EmployeeBreakDownReportModel.EmpSummary).Employee_Name == "Total")
                {
                    // e.Row.Background = new SolidColorBrush(Colors.DodgerBlue);
                    // e.Row.FontSize = 14;
                    e.Row.FontWeight = FontWeights.Bold;

                }
            }
        }

        private void LoadReport(object sender, MouseButtonEventArgs e)
        {
            im.PopulateGrid();
        }

        private void ExcelReport(object sender, MouseButtonEventArgs e)
        {
            var p = new ExportToExcel();
            p.GenerateReport(GridData);
        }

	}
}