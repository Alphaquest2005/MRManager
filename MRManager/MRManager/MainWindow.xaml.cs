using System.Windows;
using System.Windows.Input;
using SystemInterfaces;
using Core.Common.UI;
using EF.DBContexts;
using EF.Entities;
using NH.DBContext;
using Application = System.Windows.Application;
using DataInterfaces;

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
			var t = new MRManagerDBContext().GetType().Assembly;
			var x = new EFEntity<IEntity>().GetType().Assembly;
			var d = new NHDBContext();
			BootStrapper.BootStrapper.Instance.StartUp(d,t ,x );
			
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
