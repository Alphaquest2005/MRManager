using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using CashSummaryManager.Annotations;
using CheckManager;

namespace CashSummaryManager
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        private static BaseViewModel instance;
        


        static BaseViewModel()
        {
            instance = new BaseViewModel();
            

        }

        public static BaseViewModel Instance
        {
            get { return instance; }
            set
            {
                instance = value; 
            }
        }


        public User User { get; set; }


        public BaseViewModel()
        {


            Application.Current.Dispatcher.Invoke(() => { GetStores(); });

           
        }

        public void GetDrawers(string storeId)
        {
            using (var ctx = new CashSummaryDBDataContext())
            {
                Drawers = new ObservableCollection<Drawer>(ctx.Drawers.Where(x => x.StoreId == storeId));
                Cashiers = new ObservableCollection<Cashier>(ctx.Cashiers.Where(x => x.WorkGroupId == storeId));
                Supervisors = new ObservableCollection<Supervisor>(ctx.Supervisors.Where(x => x.WorkGroupId == storeId));
            }
        }



        private void GetStores()
        {
            using (var ctx = new CashSummaryDBDataContext())
            {
                Stores = new ObservableCollection<Store>(ctx.Stores);
                
            }
        }



        private ObservableCollection<Store> _stores;

        public ObservableCollection<Store> Stores
        {
            get => _stores;
            set
            {
                if (_stores == value) return;
                _stores = value;

                OnPropertyChanged();
            }
        }

        private ObservableCollection<Drawer> _drawers;

        public ObservableCollection<Drawer> Drawers
        {
            get => _drawers;
            set
            {
                if (_drawers == value) return;
                _drawers = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Cashier> _cashiers;

        public ObservableCollection<Cashier> Cashiers
        {
            get => _cashiers;
            set
            {
                if (_cashiers == value) return;
                _cashiers = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Supervisor> _supervisors;

        public ObservableCollection<Supervisor> Supervisors
        {
            get => _supervisors;
            set
            {
                if (_supervisors == value) return;
                _supervisors = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Search(string searchTxt)
        {
            SearchTxt = searchTxt;
           
            
        }

        public string SearchTxt { get; set; } = "";
        

        public void Print()
        {
            throw new NotImplementedException();
        }

        
    }
}
