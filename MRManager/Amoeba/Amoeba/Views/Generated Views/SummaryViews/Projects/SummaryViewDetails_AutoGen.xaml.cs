using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Views
{
	/// <summary>
	/// Interaction logic for SummaryList.xaml
	/// </summary>
	public partial class ProjectsSummaryDetailsView 
	{
		public ProjectsSummaryDetailsView()
		{
			this.InitializeComponent();

			// Insert code required on object creation below this point.
		}

		private void BringIntoView(object sender, MouseEventArgs e)
		{
			BringIntoView(sender);
			e.Handled = true;
		}

		private void BringIntoView(object sender)
		{
			if (typeof (Expander).IsInstanceOfType(sender))
			{
				Slider.BringIntoView(((FrameworkElement) sender) as Expander);
			}
			else
			{
				Expander p = ((FrameworkElement) sender).Parent as Expander;
				p.UpdateLayout();
				Slider.BringIntoView(p);
			}
		}
 
		private void GoToSettings(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("SettingsListEXP");
			e.Handled = true;
		}
		private void GoToSettingsAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("SettingsAutoViewListEXP");
			e.Handled = true;
		}
		 
		private void GoToProjects(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("ProjectsListEXP");
			e.Handled = true;
		}
		private void GoToProjectsAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("ProjectsAutoViewListEXP");
			e.Handled = true;
		}
		  
	}
}
