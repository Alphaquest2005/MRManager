using PayrollManager.DataLayer;
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

namespace PayrollManager
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
            UserOptions
        }
        private Visibility hintVisibility;

        /// <summary>
        /// Creates a new instance of <c>LogOnScreen</c>.
        /// </summary>
        /// 
        Status _status = Status.LoginScreen;

        public LogInScreen()
        {
            InitializeComponent();

            DataContext = this;
            HintVisibility = Visibility.Hidden;
            this.Height = 179;
            xUsername.Focus();

        OptionsRow.Height = new GridLength(0);
               
        UserOptionsRow.Height = new GridLength(0);

        
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
        
        public void ShowUserOptions()
        {
            OptionsRow.Height = new GridLength(0);
            UserOptionsRow.Height = new GridLength(RowHeight);
            _status = Status.UserOptions;
            OptionsContinueBtn.Focus();
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
            OptionsContinueBtn.Focus();
            try
            {
                DialogResult = true;

                Close();
            }
            catch
            {
            }
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




        private void Continue_Click_1(object sender, RoutedEventArgs e)
        {
            //if (Application.Current.Windows.Count == 1)
            //{
                Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

                Application.Current.MainWindow = null;

                this.Close();

                Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;

                Window frm = new MainWindow();
                Application.Current.MainWindow = frm;
                frm.Show();
               // Bootstrapper bootStrapper = new Bootstrapper();
               // bootStrapper.Run();
            //}
            //else
            //{
            //    // log off cashier
                
            //    Application.Current.Shutdown();
            //}
        }

        private void BackBtn_Click_1(object sender, RoutedEventArgs e)
        {
            db.SaveChanges();
            HideUserOptions();
        }



        PayrollDB db = new PayrollDB();
        
        private void ExitBtn_Click_1(object sender, RoutedEventArgs e)
        {


            App.Current.Shutdown();
        }

        
        private void BackBtn1_Click(object sender, RoutedEventArgs e)
        {
           // HideCloseDrawer();
           
            ShowOptions();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            User user = (from c in db.Employees.OfType<User>()
                                 where c.EmployeeId == App.CurrentUser.EmployeeId
                                 select c).First();
            user.Password = App.CurrentUser.Password;
            user.Username = App.CurrentUser.Username;
            db.SaveChanges();
            MessageBox.Show("Saved");
        }

        private void UserOptionsBtn_Click(object sender, RoutedEventArgs e)
        {
            UserOptionsGrd.DataContext = App.CurrentUser;
            ShowUserOptions();
        }


    }
}
        