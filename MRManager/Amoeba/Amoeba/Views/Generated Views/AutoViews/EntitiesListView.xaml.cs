using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for Entities.xaml
	/// </summary>
	public partial class EntitiesAutoViewList_AutoGen
	{
		public EntitiesAutoViewList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("EntitiesAutoViewModelDataSource") as EntitiesAutoViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		EntitiesAutoViewModel_AutoGen im;

	}
}
