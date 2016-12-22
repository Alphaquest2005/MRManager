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
	public partial class TriggersAutoViewList_AutoGen
	{
		public TriggersAutoViewList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("TriggersAutoViewModelDataSource") as TriggersAutoViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		TriggersAutoViewModel_AutoGen im;

	}
}
