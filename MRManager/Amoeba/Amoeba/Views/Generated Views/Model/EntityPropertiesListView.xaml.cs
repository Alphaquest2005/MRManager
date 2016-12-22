using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for EntityProperties.xaml
	/// </summary>
	public partial class EntityPropertiesList_AutoGen
	{
		public EntityPropertiesList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("EntityPropertiesViewModelDataSource") as EntityPropertiesViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		EntityPropertiesViewModel_AutoGen im;

	}
}
