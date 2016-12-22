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
	public partial class PresentationPropertiesList_AutoGen
	{
		public PresentationPropertiesList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("PresentationPropertiesViewModelDataSource") as PresentationPropertiesViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		PresentationPropertiesViewModel_AutoGen im;

	}
}
