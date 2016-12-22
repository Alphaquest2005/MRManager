using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Views
{
	/// <summary>
	/// Interaction logic for SummaryList.xaml
	/// </summary>
	public partial class RelationshipTypesSummaryDetailsView 
	{
		public RelationshipTypesSummaryDetailsView()
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
 
		private void GoToEntityRelationships(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("EntityRelationshipsListEXP");
			e.Handled = true;
		}
		private void GoToEntityRelationshipsAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("EntityRelationshipsAutoViewListEXP");
			e.Handled = true;
		}
		 
		private void GoToRelationshipTypes(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("RelationshipTypesListEXP");
			e.Handled = true;
		}
		private void GoToRelationshipTypesAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("RelationshipTypesAutoViewListEXP");
			e.Handled = true;
		}
		  
	}
}
