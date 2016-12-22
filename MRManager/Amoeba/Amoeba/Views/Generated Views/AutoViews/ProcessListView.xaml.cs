using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for Process.xaml
	/// </summary>
	public partial class ProcessAutoViewList_AutoGen
	{
		public ProcessAutoViewList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("ProcessAutoViewModelDataSource") as ProcessAutoViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		ProcessAutoViewModel_AutoGen im;

	}
}
