using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for Layout.xaml
	/// </summary>
	public partial class LayoutAutoViewList_AutoGen
	{
		public LayoutAutoViewList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("LayoutAutoViewModelDataSource") as LayoutAutoViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		LayoutAutoViewModel_AutoGen im;

	}
}
