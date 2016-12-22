using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Views
{
	/// <summary>
	/// Interaction logic for SummaryList.xaml
	/// </summary>
	public partial class EntitiesSummaryDetailsView 
	{
		public EntitiesSummaryDetailsView()
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
		 
		private void GoToEntityViewEntityProperties(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("EntityViewEntityPropertiesListEXP");
			e.Handled = true;
		}
		private void GoToEntityViewEntityPropertiesAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("EntityViewEntityPropertiesAutoViewListEXP");
			e.Handled = true;
		}
		 
		private void GoToEntityViewPropertyFunctionParameter(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("EntityViewPropertyFunctionParameterListEXP");
			e.Handled = true;
		}
		private void GoToEntityViewPropertyFunctionParameterAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("EntityViewPropertyFunctionParameterAutoViewListEXP");
			e.Handled = true;
		}
		 
		private void GoToEntityViewPropertyFunction(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("EntityViewPropertyFunctionListEXP");
			e.Handled = true;
		}
		private void GoToEntityViewPropertyFunctionAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("EntityViewPropertyFunctionAutoViewListEXP");
			e.Handled = true;
		}
		 
		private void GoToEntityViewViewProperties(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("EntityViewViewPropertiesListEXP");
			e.Handled = true;
		}
		private void GoToEntityViewViewPropertiesAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("EntityViewViewPropertiesAutoViewListEXP");
			e.Handled = true;
		}
		 
		private void GoToEntityViewProperties(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("EntityViewPropertiesListEXP");
			e.Handled = true;
		}
		private void GoToEntityViewPropertiesAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("EntityViewPropertiesAutoViewListEXP");
			e.Handled = true;
		}
		 
		private void GoToEntityView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("EntityViewListEXP");
			e.Handled = true;
		}
		private void GoToEntityViewAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("EntityViewAutoViewListEXP");
			e.Handled = true;
		}
		 
		private void GoToPrimaryKeyOptions(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("PrimaryKeyOptionsListEXP");
			e.Handled = true;
		}
		private void GoToPrimaryKeyOptionsAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("PrimaryKeyOptionsAutoViewListEXP");
			e.Handled = true;
		}
		 
		private void GoToDataProperties(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("DataPropertiesListEXP");
			e.Handled = true;
		}
		private void GoToDataPropertiesAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("DataPropertiesAutoViewListEXP");
			e.Handled = true;
		}
		 
		private void GoToPresentationProperties(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("PresentationPropertiesListEXP");
			e.Handled = true;
		}
		private void GoToPresentationPropertiesAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("PresentationPropertiesAutoViewListEXP");
			e.Handled = true;
		}
		 
		private void GoToTestValues(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("TestValuesListEXP");
			e.Handled = true;
		}
		private void GoToTestValuesAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("TestValuesAutoViewListEXP");
			e.Handled = true;
		}
		 
		private void GoToEntityProperties(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("EntityPropertiesListEXP");
			e.Handled = true;
		}
		private void GoToEntityPropertiesAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("EntityPropertiesAutoViewListEXP");
			e.Handled = true;
		}
		 
		private void GoToViewEntity(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("ViewEntityListEXP");
			e.Handled = true;
		}
		private void GoToViewEntityAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("ViewEntityAutoViewListEXP");
			e.Handled = true;
		}
		 
		private void GoToEntities(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("EntitiesListEXP");
			e.Handled = true;
		}
		private void GoToEntitiesAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("EntitiesAutoViewListEXP");
			e.Handled = true;
		}
		  
	}
}
