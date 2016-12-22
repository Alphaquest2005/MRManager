using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for Steps.xaml
	/// </summary>
	public partial class StepsAutoViewList_AutoGen
	{
		public StepsAutoViewList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("StepsAutoViewModelDataSource") as StepsAutoViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		StepsAutoViewModel_AutoGen im;

	}
}
