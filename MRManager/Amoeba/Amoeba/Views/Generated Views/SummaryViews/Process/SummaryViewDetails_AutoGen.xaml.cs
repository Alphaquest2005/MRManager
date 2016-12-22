using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Views
{
	/// <summary>
	/// Interaction logic for SummaryList.xaml
	/// </summary>
	public partial class ProcessSummaryDetailsView 
	{
		public ProcessSummaryDetailsView()
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
		 
		private void GoToProcessSteps(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("ProcessStepsListEXP");
			e.Handled = true;
		}
		private void GoToProcessStepsAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("ProcessStepsAutoViewListEXP");
			e.Handled = true;
		}
		 
		private void GoToProcess(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("ProcessListEXP");
			e.Handled = true;
		}
		private void GoToProcessAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("ProcessAutoViewListEXP");
			e.Handled = true;
		}
		  
	}
}
