using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Views
{
	/// <summary>
	/// Interaction logic for SummaryList.xaml
	/// </summary>
	public partial class DataTypesSummaryDetailsView 
	{
		public DataTypesSummaryDetailsView()
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
		 
		private void GoToFunctionParameters(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("FunctionParametersListEXP");
			e.Handled = true;
		}
		private void GoToFunctionParametersAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("FunctionParametersAutoViewListEXP");
			e.Handled = true;
		}
		 
		private void GoToParameters(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("ParametersListEXP");
			e.Handled = true;
		}
		private void GoToParametersAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("ParametersAutoViewListEXP");
			e.Handled = true;
		}
		 
		private void GoToFunctionReturnType(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("FunctionReturnTypeListEXP");
			e.Handled = true;
		}
		private void GoToFunctionReturnTypeAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("FunctionReturnTypeAutoViewListEXP");
			e.Handled = true;
		}
		 
		private void GoToDataTypes(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("DataTypesListEXP");
			e.Handled = true;
		}
		private void GoToDataTypesAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("DataTypesAutoViewListEXP");
			e.Handled = true;
		}
		  
	}
}
