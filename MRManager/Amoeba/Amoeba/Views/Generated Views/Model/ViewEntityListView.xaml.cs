using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for ViewEntity.xaml
	/// </summary>
	public partial class ViewEntityList_AutoGen
	{
		public ViewEntityList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("ViewEntityViewModelDataSource") as ViewEntityViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		ViewEntityViewModel_AutoGen im;

	}
}
