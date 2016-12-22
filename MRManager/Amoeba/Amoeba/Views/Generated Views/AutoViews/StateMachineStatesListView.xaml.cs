using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for StateMachineStates.xaml
	/// </summary>
	public partial class StateMachineStatesAutoViewList_AutoGen
	{
		public StateMachineStatesAutoViewList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("StateMachineStatesAutoViewModelDataSource") as StateMachineStatesAutoViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		StateMachineStatesAutoViewModel_AutoGen im;

	}
}
