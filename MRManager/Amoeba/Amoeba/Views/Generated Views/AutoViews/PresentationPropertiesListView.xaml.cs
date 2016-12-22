using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for PresentationProperties.xaml
	/// </summary>
	public partial class PresentationPropertiesAutoViewList_AutoGen
	{
		public PresentationPropertiesAutoViewList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("PresentationPropertiesAutoViewModelDataSource") as PresentationPropertiesAutoViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		PresentationPropertiesAutoViewModel_AutoGen im;

	}
}
