using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for ModelTypes.xaml
	/// </summary>
	public partial class ModelTypesAutoViewList_AutoGen
	{
		public ModelTypesAutoViewList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("ModelTypesAutoViewModelDataSource") as ModelTypesAutoViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		ModelTypesAutoViewModel_AutoGen im;

	}
}
