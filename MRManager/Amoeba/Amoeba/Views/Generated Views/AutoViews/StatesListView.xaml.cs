using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for States.xaml
	/// </summary>
	public partial class StatesAutoViewList_AutoGen
	{
		public StatesAutoViewList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("StatesAutoViewModelDataSource") as StatesAutoViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		StatesAutoViewModel_AutoGen im;

	}
}
