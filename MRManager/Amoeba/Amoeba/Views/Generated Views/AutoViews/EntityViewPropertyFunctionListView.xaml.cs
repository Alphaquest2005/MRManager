using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for EntityViewPropertyFunction.xaml
	/// </summary>
	public partial class EntityViewPropertyFunctionAutoViewList_AutoGen
	{
		public EntityViewPropertyFunctionAutoViewList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("EntityViewPropertyFunctionAutoViewModelDataSource") as EntityViewPropertyFunctionAutoViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		EntityViewPropertyFunctionAutoViewModel_AutoGen im;

	}
}
