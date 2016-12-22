using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for StateMachines.xaml
	/// </summary>
	public partial class StateMachinesAutoViewList_AutoGen
	{
		public StateMachinesAutoViewList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("StateMachinesAutoViewModelDataSource") as StateMachinesAutoViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		StateMachinesAutoViewModel_AutoGen im;

	}
}
