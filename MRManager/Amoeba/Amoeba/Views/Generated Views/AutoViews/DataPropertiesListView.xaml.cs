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
	public partial class DataPropertiesAutoViewList_AutoGen
	{
		public DataPropertiesAutoViewList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("DataPropertiesAutoViewModelDataSource") as DataPropertiesAutoViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		DataPropertiesAutoViewModel_AutoGen im;

	}
}
