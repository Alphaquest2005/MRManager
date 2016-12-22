using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for EntityViewProperties.xaml
	/// </summary>
	public partial class EntityViewPropertiesAutoViewList_AutoGen
	{
		public EntityViewPropertiesAutoViewList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("EntityViewPropertiesAutoViewModelDataSource") as EntityViewPropertiesAutoViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		EntityViewPropertiesAutoViewModel_AutoGen im;

	}
}
