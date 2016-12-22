using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for States.xaml
	/// </summary>
	public partial class StatesList_AutoGen
	{
		public StatesList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("StatesViewModelDataSource") as StatesViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		StatesViewModel_AutoGen im;

	}
}
