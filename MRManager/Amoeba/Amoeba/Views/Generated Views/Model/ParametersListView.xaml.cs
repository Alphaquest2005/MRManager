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
	public partial class ParametersList_AutoGen
	{
		public ParametersList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("ParametersViewModelDataSource") as ParametersViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		ParametersViewModel_AutoGen im;

	}
}
