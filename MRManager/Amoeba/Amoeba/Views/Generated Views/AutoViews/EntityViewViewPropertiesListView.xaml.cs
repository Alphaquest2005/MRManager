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
	public partial class EntityViewViewPropertiesAutoViewList_AutoGen
	{
		public EntityViewViewPropertiesAutoViewList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("EntityViewViewPropertiesAutoViewModelDataSource") as EntityViewViewPropertiesAutoViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		EntityViewViewPropertiesAutoViewModel_AutoGen im;

	}
}
