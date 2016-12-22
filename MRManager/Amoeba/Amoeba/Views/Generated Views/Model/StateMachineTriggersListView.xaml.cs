using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for StateMachineTriggers.xaml
	/// </summary>
	public partial class StateMachineTriggersList_AutoGen
	{
		public StateMachineTriggersList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("StateMachineTriggersViewModelDataSource") as StateMachineTriggersViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		StateMachineTriggersViewModel_AutoGen im;

	}
}
