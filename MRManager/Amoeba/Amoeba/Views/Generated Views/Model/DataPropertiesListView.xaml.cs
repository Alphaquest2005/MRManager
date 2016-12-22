using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for DataProperties.xaml
	/// </summary>
	public partial class DataPropertiesList_AutoGen
	{
		public DataPropertiesList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("DataPropertiesViewModelDataSource") as DataPropertiesViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		DataPropertiesViewModel_AutoGen im;

	}
}
