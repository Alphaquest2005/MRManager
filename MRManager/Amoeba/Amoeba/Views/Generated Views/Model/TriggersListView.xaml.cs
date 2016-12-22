using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for Triggers.xaml
	/// </summary>
	public partial class TriggersList_AutoGen
	{
		public TriggersList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("TriggersViewModelDataSource") as TriggersViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		TriggersViewModel_AutoGen im;

	}
}
