using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Views
{
	/// <summary>
	/// Interaction logic for SummaryList.xaml
	/// </summary>
	public partial class ScreensSummaryDetailsView 
	{
		public ScreensSummaryDetailsView()
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
 
		private void GoToProcessStepScreens(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("ProcessStepScreensListEXP");
			e.Handled = true;
		}
		private void GoToProcessStepScreensAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("ProcessStepScreensAutoViewListEXP");
			e.Handled = true;
		}
		 
		private void GoToScreenLayouts(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("ScreenLayoutsListEXP");
			e.Handled = true;
		}
		private void GoToScreenLayoutsAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("ScreenLayoutsAutoViewListEXP");
			e.Handled = true;
		}
		 
		private void GoToScreenParts(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("ScreenPartsListEXP");
			e.Handled = true;
		}
		private void GoToScreenPartsAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("ScreenPartsAutoViewListEXP");
			e.Handled = true;
		}
		 
		private void GoToScreenViews(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("ScreenViewsListEXP");
			e.Handled = true;
		}
		private void GoToScreenViewsAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("ScreenViewsAutoViewListEXP");
			e.Handled = true;
		}
		 
		private void GoToScreens(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("ScreensListEXP");
			e.Handled = true;
		}
		private void GoToScreensAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("ScreensAutoViewListEXP");
			e.Handled = true;
		}
		  
	}
}
