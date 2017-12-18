using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using LinqLib.DynamicCodeGenerator;
using LinqLib.Sequence;
using System.Windows.Controls;
using System.Windows;
using PayrollManager.DataLayer;

namespace PayrollManager
{
	public class BranchPayrollItemBreakDownModel : BaseViewModel
	{
        private static object syncRoot = new Object();
		public BranchPayrollItemBreakDownModel()
		{
			staticPropertyChanged +=BranchPayrollItemBreakDownModel_staticPropertyChanged;
            ReportDate = DateTime.Now;
		}

	    private static bool isDirty = false;
        private void BranchPayrollItemBreakDownModel_staticPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
            if (e.PropertyName == "CurrentPayrollJob")
            {
                isDirty = true;
                OnPropertyChanged("BranchData");
            }

        }

        DataGrid _myDataGrid = new DataGrid();
        public DataGrid MyDataGrid
        {
            get
            {
                return _myDataGrid;
            }
            set
            {
                _myDataGrid = value;
                OnPropertyChanged("BranchData");
            }

        }

       static DateTime _reportDate = DateTime.Now;
	   	    private List<object> _eb;

	    public DateTime ReportDate
        {
            get { return _reportDate; }
            set
            {
                _reportDate = value;
                isDirty = true;
                OnPropertyChanged("ReportDate");
                OnPropertyChanged("BranchData");
            }
        }

        public object BranchData
        {
            get 
            {
                lock (syncRoot)
                {
                    if (isDirty)
                    {
                        var _plist = GetBranchData();
                        CalcBranchData(_plist);
                        SetBranchTotals();
                        isDirty = false;
                    }
                }
                return _eb;
            }
        }

      

        private List<BranchPayrollItemSummaryLine> GetBranchData()
	    {
            try
            {

               
           
                using (var ctx = new PayrollDB())
                {
                    var startDate = DateTime.Parse(string.Format("{0}/1/{1}", ReportDate.Month, ReportDate.Year));
                    var endDate =
                        DateTime.Parse(string.Format("{0}/1/{1}", ReportDate.AddMonths(1).Month,
                            ReportDate.AddMonths(1).Year));
                    var plst = (from p in ctx.PayrollItems//.Where(x => x.PayrollJobId == CurrentPayrollJob.PayrollJobId)
                        .Where(
                            pi =>
                                pi.PayrollJob.EndDate.Month == ReportDate.Month &&
                                pi.PayrollJob.EndDate.Year == ReportDate.Year)
                        .Include(x => x.PayrollJob.Branch)
                        
                        .OrderByDescending(x => x.IncomeDeduction)
                        .ThenBy(x => x.PayrollSetupItem == null ? x.Priority : x.PayrollSetupItem.Priority)
                        group p by new {p.Name}
                        into g 
                        select new BranchPayrollItemSummaryLine
                        {
                            Payroll_Item = g.Key.Name,
                            Total = g.Sum(p => p.Amount),
                            PayrollItems =
                                g.OrderByDescending(x => x.IncomeDeduction)
                                    .ThenBy(x => x.PayrollSetupItem == null ? x.Priority : x.PayrollSetupItem.Priority)
                                    
                        }).ToList();
                    if (!plst.Any()) return null;
                    plst.ForEach(x => x.PayrollItems.ToList().ForEach(z =>
                    {
                        z.PayrollJobReference.Load();
                        z.PayrollJob.BranchReference.Load();
                    }));
                    return plst;
                    
                }
      
            }
            catch (Exception)
            {

                throw;
            }
	    }

	    public void PopulateGrid()
	    {
	        try
	        {

	        MyDataGrid.Columns.Clear();
	        if (_eb != null)
	        {
	            var sstyle = new Style(typeof (TextBlock));
	            sstyle.Setters.Add(new Setter(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Right));
	            MyDataGrid.Columns.Add(new System.Windows.Controls.DataGridTextColumn()
	            {
	                Header = "Payroll Item",
	                Binding = new Binding("Payroll_Item")
	            });


	            foreach (var item in ((LinqLib.DynamicCodeGenerator.IDynamicPivotObject) _eb[0]).PropertiesNames
	                )
	            {
	                MyDataGrid.Columns.Add(new System.Windows.Controls.DataGridTextColumn()
	                {
	                    Header = item.Replace("_", " "),
	                    Binding = new Binding(item) {StringFormat = "c"},
	                    ElementStyle = sstyle
	                });
	            }
	            MyDataGrid.Columns.Add(new System.Windows.Controls.DataGridTextColumn()
	            {
	                Header = "Total",
	                Binding = new Binding("Total") {StringFormat = "c"},
	                FontWeight = FontWeights.Bold,
	                ElementStyle = sstyle
	            });
	        }
            }
            catch (Exception)
            {

                throw;
            }
	    }

	    private void SetBranchTotals()
	    {
	        try
	        {


	            if (_eb != null)
	            {
                    if (!(_eb[0] is IDynamicPivotObject)) return;
	                var tot =
	                    (LinqLib.DynamicCodeGenerator.IDynamicPivotObject) Activator.CreateInstance(_eb[0].GetType());
	                tot.GetType().GetProperty("Payroll_Item").SetValue(tot, "Total");
	                //    tot.GetType().GetProperty("Total").SetValue(tot, DBNull.Value);

	                foreach (var item in tot.PropertiesNames)
	                {
	                    double val = 0;
	                    foreach (var row in _eb)
	                    {
	                        val += System.Convert.ToDouble(row.GetType().GetProperty(item).GetValue(row));
	                    }
	                    tot.GetType().GetProperty(item).SetValue(tot, val);
	                }


	                _eb.Add(tot);
	            }
	        }
	        catch (Exception)
	        {

	            throw;
	        }
	    }

	    private void CalcBranchData(List<BranchPayrollItemSummaryLine> plist)
	    {
	        try
	        {
                if (plist == null) return;
	            _eb = plist.Pivot(
	                X => X.PayrollItems.GroupBy(p => p.PayrollJob.Branch.Name)
	                        .Select(g => new BranchSummary {BranchName = g.Key, Total = g.Sum(p => p.Amount)}),
	                X => X.BranchName, X => X.Total, true, null).ToList();
	        }
	        catch (Exception)
	        {

	            throw;
	        }
	    }

	    private void TransformerClassGenerationEventHandler(object sender, ClassGenerationEventArgs e)
        {
            //	throw new NotImplementedException();
        }
        

    
        public class BranchSummary
        {
            public string BranchName { get; set; }
            public ObservableCollection<DataLayer.PayrollItem> PayrollItems {get;set;}
            public double Total { get; set; }
        }

        public class BranchPayrollItemSummaryLine
        {
            public string Payroll_Item { get; set; }
            //public string BranchName { get; set; }
            //public int Priority { get; set; }
            //public bool IncomeDeduction { get; set; }
            public double Total { get; set; }
            public IOrderedEnumerable<PayrollItem> PayrollItems { get; set; }
        }

		
	}
}