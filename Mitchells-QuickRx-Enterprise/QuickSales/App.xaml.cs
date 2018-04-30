using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Reflection;
using System.Windows;
using log4netWrapper;
using QuickSales.Properties;
using RMSDataAccessLayer;
using SalesRegion;
using TrackableEntities;

namespace QuickSales
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
       
        private bool Authenticate(string user, string pass)
        {
            
            Logger.Log(LoggingLevel.Info, "Start Authenticating");

            Logger.Log(LoggingLevel.Info, string.Format("Accessing Database"));

            

            using (var ctx = new RMSModel())
            {
                var cashier = (from c in ctx.Persons.OfType<Cashier>()
                    where c.LoginName == user
                    select c).FirstOrDefault();
                Logger.Log(LoggingLevel.Info, string.Format("Cashier: [{0}]", cashier != null?cashier.LoginName:""));
                if (cashier != null && cashier.SPassword == pass)
                {
                    return LogIn(cashier);
                }
                else
                {
                    Logger.Log(LoggingLevel.Info, string.Format("Log in Failed for Cashier: [{0}]", cashier != null ? cashier.LoginName : ""));
                    return false;
                }
            }
        }
        public Cashier _cashier = null;
        public Cashier Cashier
        {
            get
            {
                return _cashier;
            }
        }

        private bool LogIn(Cashier cashier)
        {
            Logger.Log(LoggingLevel.Info, string.Format("Login Cashier: [{0}]", cashier != null ? cashier.LoginName : ""));
            _cashier = cashier;
            using (var ctx = new RMSModel())
            {
                CashierLog log =
                    ctx.CashierLogs
                        .OrderByDescending(x => x.LoginTime)
                        .FirstOrDefault(x => x.PersonId == cashier.Id && x.MachineName == Environment.MachineName);
                if (log != null)
                {
                    LogOut(cashier);
                }
                SalesRegion.SalesVM.Instance.CashierEx = cashier;

                log = new CashierLog()
                {
                    MachineName = Environment.MachineName,
                    LoginTime = DateTime.Now,
                    Status = "LogIn",
                    PersonId = cashier.Id,
                    TrackingState = TrackingState.Added
                };
                   
                
                ctx.CashierLogs.Add(log);

                ctx.SaveChanges();
            }
            //  db.Dispose();
            return true;
        }


        private bool LogOut(Cashier cashier)
        {
            if (cashier == null) return false;
            
            _cashier = cashier;
            using(var ctx = new RMSModel())
            {
                CashierLog log = ctx.CashierLogs.OrderByDescending(x => x.LoginTime).FirstOrDefault(x => x.PersonId == cashier.Id && x.MachineName == Environment.MachineName);

                if (log != null && log.Status == "LogIn")
                {
                     log.MachineName = Environment.MachineName;
                     log.LogoutTime = DateTime.Now;
                    log.Status = "LogOut";
                    log.PersonId = cashier.Id;
                    //log.Cashier = cashier;

                    ctx.CashierLogs.AddOrUpdate(log);
                    ctx.SaveChanges();
                }
                //db.Dispose(); 
                return true;
            }
            return false;
        }
        /// <summary>
        /// Creates a new instance of <c>App</c>
        /// </summary>
        public App()
        {
            Logger.Initialize();
            Logger.Log(LoggingLevel.Info, string.Format("Application Started - {0}",DateTime.Now));
            Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            Dispatcher.UnhandledException += OnUnhandledException;

            SalesVM.Instance.ServerMode = Settings.Default.ServerMode;

            LoginRoutine();
            Application.Current.Exit += OnApplicationExit;
        }

        private void OnUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
        
            var lastexception = false;
            Exception ex = e.Exception ;
            while (lastexception == false)
            {
                
                if (ex.InnerException == null)
                {
                    lastexception = true;
                    var errorMessage = string.Format("An unhandled Exception occurred!: {0} ---- {1}", ex.Message, ex.StackTrace);
                    Logger.Log(LoggingLevel.Error, errorMessage);
                    MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    e.Handled = true;
                }
                ex = ex.InnerException;

            }
        
        }

        private void OnApplicationExit(object sender, ExitEventArgs e)
        {
            LogOut(_cashier);
        }

        public  void LoginRoutine()
        {
            Logger.Log(LoggingLevel.Info, string.Format("Show login Screen"));
            LogInScreen logon = new LogInScreen();
#if DEBUG
            logon.HintVisible = true;
#endif
            bool? res = logon.ShowDialog();
            if (!res ?? true)
            {
                Logger.Log(LoggingLevel.Info, string.Format("Login Canceled Shutting down"));
                Shutdown(1);
            }
            else 
                if (Authenticate(logon.UserName, logon.Password))
            {
                //StartupContainer();
                LogInScreen logon1 = new LogInScreen();
                logon1.Cashier = _cashier;
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
           

           
        }
    }
}
