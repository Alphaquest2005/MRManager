using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for Parameters.xaml
	/// </summary>
	public partial class ParametersAutoViewList_AutoGen
	{
		public ParametersAutoViewList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("ParametersAutoViewModelDataSource") as ParametersAutoViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		ParametersAutoViewModel_AutoGen im;

	}
}
