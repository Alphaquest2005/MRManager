using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Views
{
	/// <summary>
	/// Interaction logic for SummaryList.xaml
	/// </summary>
	public partial class TriggersSummaryDetailsView 
	{
		public TriggersSummaryDetailsView()
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
 
		private void GoToStateMachineTriggers(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("StateMachineTriggersListEXP");
			e.Handled = true;
		}
		private void GoToStateMachineTriggersAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("StateMachineTriggersAutoViewListEXP");
			e.Handled = true;
		}
		 
		private void GoToTriggers(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("TriggersListEXP");
			e.Handled = true;
		}
		private void GoToTriggersAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("TriggersAutoViewListEXP");
			e.Handled = true;
		}
		  
	}
}
