using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MNIB_Distribution_Manager;

namespace CheckManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private bool Authenticate(string user, string pass)
        {

           
            using (var ctx = new ChequeDBDataContext())
            {
                _user = ctx.Users.FirstOrDefault(x => x.LoginName == user);
                
               
                if (_user != null && _user.Password == pass)
                {
                    _user.UserPermissions.Load();
                    return LogIn();
                }
                else
                {
                  
                    return false;
                }
            }
        }
        private User _user = null;
        public User User
        {
            get
            {
                return _user;
            }
        }

        private bool LogIn()
        {
            
            
            using (var ctx = new ChequeDBDataContext())
            {
                UserLog log =
                    ctx.UserLogs
                        .FirstOrDefault(x => x.UserId == _user.Id && x.MachineName == Environment.MachineName && x.LoginTime != null);
                if (log != null)
                {
                    LogOut();
                }
                CheckViewModel.Instance.User = _user;

                log = new UserLog()
                {
                    MachineName = Environment.MachineName,
                    LoginTime = DateTime.Now,
                    Status = "LogIn",
                    UserId = _user.Id,
                    
                };

                ctx.UserLogs.InsertOnSubmit(log);
                //ctx.UserLogs.Add(log);

                ctx.SubmitChanges();
                
            }
            //  db.Dispose();
            return true;
        }


        private bool LogOut()
        {
            if (_user == null) return false;

            
            using (var ctx = new ChequeDBDataContext())
            {
                UserLog log = ctx.UserLogs.OrderByDescending(x => x.LoginTime).FirstOrDefault(x => x.UserId == _user.Id && x.MachineName == Environment.MachineName);

                if (log != null && log.Status == "LogIn")
                {
                    log.MachineName = Environment.MachineName;
                    log.LogoutTime = DateTime.Now;
                    log.Status = "LogOut";
                    log.UserId = _user.Id;
                    //log.User = user;

                    //ctx.UserLogs.(log);
                    ctx.SubmitChanges();
                }
                //db.Dispose(); 
                return true;
            }

        }
        /// <summary>
        /// Creates a new instance of <c>App</c>
        /// </summary>
        public App()
        {
            
            Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            Dispatcher.UnhandledException += OnUnhandledException;
            LoginRoutine();
            Application.Current.Exit += OnApplicationExit;
        }

        private void OnUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {

            var lastexception = false;
            while (lastexception == false)
            {
                if (e.Exception.InnerException == null)
                {
                    lastexception = true;
                    var errorMessage = string.Format("An unhandled Exception occurred!: {0} ---- {1}", e.Exception.Message, e.Exception.StackTrace);
                    MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    e.Handled = true;
                }

            }

        }

        private void OnApplicationExit(object sender, ExitEventArgs e)
        {
            LogOut();
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
                CheckViewModel.Instance.User = User;
                var shell = new MainWindow();
                shell.Show();

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
    }
}
