using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CashSummaryManager.Annotations;
using CashSummaryManager.Converters;

namespace CashSummaryManager.ViewModels
{
    public class DrawerSelector: INotifyPropertyChanged
    {
        private static readonly DrawerSelector instance;

        public static DrawerSelector Instance
        {
            get { return instance; }
        }
        static DrawerSelector()
        {
            instance = new DrawerSelector();
        }
        private Drawer _drawer;

        public Drawer Drawer
        {
            get => _drawer; 
            set
            {
                if (_drawer == value) return;
                _drawer = value;
                GetDrawerSessions();
                OnPropertyChanged();
            }
        }

        private void GetDrawerSessions()
        {
           
            using (var ctx = new CashSummaryDBDataContext())
            {
                var res = ctx.DrawerSessions.Where(x => x.TradeDate == TradeDate);
                if (Store != null) res = res.Where(x => x.StoreId == Store.StoreId);
                if (Drawer != null) res = res.Where(x => x.DrawId == Drawer.DrawerId);
                if (Drawer != null) res = res.Where(x => x.DrawId == Drawer.DrawerId);

                DrawerSessions = new ObservableCollection<DrawerSession>(res.ToList());
                
            }
        }





        public bool HasSelectedDrawerSession => DrawerSession != null;
        

        private ObservableCollection<DrawerSession> _drawerSessions = new ObservableCollection<DrawerSession>();

        public ObservableCollection<DrawerSession> DrawerSessions
        {
            get => _drawerSessions;
            set
            {
                if (_drawerSessions == value) return;
                _drawerSessions = value;
                OnPropertyChanged(nameof(HasSelectedDrawerSession));
                OnPropertyChanged();
            }
        }

        private Store _store;

        public Store Store
        {
            get => _store;
            set
            {
                if (_store == value) return;
                _store = value;
                BaseViewModel.Instance.GetDrawers(_store.StoreId);
                GetDrawerSessions();
                OnPropertyChanged();
            }
        }

        private DateTime _tradeDate = DateTime.Today;

        public DateTime TradeDate
        {
            get => _tradeDate;
            set
            {
                if (_tradeDate == value) return;
                _tradeDate = value;
                GetDrawerSessions();
                OnPropertyChanged();
            }
        }

        private Cashier _cashier;

        public Cashier Cashier
        {
            get => _cashier;
            set
            {
                if (_cashier == value) return;
                _cashier = value;
                GetDrawerSessions();
                OnPropertyChanged();
            }
        }
        private Supervisor _supervisor;
        private DrawerSession _drawerSession;

        public Supervisor Supervisor
        {
            get => _supervisor;
            set
            {
                if (_supervisor == value) return;
                _supervisor = value;
                GetDrawerSessions();
                OnPropertyChanged();
            }
        }

        public DrawerSession DrawerSession
        {
            get => _drawerSession;
            set
            {
                if (Equals(value, _drawerSession)) return;
                _drawerSession = value;
                OnPropertyChanged(nameof(HasSelectedDrawerSession));
                OnPropertyChanged();
                CashBreakDown.Instance.GetDrawerSessionDetails();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
