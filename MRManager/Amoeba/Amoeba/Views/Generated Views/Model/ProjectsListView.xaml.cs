using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for Projects.xaml
	/// </summary>
	public partial class ProjectsList_AutoGen
	{
		public ProjectsList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("ProjectsViewModelDataSource") as ProjectsViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		ProjectsViewModel_AutoGen im;

	}
}
