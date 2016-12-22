using System;
using System.Windows;
using System.Windows.Input;
using SystemMessages;
using EF.DBContexts;
using EF.Entities;
using EventAggregator;
using EventMessages;
using Interfaces;
using NH.DBContext;
using ViewModels;
using Application = System.Windows.Application;
using DataInterfaces;

namespace Amoeba
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			var t = new AmoebaDBContext().GetType().Assembly;
			var x = new EFEntity<IEntity>().GetType().Assembly;
			var d = new NHDBContext();
			BootStrapper.BootStrapper.Instance.StartUp(d,t ,x );
			
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


		public IApplicationSetting CurrentApplicationSetting => BaseViewModel.CurrentApplicationSetting;


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
	}
}
