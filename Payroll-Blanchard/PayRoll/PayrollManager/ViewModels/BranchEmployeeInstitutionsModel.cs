using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Data;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Data.Entity;
using System.Dynamic;
using LinqLib.DynamicCodeGenerator;
using LinqLib.Sequence;
using System.Windows.Controls;
using System.Windows;
using System.Threading.Tasks;
using PayrollManager.DataLayer;

namespace PayrollManager
{
    public class BranchEmployeeInstitutionsModel : BaseViewModel
    {
        private static object syncRoot = new Object();
        public BranchEmployeeInstitutionsModel()
        {
            staticPropertyChanged += BranchEmployeeInstitutionsModel_staticPropertyChanged;
            ReportDate = DateTime.Now;
        }

        private static bool isDirty = false;
        private static List<EmployeeSummaryLine> _employeeDeductionsData = null;
        private static List<EmployeeAccountSummaryLine> _employeeSalaryData = null;
        private static List<dynamic> _deductionsData = null;

        private void BranchEmployeeInstitutionsModel_staticPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
            if (e.PropertyName == "CurrentPayrollJob" || e.PropertyName == "CurrentBranch")
            {
                isDirty = true;
                allbranch = false;
                OnPropertyChanged("DeductionsData");
                OnPropertyChanged("NetSalaryData");
            }
        }

        private DataGrid _deductionsGrid = new DataGrid();

        public DataGrid DeductionsGrid
        {
            get { return _deductionsGrid; }
            set
            {
                _deductionsGrid = value;
                OnPropertyChanged("DeductionsData");
            }

        }

        private DataGrid _netSalaryGrid = new DataGrid();

        public DataGrid NetSalaryGrid
        {
            get { return _netSalaryGrid; }
            set
            {
                _netSalaryGrid = value;
                OnPropertyChanged("NetSalaryData");
            }

        }

        private DataGrid _grandTotalGrid = new DataGrid();

        public DataGrid GrandTotalGrid
        {
            get { return _grandTotalGrid; }
            set
            {
                _grandTotalGrid = value;
                OnPropertyChanged("GrandTotalData");
            }

        }

        private DateTime _reportDate = DateTime.Now;

        public DateTime ReportDate
        {
            get { return _reportDate; }
            set
            {
                _reportDate = value;
                allbranch = true;
                OnPropertyChanged("ReportDate");
                OnPropertyChanged("DeductionsData");
                OnPropertyChanged("NetSalaryData");
                OnPropertyChanged("GrandTotalData");
            }
        }

        private static bool allbranch = false;
        private List<object> _netSalaryData;

        public object DeductionsData
        {
            get
            {
                lock (syncRoot)
                {
                    if (isDirty)
                    {

                        _employeeDeductionsData = GetEmployeeDeductionsData().Result;
                        _deductionsData = CalculateDeductions();
                        SetDeductionTotals();
                    }
                }
                return _deductionsData;
            }
        }

        private dynamic CalculateDeductions()
        {
            try
            {
                if (_employeeDeductionsData == null || !_employeeDeductionsData.Any()) return null;
                var eb =
                    _employeeDeductionsData.Pivot(
                        X => X.PayrollItems.GroupBy(p => new { p.CreditAccount.Institution.ShortName, p.CreditAccount.Institution.Priority })
                            .Select(g => new InstitutionSummary { Institution = $"{g.Key.Priority ?? "99"}-{g.Key.ShortName}", Total = g.Sum(p => p.Amount), Priority = g.Key.Priority }).OrderBy(x => x.Priority),
                        X => X.Institution,
                        X => X.Total, true, null).ToList();
                return eb;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<List<EmployeeSummaryLine>> GetEmployeeDeductionsData()
        {
            try
            {

                var t = Task.Run(() =>
                {
                    if (CurrentPayrollJob == null) return new List<EmployeeSummaryLine>();
                    using (var ctx = new PayrollDB())
                    {

                        var plist = ctx.PayrollItems
                            //use this to get all jobs over branches
                            .Where(x => x.PayrollJob.StartDate == CurrentPayrollJob.StartDate && x.PayrollJob.EndDate == CurrentPayrollJob.EndDate && x.PayrollJob.PayrollJobTypeId == CurrentPayrollJob.PayrollJobTypeId)
                            .Include("CreditAccount.Institution")
                            .Include(x => x.Employee)
                            .Where(pi => pi.DebitAccount is DataLayer.EmployeeAccount &&
                                         "Salary Deduction,Communal Birthday Club".ToUpper()
                                             .Contains(pi.Name.Trim().ToUpper()))
                            .OrderBy(x => x.Employee.LastName)
                            .GroupBy(p => new {DisplayName = p.Employee.FirstName + " " + p.Employee.LastName})
                            .Select(g => new
                            {
                                Employee = g.Key.DisplayName,
                                Total = g.Sum(p => p.Amount),
                                PayrollItems = g
                            })
                            .ToList()
                            .Select(g => new EmployeeSummaryLine
                            {
                                Employee = g.Employee,
                                Total = g.Total,
                                PayrollItems = g.PayrollItems.ToList()
                            }).ToList();
                        if (!plist.Any()) return new List<EmployeeSummaryLine>();

                        plist.ForEach(x =>
                        {
                            x.PayrollItems.ForEach(z =>
                            {
                                z.CreditAccountReference.Load();
                                z.CreditAccount.InstitutionReference.Load();
                            });
                        });
                        return plist;
                    }
                }).ConfigureAwait(false);

                return await t;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async void SetDeductionTotals()
        {


            // var eb = db.PayrollItems.AsEnumerable().GroupBy(b => new BranchSummary { BranchName = b.Name, PayrollItems = new ObservableCollection<DataLayer.PayrollItem>(( p => p.PayrollItems)), Total = b.Sum(p => p.NetAmount) }).AsEnumerable().Pivot(E => E.PayrollItems, E => E.PayrollJob.Branch.Name, E => E.Amount, true, TransformerClassGenerationEventHandler).ToList();
            try
            {

                if (_deductionsData != null)
                {
                    if (!(_deductionsData[0] is IDynamicPivotObject)) return;
                    var t = _deductionsData[0].GetType();
                    var tot =
                        (LinqLib.DynamicCodeGenerator.IDynamicPivotObject)
                            Activator.CreateInstance(t);
                    tot.GetType().GetProperty("Employee").SetValue(tot, "Total");
                    //    tot.GetType().GetProperty("Total").SetValue(tot, DBNull.Value);

                    foreach (var item in tot.PropertiesNames)
                    {
                        double val = 0;
                        foreach (var row in _deductionsData)
                        {
                            val += System.Convert.ToDouble(row.GetType().GetProperty(item).GetValue(row));
                        }
                        tot.GetType().GetProperty(item).SetValue(tot, val);
                    }
                    tot.GetType()
                        .GetProperty("Total")
                        .SetValue(tot, _employeeDeductionsData.Sum(x => x.PayrollItems.Sum(z => z.Amount)));

                    _deductionsData.Add(tot);
                }

                //  return _deductionsData;


            }
            catch (Exception)
            {

                throw;
            }

        }

        public void PopulateDeductionsGrid()
        {
            try
            {

                DeductionsGrid.Columns.Clear();
                if (_deductionsData != null)
                {
                    var sstyle = new Style(typeof(TextBlock));
                    sstyle.Setters.Add(new Setter(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Right));
                    DeductionsGrid.Columns.Add(new System.Windows.Controls.DataGridTextColumn()
                    {
                        Header = "Employee",
                        Binding = new Binding("Employee")
                    });
                    foreach (var item in ((IDynamicPivotObject)_deductionsData[0]).PropertiesNames.OrderBy(x => x))
                    {
                        DeductionsGrid.Columns.Add(new DataGridTextColumn()
                        {
                            Header = item.Substring(item.IndexOf("_") + 3).Replace("_", " "),
                            Binding = new Binding(item) { StringFormat = "c" },
                            ElementStyle = sstyle
                        });
                    }


                    DeductionsGrid.Columns.Add(new DataGridTextColumn()
                    {
                        Header = "Total",
                        Binding = new Binding("Total") { StringFormat = "c" },
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

        public object NetSalaryData
        {
            get
            {
                lock (syncRoot)
                {
                    if (isDirty)
                    {
                        _employeeSalaryData = GetNetSalaryData().Result;
                        _netSalaryData = CalculateNetSalaryData(_employeeSalaryData);
                        SetNetSalaryTotals();
                        isDirty = false;
                    }
                }
                return _netSalaryData;
            }

        }

        private List<object> _grandTotalData = new List<object>();
        public object GrandTotalData
        {
            get
            {
                lock (syncRoot)
                {
                    if (NetSalaryData != null && DeductionsData != null)
                    {
                        _grandTotalData.Clear();
                        dynamic gt = new ExpandoObject();
                        gt.GrandTotal = "Total";

                        var nsRow = ((List<dynamic>)NetSalaryData).Last();
                        var drow = ((List<dynamic>)DeductionsData).Last();

                        var cols = new List<string>();
                        foreach (var col in nsRow.PropertiesNames)
                        {
                            if (!cols.Contains(col)) cols.Add(col);
                        }

                        foreach (var col in drow.PropertiesNames)
                        {
                            if (!cols.Contains(col)) cols.Add(col);
                        }

                        foreach (var col in cols)
                        {

                            var ns = (double?)nsRow.GetType().GetProperty(col)?.GetValue(nsRow);
                            var dd = (double?)drow.GetType().GetProperty(col)?.GetValue(drow);
                            ((IDictionary<string, object>)gt).Add(col.Substring(3), (ns.GetValueOrDefault() + dd.GetValueOrDefault()).ToString("c"));

                        }

                        _grandTotalData.Add(gt);

                        

                    }
                    return _grandTotalData;
                }
            }

        }

        private async Task<List<EmployeeAccountSummaryLine>> GetNetSalaryData()
        {
            if (CurrentPayrollJob == null) return new List<EmployeeAccountSummaryLine>();
            var t = Task.Run(() =>
            {
                try
                {
                    using (var ctx = new PayrollDB())
                    {
                        var employeeSalaryData =
                        ctx.PayrollItems.Where(x => x.PayrollJob.StartDate == CurrentPayrollJob.StartDate
                                                               && x.PayrollJob.EndDate == CurrentPayrollJob.EndDate
                                                               && x.PayrollJob.PayrollJobTypeId == CurrentPayrollJob
                                                                   .PayrollJobTypeId)
                                .Include(x => x.CreditAccount.Institution)
                                .Include(x => x.CreditAccount.AccountEntries)
                                .Include(x => x.PayrollJob.PayrollJobType)
                                .Include(x => x.Employee)
                                //use this to get all jobs over branches
                                .Where(pi => (pi.PayrollJob.StartDate == CurrentPayrollJob.StartDate && pi.PayrollJob.EndDate == CurrentPayrollJob.EndDate && pi.PayrollJob.PayrollJobTypeId == CurrentPayrollJob.PayrollJobTypeId)
                                             && pi.CreditAccount is DataLayer.EmployeeAccount
                                             && pi.Name.Trim().ToUpper() == "Salary".ToUpper()
                                )
                                .OrderBy(x => x.Employee.LastName)
                            .Select(p => new { DisplayName = p.Employee.FirstName + " " + p.Employee.LastName,
                                                             p.CreditAccount,
                                               Total = (double?)p.CreditAccount.AccountEntries.Where(z => z.PayrollItem.PayrollJob.StartDate == CurrentPayrollJob.StartDate && z.PayrollItem.PayrollJob.EndDate == CurrentPayrollJob.EndDate && z.PayrollItem.PayrollJob.PayrollJobTypeId == CurrentPayrollJob.PayrollJobTypeId).Sum(q => q.CreditAmount - q.DebitAmount)
                                              })
                            .Select(g => new EmployeeAccountSummaryLine
                            {
                                Employee = g.DisplayName,
                                Account = g.CreditAccount,
                                Total = g.Total ?? 0
                            })
                            .ToList();

                        if (!employeeSalaryData.Any()) return new List<EmployeeAccountSummaryLine>();
                        employeeSalaryData.ForEach(x => x.Account.InstitutionReference.Load());
                        return employeeSalaryData;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

            }).ConfigureAwait(false);
            return await t;
        }

        private void SetNetSalaryTotals()
        {
            try
            {

                if (_netSalaryData != null)
                {
                    if (!(_netSalaryData[0] is IDynamicPivotObject)) return;
                    var tot =
                        (LinqLib.DynamicCodeGenerator.IDynamicPivotObject)
                            Activator.CreateInstance(_netSalaryData[0].GetType());
                    tot.GetType().GetProperty("Employee").SetValue(tot, "Total");
                    //    tot.GetType().GetProperty("Total").SetValue(tot, DBNull.Value);

                    foreach (var item in tot.PropertiesNames)
                    {
                        double val =
                            _netSalaryData.Sum(
                                row => System.Convert.ToDouble(row.GetType().GetProperty(item).GetValue(row)));
                        tot.GetType().GetProperty(item).SetValue(tot, val);
                    }
                    tot.GetType().GetProperty("Total").SetValue(tot, _employeeSalaryData.Sum(x => x.Total));

                    _netSalaryData.Add(tot);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private static List<object> CalculateNetSalaryData(IEnumerable<EmployeeAccountSummaryLine> plist)
        {
            try
            {
                if (plist == null || !plist.Any()) return null;
                var eb =
                    plist.Pivot(
                        X =>
                            plist.Select(
                                x =>
                                    new InstitutionSummary
                                    {
                                        Institution = $"{X.Account.Institution.Priority ?? "99"}-{X.Account.Institution.ShortName}",
                                        Total = X.Total,
                                        Priority = x.Account.Institution.Priority
                                    }).OrderBy(x => x.Priority),
                        X => X.Institution, X => X.Total, true, null).ToList();
                return eb;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public void PopulateNetSalaryGrid()
        {
            try
            {

                NetSalaryGrid.Columns.Clear();
                if (_netSalaryData != null)
                {
                    var sstyle = new Style(typeof(TextBlock));
                    sstyle.Setters.Add(new Setter(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Right));
                    NetSalaryGrid.Columns.Add(new System.Windows.Controls.DataGridTextColumn()
                    {
                        Header = "Employee",
                        Binding = new Binding("Employee")
                    });
                    foreach (
                        var item in
                            ((LinqLib.DynamicCodeGenerator.IDynamicPivotObject)_netSalaryData[0]).PropertiesNames.OrderBy(x => x))
                    {
                        NetSalaryGrid.Columns.Add(new System.Windows.Controls.DataGridTextColumn()
                        {
                            Header = item.Substring(item.IndexOf("_") + 3).Replace("_", " "),
                            Binding = new Binding(item) { StringFormat = "c" },
                            ElementStyle = sstyle
                        });
                    }
                    NetSalaryGrid.Columns.Add(new System.Windows.Controls.DataGridTextColumn()
                    {
                        Header = "Total",
                        Binding = new Binding("Total") { StringFormat = "c" },
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

        public void PopulateGrandTotalGrid()
        {
            try
            {

                GrandTotalGrid.Columns.Clear();
                var gdata = GrandTotalData;
                if (gdata != null && ((List<object>)gdata).Any())
                {
                    var sstyle = new Style(typeof(TextBlock));
                    sstyle.Setters.Add(new Setter(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Right));

                    foreach (
                        var item in ((IDictionary<string, object>)((List<Object>)gdata).First()).Keys)
                    {
                        GrandTotalGrid.Columns.Add(new System.Windows.Controls.DataGridTextColumn()
                        {
                            Header = item.Replace("_", " "),
                            Binding = new Binding(item) { StringFormat = "c" },
                            ElementStyle = sstyle
                        });
                    }

                }
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



        public class InstitutionSummary
        {
            public string Institution { get; set; }
            public ObservableCollection<DataLayer.PayrollItem> PayrollItems { get; set; }
            public double Total { get; set; }
            public string Priority { get; set; }
        }

        public class InstitutionAccountSummary
        {
            public string Institution { get; set; }
            public ObservableCollection<DataLayer.AccountEntry> AccountEntries { get; set; }
            public double Total { get; set; }
        }

        public class EmployeeSummaryLine
        {
            public string Employee { get; set; }
            public double Total { get; set; }
            public List<DataLayer.PayrollItem> PayrollItems { get; set; }
        }

        public class EmployeeAccountSummaryLine
        {
            public string Employee { get; set; }
            public double Total { get; set; }
            public DataLayer.Account Account { get; set; }
        }


    }

}
