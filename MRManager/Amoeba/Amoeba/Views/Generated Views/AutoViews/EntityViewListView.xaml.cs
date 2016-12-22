using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for EntityView.xaml
	/// </summary>
	public partial class EntityViewAutoViewList_AutoGen
	{
		public EntityViewAutoViewList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("EntityViewAutoViewModelDataSource") as EntityViewAutoViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		EntityViewAutoViewModel_AutoGen im;

	}
}
