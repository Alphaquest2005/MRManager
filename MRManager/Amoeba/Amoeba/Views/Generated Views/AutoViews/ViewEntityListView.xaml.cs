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
	public partial class ViewEntityAutoViewList_AutoGen
	{
		public ViewEntityAutoViewList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("ViewEntityAutoViewModelDataSource") as ViewEntityAutoViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		ViewEntityAutoViewModel_AutoGen im;

	}
}
