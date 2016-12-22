using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for EntityViewEntityProperties.xaml
	/// </summary>
	public partial class EntityViewEntityPropertiesList_AutoGen
	{
		public EntityViewEntityPropertiesList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("EntityViewEntityPropertiesViewModelDataSource") as EntityViewEntityPropertiesViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		EntityViewEntityPropertiesViewModel_AutoGen im;

	}
}
