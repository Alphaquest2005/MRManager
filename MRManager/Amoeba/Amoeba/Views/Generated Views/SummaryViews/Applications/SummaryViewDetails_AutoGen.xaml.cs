using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Views
{
	/// <summary>
	/// Interaction logic for SummaryList.xaml
	/// </summary>
	public partial class ApplicationsSummaryDetailsView 
	{
		public ApplicationsSummaryDetailsView()
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
 
		private void GoToApplicationEntities(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("ApplicationEntitiesListEXP");
			e.Handled = true;
		}
		private void GoToApplicationEntitiesAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("ApplicationEntitiesAutoViewListEXP");
			e.Handled = true;
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
		 
		private void GoToApplications(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("ApplicationsListEXP");
			e.Handled = true;
		}
		private void GoToApplicationsAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("ApplicationsAutoViewListEXP");
			e.Handled = true;
		}
		  
	}
}
