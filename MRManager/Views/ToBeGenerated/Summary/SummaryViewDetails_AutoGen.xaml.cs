using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Views
{
	/// <summary>
	/// Interaction logic for SummaryList.xaml
	/// </summary>
	public partial class AddressesSummaryDetailsView 
	{
		public AddressesSummaryDetailsView()
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
 
		private void GoToAddressCities(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("AddressCitiesListEXP");
			e.Handled = true;
		}
		private void GoToAddressCitiesAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("AddressCitiesAutoViewListEXP");
			e.Handled = true;
		}
		 
		private void GoToAddressCountries(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("AddressCountriesListEXP");
			e.Handled = true;
		}
		private void GoToAddressCountriesAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("AddressCountriesAutoViewListEXP");
			e.Handled = true;
		}
		 
		private void GoToAddressLines(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("AddressLinesListEXP");
			e.Handled = true;
		}
		private void GoToAddressLinesAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("AddressLinesAutoViewListEXP");
			e.Handled = true;
		}
		 
		private void GoToAddressParishes(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("AddressParishesListEXP");
			e.Handled = true;
		}
		private void GoToAddressParishesAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("AddressParishesAutoViewListEXP");
			e.Handled = true;
		}
		 
		private void GoToAddressStates(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("AddressStatesListEXP");
			e.Handled = true;
		}
		private void GoToAddressStatesAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("AddressStatesAutoViewListEXP");
			e.Handled = true;
		}
		 
		private void GoToAddressZipCodes(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("AddressZipCodesListEXP");
			e.Handled = true;
		}
		private void GoToAddressZipCodesAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("AddressZipCodesAutoViewListEXP");
			e.Handled = true;
		}
		 
		private void GoToForeignAddresses(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("ForeignAddressesListEXP");
			e.Handled = true;
		}
		private void GoToForeignAddressesAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("ForeignAddressesAutoViewListEXP");
			e.Handled = true;
		}
		 
		private void GoToOrganisationAddress(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("OrganisationAddressListEXP");
			e.Handled = true;
		}
		private void GoToOrganisationAddressAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("OrganisationAddressAutoViewListEXP");
			e.Handled = true;
		}
		 
		private void GoToPrimaryPersonAddress(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("PrimaryPersonAddressListEXP");
			e.Handled = true;
		}
		private void GoToPrimaryPersonAddressAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("PrimaryPersonAddressAutoViewListEXP");
			e.Handled = true;
		}
		 
		private void GoToPersonAddresses(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("PersonAddressesListEXP");
			e.Handled = true;
		}
		private void GoToPersonAddressesAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("PersonAddressesAutoViewListEXP");
			e.Handled = true;
		}
		 
		private void GoToAddresses(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("AddressesListEXP");
			e.Handled = true;
		}
		private void GoToAddressesAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("AddressesAutoViewListEXP");
			e.Handled = true;
		}
						private void GoToAddressInfo(object sender, MouseButtonEventArgs e)
				{
					Slider.BringIntoView("AddressInfoListEXP");
					e.Handled = true;
				}
						private void GoToAddressLineInfo(object sender, MouseButtonEventArgs e)
				{
					Slider.BringIntoView("AddressLineInfoListEXP");
					e.Handled = true;
				}
		  
	}
}
