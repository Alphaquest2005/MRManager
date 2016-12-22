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
	public partial class StateMachinesList_AutoGen
	{
		public StateMachinesList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("StateMachinesViewModelDataSource") as StateMachinesViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		StateMachinesViewModel_AutoGen im;

	}
}
