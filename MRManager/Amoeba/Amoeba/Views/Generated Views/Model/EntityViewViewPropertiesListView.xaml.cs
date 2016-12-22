using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for EntityViewViewProperties.xaml
	/// </summary>
	public partial class EntityViewViewPropertiesList_AutoGen
	{
		public EntityViewViewPropertiesList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("EntityViewViewPropertiesViewModelDataSource") as EntityViewViewPropertiesViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		EntityViewViewPropertiesViewModel_AutoGen im;

	}
}
