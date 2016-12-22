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
	public partial class StateMachineTriggersAutoViewList_AutoGen
	{
		public StateMachineTriggersAutoViewList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("StateMachineTriggersAutoViewModelDataSource") as StateMachineTriggersAutoViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		StateMachineTriggersAutoViewModel_AutoGen im;

	}
}
