using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using System.ComponentModel;
using System.Linq;
using LinqLib.DynamicCodeGenerator;
using LinqLib.Sequence;
using System.Collections.ObjectModel;
using System.Data.Entity.SqlServer;
using System.Data.Objects;
using System.Windows.Controls;
using System.Windows;
using System.Threading.Tasks;
using PayrollManager.DataLayer;

namespace PayrollManager
{
	public class EmployeeBreakDownReportModel : BaseViewModel
	{
        private static object syncRoot = new Object();
		public EmployeeBreakDownReportModel()
		{
			staticPropertyChanged += EmployeeBreakDownReportModel_staticPropertyChanged;
		}

        private static bool isDirty = false;
        private void EmployeeBreakDownReportModel_staticPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
            if (e.PropertyName == "CurrentBranch" || e.PropertyName == "CurrentPayrollJob")//
            {
                isDirty = true;
                OnPropertyChanged("EmployeeBreakDown");
            }
        }


        DataGrid _myDataGrid = new DataGrid();
	   static private List<EmpSummary> _emplst = null;
	   static private dynamic _eb = null;

	    public DataGrid MyDataGrid
        {
            get
            {
                return _myDataGrid;
            }
            set
            {
                _myDataGrid = value;
                OnPropertyChanged("EmployeeBreakDown");
            }

        }

	    public object EmployeeBreakDown
	    {
	        get
	        {
	            try
	            {
	                lock (syncRoot)
	                {
	                    if (isDirty)
	                    {
	                        _emplst = GetEmpData();

	                        _eb = GetEmployeeBreakDown(_emplst);
	                        isDirty = false;
	                    }
	                }
	                return _eb;
	            }
	            catch (Exception)
	            {
	                throw;
	            }
	        }
	    }

	    private List<EmpSummary> GetEmpData()
	    {

	        
	            if (CurrentPayrollJob == null || CurrentBranch == null) return new List<EmpSummary>();
	            int cpjob = CurrentPayrollJob.PayrollJobId;
	            using (var ctx = new PayrollDB())
	            {
	                try
	                {
	                    var payrollItems = ctx.PayrollItems.Where(p => p.PayrollJobId == cpjob
	                                                                   && p.ParentPayrollItem == null).ToList();


                        var emplst = Employees
	                        .Select(e => new EmpSummary
	                        {
	                            Employee_Name = e.DisplayName,
	                            PayrollItems = payrollItems.Where(x => x.EmployeeId == e.EmployeeId)
                                            .Select(
	                                            x =>
	                                                new
	                                                {
	                                                    PayrollItem = x,
	                                                    Priority =
	                                                    x.PayrollSetupItem == null
	                                                        ? x.Priority
	                                                        : x.PayrollSetupItem.Priority
	                                                })
	                                        .GroupBy(
	                                            x =>
	                                                new
	                                                {
	                                                    Name = x.PayrollItem.Name.Trim(),
	                                                    x.PayrollItem.IncomeDeduction,
	                                                    x.Priority
	                                                })
	                                        .Select(g => new PayrollSummary
	                                        {
	                                            PayrollItemName =  g.Key.Name,//.ToString("D2")
                                                IncomeDeduction = g.Key.IncomeDeduction,
	                                            Priority = g.Key.Priority,
	                                            Total = g.Sum(x => x.PayrollItem.Amount)
	                                        }).OrderByDescending(x => x.IncomeDeduction)
	                                        .ThenBy(x => x.Priority)
                                            .ToList()
                                        ,
	                            Total = e.NetAmount
	                        })
	                        .ToList();



	                    return emplst;
	                }
	                catch (Exception e)
	                {
	                    Console.WriteLine(e);
	                    throw;
	                }
	            }
	      

	    }

	    public void PopulateGrid()
        {
           
            MyDataGrid.Columns.Clear();
            if (_eb != null)
            {
                MyDataGrid.Columns.Add(new System.Windows.Controls.DataGridTextColumn()
                {
                    Header = "Employee Name",
                    Binding = new Binding("Employee_Name")
                });

                var collst = _emplst.SelectMany(x => x.PayrollItems)
                    .OrderByDescending(x => x.IncomeDeduction)
                    .ThenByDescending(x => x.Priority)
                    .Select(x => x.PayrollItem).Distinct();

                var proplst = ((LinqLib.DynamicCodeGenerator.IDynamicPivotObject) _eb[0]).PropertiesNames.OrderBy(x => x);

                foreach (var item in collst.OrderBy(x => x))
                {
                    string t = item.Replace(" ", "_").Replace("-", "");
                    foreach (string p in proplst)
                    {
                        if (p.Trim('_').IndexOf(t) == 0)
                            //if (proplst.Contains(t))
                        {
                            ///var col = 
                            MyDataGrid.Columns.Add(new System.Windows.Controls.DataGridTextColumn()
                            {
                                Header = item.Substring(3),
                                Binding = new Binding(p) {StringFormat = "c"}
                            });
                            break;
                        }
                    }
                }
                MyDataGrid.Columns.Add(new System.Windows.Controls.DataGridTextColumn()
                {
                    Header = "Total",
                    Binding = new Binding("Total") {StringFormat = "c"},
                    FontWeight = FontWeights.Bold
                });
            }
        }

	    private dynamic GetEmployeeBreakDown(List<EmpSummary> emplst)
        {
           

                if (CurrentBranch != null && emplst.Any() && CurrentPayrollJob != null)//&& CurrentBranch.Employees.Any(e => e.PayrollItems.Any(p => p.PayrollJob == CurrentPayrollJob))
                {
                    try
                    {
                        //.GroupBy(x => x.PayrollItem).Select(x => new PayItmSummary{PayrollItem = x.Key, Total = x.Sum(y => y.Total)} )
                             _eb =
                                emplst.Pivot(E => E.PayrollItems.OrderByDescending(q => q.Priority).ThenBy(q => q.IncomeDeduction),
                                    E => E.PayrollItem, E => E.Total, true, null)
                                    .ToList();

                            if(_eb != null)
                            {
                                if (!(_eb[0] is IDynamicPivotObject)) return null;
                                var t = _eb[0].GetType();
                                var tot =
                                    (LinqLib.DynamicCodeGenerator.IDynamicPivotObject)
                                        Activator.CreateInstance(t);
                                tot.GetType().GetProperty("Employee_Name").SetValue(tot, "Total");
                                double totval = 0;
                                foreach (var item in tot.PropertiesNames.OrderBy(x => x))
                                {
                                    double val = 0;
                                    foreach (var row in _eb)
                                    {
                                        val += System.Convert.ToDouble(row.GetType().GetProperty(item).GetValue(row));
                                    }
                                    tot.GetType().GetProperty(item).SetValue(tot, val);

                                    totval += val;
                                }
                                tot.GetType().GetProperty("Total").SetValue(tot, CurrentPayrollJob.NetAmount);
                                //tot.GetType().GetProperty("Total").SetValue(tot, "Total");

                                _eb.Add(tot);

                            }

                            return _eb;

                        
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                return null;
           
        }

	    protected void TransformerClassGenerationEventHandler(object sender, ClassGenerationEventArgs e)
        {
            
        }

        public class EmpSummary
        {
            public string Employee_Name { get; set; }
            public List<PayrollSummary> PayrollItems { get; set; }
            public double Total { get; set; }
        }

        public class PayrollSummary
        {
            public string PayrollItem => Priority.ToString("D2") + "-" + PayrollItemName;
            public bool IncomeDeduction { get; set; }
            public int Priority { get; set; }
           // public ObservableCollection<DataLayer.PayrollItem> PayrollItems { get; set; }
            public double Total { get; set; }

            public string PayrollItemName { get; set; }
        }

        public class PayItmSummary
        {
            public string PayrollItem { get; set; }
           public double Total { get; set; }
        }


	}
}