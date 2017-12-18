using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using PayrollManager.DataLayer;

namespace PayrollManager
{
	public class PayrollSetupItemsModel : BaseViewModel
	{
		public PayrollSetupItemsModel()
		{
			staticPropertyChanged += PayrollSetupItemsModel_staticPropertyChanged;
		}

		private void PayrollSetupItemsModel_staticPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			OnPropertyChanged(e.PropertyName);
			if (e.PropertyName == "InstitutionAccounts")
			{
				OnPropertyChanged("InstitutionAccountsList");
			}
		}

		public ObservableCollection<DataLayer.InstitutionAccount> InstitutionAccountsList
		{
			get
			{
				using (var ctx = new PayrollDB(Properties.Settings.Default.PayrollDB))
				{
					return new ObservableCollection<DataLayer.InstitutionAccount>(ctx.Accounts
						.OfType<DataLayer.InstitutionAccount>());
				}
			}
		}

		public void SavePayrollSetupItem()
		{
			using (var ctx = new PayrollDB(Properties.Settings.Default.PayrollDB))
			{
                if (CurrentPayrollSetupItem == null) return;
                if (CurrentPayrollSetupItem.PayrollSetupItemId == 0)
				{
					ctx.PayrollSetupItems.AddObject(CurrentPayrollSetupItem);
				}
				else
				{
				    if (CurrentPayrollSetupItem.EntityState == EntityState.Added) return;
                    var ritm = ctx.PayrollSetupItems.First(
						x => x.PayrollSetupItemId == CurrentPayrollSetupItem.PayrollSetupItemId);
					ctx.PayrollSetupItems.Attach(ritm);
					ctx.PayrollSetupItems.ApplyCurrentValues(CurrentPayrollSetupItem);
				}

				SaveDatabase(ctx);

			}
			OnStaticPropertyChanged("PayrollSetupItems");
			OnStaticPropertyChanged("CurrentPayrollSetupItem");
			OnStaticPropertyChanged("PayrollSetupItemsCollection");

		}

		public void DeletePayrollSetupItem()
		{
            if (CurrentPayrollSetupItem == null) return;
            if (CurrentPayrollSetupItem.PayrollSetupItemId != 0)
			{
				using (var ctx = new PayrollDB(Properties.Settings.Default.PayrollDB))
				{
					var ritm = ctx.PayrollSetupItems.First(
						x => x.PayrollSetupItemId == CurrentPayrollSetupItem.PayrollSetupItemId);
					ctx.PayrollSetupItems.DeleteObject(ritm);
					SaveDatabase(ctx);
				}
			}
			CurrentPayrollSetupItem = null;
			OnStaticPropertyChanged("PayrollSetupItems");
			OnStaticPropertyChanged("PayrollSetupItemsCollection");
			OnStaticPropertyChanged("CurrentPayrollSetupItem");

		}

		public void NewPayrollSetupItem()
		{
			using (var ctx = new PayrollDB(Properties.Settings.Default.PayrollDB))
			{
				PayrollSetupItem newpi = ctx.PayrollSetupItems.CreateObject();
				ctx.PayrollSetupItems.AddObject(newpi);
				CurrentPayrollSetupItem = newpi;
				OnPropertyChanged("CurrentPayrollSetupItem");
			}


		}

		//DataLayer.PayrollSetupItem _newPayrollSetupItem;
		//public override DataLayer.PayrollSetupItem CurrentPayrollSetupItem
		//{
		//    get
		//    {
		//        if (_newPayrollSetupItem == null)
		//        {
		//            return base.CurrentPayrollSetupItem;
		//        }
		//        else
		//        {
		//            return _newPayrollSetupItem;
		//        }
		//    }
		//    set
		//    {
		//        base.CurrentPayrollSetupItem = value;
		//        OnStaticPropertyChanged("PayrollSetupItems");
		//        OnStaticPropertyChanged("PayrollSetupItemsCollection");
		//        OnStaticPropertyChanged("CurrentPayrollSetupItem");

		//    }
		//}

		#region INotifyPropertyChanged
		public event PropertyChangedEventHandler PropertyChanged;

		private void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}
		#endregion
	}
}