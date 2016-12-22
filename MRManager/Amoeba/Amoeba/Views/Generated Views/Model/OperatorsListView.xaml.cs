using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for Operators.xaml
	/// </summary>
	public partial class OperatorsList_AutoGen
	{
		public OperatorsList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("OperatorsViewModelDataSource") as OperatorsViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		OperatorsViewModel_AutoGen im;

	}
}
