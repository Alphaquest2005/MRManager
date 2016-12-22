using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for EntityViewPropertyFunctionParameter.xaml
	/// </summary>
	public partial class EntityViewPropertyFunctionParameterAutoViewList_AutoGen
	{
		public EntityViewPropertyFunctionParameterAutoViewList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("EntityViewPropertyFunctionParameterAutoViewModelDataSource") as EntityViewPropertyFunctionParameterAutoViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		EntityViewPropertyFunctionParameterAutoViewModel_AutoGen im;

	}
}
