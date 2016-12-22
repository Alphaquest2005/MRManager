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
	public partial class EntitiesList_AutoGen
	{
		public EntitiesList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("EntitiesViewModelDataSource") as EntitiesViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		EntitiesViewModel_AutoGen im;

	}
}
