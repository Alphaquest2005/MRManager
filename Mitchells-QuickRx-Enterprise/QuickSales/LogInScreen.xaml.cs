using RMSDataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using Common.Core.Logging;
using log4netWrapper;
using TrackableEntities;

namespace QuickSales
{
    /// <summary>
    /// Interaction logic for LogInScreen.xaml
    /// </summary>
    public partial class LogInScreen : Window, INotifyPropertyChanged
    {

        enum Status
        {
            LoginScreen,
            OptionScreen,
            OpenDrawer,
            CloseDrawer,
            UserOptions
        }
        private Visibility hintVisibility;

        /// <summary>
        /// Creates a new instance of <c>LogOnScreen</c>.
        /// </summary>
        /// 
        Status _status = Status.LoginScreen;
Station station;
Cashier cashier;

        public Cashier Cashier
        {
            get { return cashier; }
            set
            {
                if (cashier != value)
                {
                    cashier = value;
                }
            }
        }
Batch batch;
public LogInScreen()
{
    try
    {

        InitializeComponent();

        DataContext = this;
        HintVisibility = Visibility.Hidden;
        this.Height = 179;
        xUsername.Focus();

        //if (db.TransactionBase.Count() >= 12000) Application.Current.Shutdown();
        using (var ctx = new RMSModel())
        {
            station = (from s in ctx.Stations.Include(x => x.Store).Include(x => x.Store.Company)
                       
                where s.MachineName == Environment.MachineName
                select s).FirstOrDefault();
        }
        if (station == null)
        {
            MessageBox.Show("Please configure WorkStation");
            return;
        }
        
       using (var ctx = new RMSModel())
        {
            batch = (from b in ctx.Batches
                where b.StationId == station.StationId && b.Status == "Open"
                select b).FirstOrDefault();
        }
        if (batch == null)
        {
            StatusTxt.Text = "Please Open Cash Drawer before continuing.";
        }
        else
        {
            StatusTxt.Text = "Cash Drawer Already Open. Continue to POS or Close Drawer.";
        }
      

        OptionsRow.Height = new GridLength(0);
        OpenDrawerRow.Height = new GridLength(0);
        CloseDrawerRow.Height = new GridLength(0);
        UserOptionsRow.Height = new GridLength(0);

        OpenDrawerGrd.DataContext = batch;
        CloseDrawerGrd.DataContext = batch;
    }
    catch (Exception ex)
    {

        throw ex;
    }
}


        /// <summary>
        /// Successful log on show options...
        /// </summary>
        /// 
        int RowHeight = 169;
        public void ShowOptions()
        {
            LoginRow.Height = new GridLength(0);
            OptionsRow.Height = new GridLength(RowHeight);
            _status = Status.OptionScreen;
        }
        public void ShowOpenDrawer()
        {
            OptionsRow.Height = new GridLength(0);
            OpenDrawerRow.Height = new GridLength(RowHeight);
            _status = Status.OpenDrawer;
            OptionsContinueBtn.Focus();
            if (batch != null)
                OpenDrawerGrd.DataContext = batch;


        }
        public void ShowCloseDrawer()
        {
            OptionsRow.Height = new GridLength(0);
            CloseDrawerRow.Height = new GridLength(375);
            _status = Status.CloseDrawer;
        }

        public void ShowUserOptions()
        {
            OptionsRow.Height = new GridLength(0);
            UserOptionsRow.Height = new GridLength(RowHeight);
            _status = Status.UserOptions;
        }

        public void HideUserOptions()
        {
            OptionsRow.Height = new GridLength(RowHeight);
            UserOptionsRow.Height = new GridLength(0);
            _status = Status.OptionScreen;
        }


        /// <summary>
        /// Returns the username entered within the UI.
        /// </summary>
        public string UserName
        {
            get { return xUsername.Text; }
        }

        /// <summary>
        /// Returns the password entered within the UI.
        /// </summary>
        public string Password
        {
            get { return xPassword.Password; }
        }

        private void DoLogonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
             Close();
        }

        public bool HintVisible
        {
            get { return HintVisibility == Visibility.Visible; }
            set
            {
                if (value)
                {
                    HintVisibility = Visibility.Visible;
                }
                else
                {
                    HintVisibility = Visibility.Hidden;
                }
            }
        }

        public Visibility HintVisibility
        {
            get { return hintVisibility; }
            set
            {
                if (value != hintVisibility)
                {
                    this.hintVisibility = value;
                    OnPropertyChanged("HintVisibility");
                }
            }
        }


        #region INotifyPropertyChanged Members

        private event PropertyChangedEventHandler propertyChangedEvent;

        public event PropertyChangedEventHandler PropertyChanged
        {
            add { propertyChangedEvent += value; }
            remove { propertyChangedEvent -= value; }
        }

        protected void OnPropertyChanged(string prop)
        {
            if (propertyChangedEvent != null)
                propertyChangedEvent(this, new PropertyChangedEventArgs(prop));
        }

        #endregion

        private void DoCredentialsFocussed(object sender, RoutedEventArgs e)
        {
            TextBoxBase tb = sender as TextBoxBase;
            if (tb == null)
            {
                PasswordBox pwb = sender as PasswordBox;
                pwb.SelectAll();
            }
            else
            {
                tb.SelectAll();
            }
        }



        private void OpenDrawerBtn_Click(object sender, RoutedEventArgs e)
        {
            ShowOpenDrawer();
        }



        private void Continue_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (batch == null)
                {
                    MessageBox.Show("Please Create Batch before continuing!");
                    return;
                }
                
                SalesRegion.SalesVM.Instance.Batch = batch;
                //SalesRegion.SalesVM.Instance.CashierEx = cashier;
                SalesRegion.SalesVM.Instance.Station = station;
                if (station != null) SalesRegion.SalesVM.Instance.Store = station.Store;

                Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

                Application.Current.MainWindow = null;

                this.Close();

                Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
                var shell = new Shell();
                shell.Show();

            }
            catch (Exception ex)
            {
                Logger.Log(LoggingLevel.Error, GetCurrentMethodClass.GetCurrentMethod() + ": --- :" + ex.Message + ex.StackTrace);
                throw ex;
            }

        }

        private void BackBtn_Click_1(object sender, RoutedEventArgs e)
        {
            using (var ctx = new RMSModel())
            {
                ctx.Persons.AddOrUpdate(cashier);
                ctx.SaveChanges();
            }
            HideUserOptions();
        }

        private void CloseDrawerBtn_Click(object sender, RoutedEventArgs e)
        {
            ShowCloseDrawer();
        }

        private void UserOptionsBtn_Click(object sender, RoutedEventArgs e)
        {
            UserOptionsGrd.DataContext = cashier;
            ShowUserOptions();
        }

  
        private void CreateBatchBtn_Click_1(object sender, RoutedEventArgs e)
        {
          
           

            if (cashier == null)
            {
                MessageBox.Show("Cashier not found. Please contact the administrator");
                return;
            }


            if (station == null)
            {
                MessageBox.Show("Station not found. Please contact the administrator");
                return;
            }

            if (batch == null)
            {
                using (var ctx = new RMSModel())
                {
                    batch = new Batch()
                    {
                        OpeningCash = System.Convert.ToDouble(OpeningAmountTxt.Text), OpeningTime = DateTime.Now,
                        Status = "Open", StationId = station.StationId, OpeningCashier = cashier.Id,
                        TrackingState = TrackingState.Added
                    };
                        
                    ctx.Batches.Add(batch);
                    OpenDrawerGrd.DataContext = batch;
                   ctx.SaveChanges();
                }
            }
            else
            {
                OpenDrawerGrd.DataContext = batch;
                MessageBox.Show("Batch already open. Please close batch before proceding!");
                return;
            }

        }

        private void CloseoutBtn_Click(object sender, RoutedEventArgs e)
        {
            if (batch != null)
            {
                batch.ClosingTime = DateTime.Now;
                batch.Status = "Close";
                batch.ClosingCashier = cashier.Id;
               // batch.CloseCashier = cashier;
               
                using (var ctx = new RMSModel())
                {
                    ctx.Batches.AddOrUpdate(batch);
                    ctx.SaveChanges();
                }
                CloseDrawerGrd.DataContext = batch;
            }
            else
            {
                MessageBox.Show("There are no open Batches!");
                return;
            }
        }

        private void ExitBtn_Click_1(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void PrintZBtn_Click_1(object sender, RoutedEventArgs e)
        {
            SUT.PrintEngine.PrintVisual.Print(ref ZGrd,batch.Station.ReceiptPrinterName);
        }

        private void BackBtn1_Click(object sender, RoutedEventArgs e)
        {
           // HideCloseDrawer();
            CloseDrawerRow.Height = new GridLength(0);
            ShowOptions();
        }


    }
}
        