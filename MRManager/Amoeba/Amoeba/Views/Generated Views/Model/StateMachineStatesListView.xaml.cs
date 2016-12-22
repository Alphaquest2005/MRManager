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
	public partial class StateMachineStatesList_AutoGen
	{
		public StateMachineStatesList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("StateMachineStatesViewModelDataSource") as StateMachineStatesViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		StateMachineStatesViewModel_AutoGen im;

	}
}
