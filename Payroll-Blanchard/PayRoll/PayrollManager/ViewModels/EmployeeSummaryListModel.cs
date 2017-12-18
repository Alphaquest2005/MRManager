using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Data.Objects;
using System.Linq;
using PayrollManager.DataLayer;

namespace PayrollManager
{
	public class EmployeeSummaryListModel : BaseViewModel
	{
		public EmployeeSummaryListModel()
		{
			staticPropertyChanged +=EmployeeSummaryListModel_staticPropertyChanged;
		}

		private void EmployeeSummaryListModel_staticPropertyChanged(object sender, PropertyChangedEventArgs e)
		{

			if (e.PropertyName == "CurrentBranch" && CurrentBranch != null || e.PropertyName == "Employees")
			{

				if (CurrentBranch == null)
				{
					Employees = new ObservableCollection<DataLayer.Employee>(base.Employees
						.Where(x => x.EmploymentEndDate.HasValue == false ||
									EntityFunctions.TruncateTime(x.EmploymentEndDate.Value) >= EntityFunctions.TruncateTime(DateTime.Now))
						.OrderBy(x => x.LastName).ToList());
				}
				else
				{
					if (EmployeeFilter == null) EmployeeFilter = "";
					Employees = new ObservableCollection<DataLayer.Employee>(base.Employees
						.Where(x => x.EmploymentEndDate.HasValue == false ||
									EntityFunctions.TruncateTime(x.EmploymentEndDate.Value) >= EntityFunctions.TruncateTime(DateTime.Now))
						.Where(emp => emp.BranchId == CurrentBranch.BranchId &&
									  emp.DisplayName.ToUpper().Contains(EmployeeFilter.ToUpper()) == true)
						.OrderBy(x => x.LastName).ToList());
				}


				//if (e.PropertyName == "CurrentPayrollJob")
				//{
				//    OnStaticPropertyChanged("Employees");
				//}
			}
		}

		string _employeeFilter ;
		public string EmployeeFilter
		{
			get
			{
				return _employeeFilter;
			}
			set
			{
				_employeeFilter = value;
				OnStaticPropertyChanged("Employees");
			}
		}

		public new ObservableCollection<DataLayer.Employee> Employees
		{
			get => _employeelist;
			set
			{
				_employeelist = value;
				OnPropertyChanged("Employees");
			}
		}

		private ObservableCollection<DataLayer.Employee> _employeelist = null;
		//   public new ObservableCollection<DataLayer.Employee> Employees
		//   {
		//       get
		//       {
		//           if (_employeelist == null)
		//           {
		//               using (var ctx = new PayrollDB(Properties.Settings.Default.PayrollDB))
		//               {
		//                   try
		//                   {
		//                       _employeelist = new ObservableCollection<DataLayer.Employee>(ctx.Employees
		//                           .Where(x => x.EmploymentEndDate.HasValue == false ||
		//                                       EntityFunctions.TruncateTime(x.EmploymentEndDate.Value) >= EntityFunctions.TruncateTime(DateTime.Now)).ToList());
		//                   }
		//                   catch (Exception e)
		//                   {
		//                       Console.WriteLine(e);
		//                       throw;
		//                   }


		//               }
		//           }
		//           return _employeelist;
		//       }
		//       set
		//       {
		//           _employeelist = value;
		//           OnPropertyChanged("Employees");
		//       }
		//   }



	}
}