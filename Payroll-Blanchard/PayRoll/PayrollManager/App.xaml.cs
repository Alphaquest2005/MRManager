using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Linq;
using EmailLogger;

namespace PayrollManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 

    public partial class App : Application
    {
        DataLayer.PayrollDB db = null;
        static DataLayer.User _currentUser;

        /// <summary>
        /// Creates a new instance of <c>App</c>
        /// </summary>   
        /// 
        public static SplashScreen2 splash = new SplashScreen2(@"blue_purple_green_copy-408x259.jpg");
        //[MyExceptionHandlerAspect]
        public App()
        {
            
               
            try
            {
                db = new DataLayer.PayrollDB();
                splash.Show(TimeSpan.FromSeconds(0));
                Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;
           
                this.Dispatcher.UnhandledException += OnDispatcherUnhandledException;
                LoginRoutine();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "|" + ex.StackTrace);
                throw ex;
            }

        }
        string machineName = "";

        //[MyExceptionHandlerAspect]
        void OnDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            string errorMessage = string.Format("An unhandled exception occurred!: {0} ---- {1}", e.Exception.Message, e.Exception.StackTrace);
            MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            if (!errorMessage.Contains("Failure sending mail"))
            {
                var en = new EmailLogger.EmailslNotifications("Exception Occurred");
                en.SendNotificationEmail("",
                    PayrollManager.Properties.Settings.Default.MachineName + ".com",
                    PayrollManager.Properties.Settings.Default.MachineName,
                    @"logs@insight-software.biz",
                    null,
                    "Exception Occurred",
                    "", "", e.Exception.GetType().Name, e.Exception.Message, e.Exception.StackTrace);
            }


            e.Handled = true;
           // throw new Exception(errorMessage);
          
        }

        public static DataLayer.User CurrentUser
        {
            get
            {
                return _currentUser;
            }
            set
            {
                _currentUser = value;

            }
        }


        void App_SessionEnding(object sender, SessionEndingCancelEventArgs e)
        {

        }



        private bool Authenticate(string user, string pass)
        {
            // RMSModel db = new RMSModel();
            CurrentUser = (from c in db.Employees.OfType<DataLayer.User>()
                           where c.Username == user
                           select c).FirstOrDefault();

            if (CurrentUser != null && CurrentUser.Password == pass)
            {

                return true;
            }
            else
            {
                return false;
            }

        }

       

        public void LoginRoutine()
        {
            LogInScreen logon = new LogInScreen();
#if DEBUG
            logon.HintVisible = true;
#endif
            bool? res = logon.ShowDialog();
            if (!res ?? true)
            {
                Shutdown(1);
            }
            else
                if (Authenticate(logon.UserName, logon.Password))
                {
                    //StartupContainer();
                    LogInScreen logon1 = new LogInScreen();
                    logon1.Show();
                    logon1.ShowOptions();
                }
                else
                {
                    MessageBox.Show(
                        "Application is exiting due to invalid credentials",
                        "Application Exit",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    Shutdown(1);
                }
        }

        private void StartupContainer()
        {
            Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            Application.Current.MainWindow = null;

            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;

            Window frm = new MainWindow();
            Application.Current.MainWindow = frm;
            frm.Show();

        }
    }

}
