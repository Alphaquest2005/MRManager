using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using SystemInterfaces;
using Core.Common.UI;
using EF.DBContexts;
using EF.Entities;
using RevolutionLogger;
using Application = System.Windows.Application;

namespace MRManager
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
            if (File.Exists("MRManager-Logs.xml")) File.Delete("MRManager-Logs.xml");
            Logger.Initialize();

            Logger.Log(LoggingLevel.Info, $"The UI Thread is:{Application.Current.Dispatcher.Thread.ManagedThreadId}");

            Task.Run(() =>
		    {
                var dbContextAssembly = new MRManagerDBContext().GetType().Assembly;
                var entitiesAssembly = new EFEntity<IEntity>().GetType().Assembly;
                BootStrapper.BootStrapper.Instance.StartUp( true, Process.WorkFlow.MachineInfoData.MachineInfos, Process.WorkFlow.Processes.ProcessInfos, Process.WorkFlow.Processes.ProcessComplexEvents, ViewModel.WorkFlow.ProcessViewModels.ProcessViewModelInfos.Skip(1).ToList(),dbContextAssembly,entitiesAssembly);
		    }).ConfigureAwait(false);


		}
        private void BackBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (IsMouseOver == true)
            {
                AppSlider.Slider.MoveToPreviousCtl();
            }
        }
        private void Window_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			DragMove();
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();

		}

		private void Grid_PreviewMouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
		{
			DragMove();
		}


		//public IApplicationSetting CurrentApplicationSetting => BaseViewModel.CurrentApplicationSetting;


		private void MinimizeWindow(object sender, MouseButtonEventArgs e)
		{
			WindowState = WindowState.Minimized;
		}

		private void SwitchWindowState(object sender, MouseButtonEventArgs e)
		{
			if (this.WindowState == WindowState.Maximized)
			{
				WindowState = WindowState.Normal;
				return;
			}
			if (WindowState == WindowState.Normal)
			{
				WindowState = WindowState.Maximized;

			}

		}

        private void Screen_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }
    }
}
