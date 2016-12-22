using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for Views.xaml
	/// </summary>
	public partial class ViewsList_AutoGen
	{
		public ViewsList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("ViewsViewModelDataSource") as ViewsViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		ViewsViewModel_AutoGen im;

	}
}
