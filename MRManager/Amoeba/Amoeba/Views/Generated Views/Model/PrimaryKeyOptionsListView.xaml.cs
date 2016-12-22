using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for PrimaryKeyOptions.xaml
	/// </summary>
	public partial class PrimaryKeyOptionsList_AutoGen
	{
		public PrimaryKeyOptionsList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("PrimaryKeyOptionsViewModelDataSource") as PrimaryKeyOptionsViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		PrimaryKeyOptionsViewModel_AutoGen im;

	}
}
