using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Core.Common.UI.DataVirtualization;
using OversShortQS.Client.Entities;
using WaterNut.QuerySpace.OversShortQS.ViewModels;

namespace WaterNut.Views
{
	/// <summary>
	/// Interaction logic for PreviousItems.xaml
	/// </summary>
    public partial class OverShortDetailAllocationsView : UserControl
	{
        public OverShortDetailAllocationsView()
		{
			this.InitializeComponent();
            im = TryFindResource("OverShortDetailAllocationsModelDataSource") as OverShortDetailsModel;
            // Insert code required on object creation below this point.
		}

	    private OverShortDetailsModel im;
       
        private void Send2Excel(object sender, MouseButtonEventArgs e)
        {

        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as TextBox).GetBindingExpression(TextBox.TextProperty).UpdateSource();
            }
        }

        private void ViewSuggestions(object sender, MouseButtonEventArgs e)
        {
            im.ViewSuggestions(((sender as FrameworkElement).DataContext as VirtualListItem<OverShortDetailsEX>).Data);
        }

        private async void MatchItem(object sender, MouseButtonEventArgs e)
        {
           await im.MatchToCurrentItem(((sender as FrameworkElement).DataContext as VirtualListItem<OverShortDetailsEX>).Data).ConfigureAwait(false);
        }

        private async void RemoveMatch(object sender, MouseButtonEventArgs e)
        {
            await im.RemoveOsMatch(((sender as FrameworkElement).DataContext as VirtualListItem<OverShortDetailsEX>).Data).ConfigureAwait(false);
        }

        private async void RemoveOSDetail(object sender, MouseButtonEventArgs e)
        {
            await im.RemoveOverShortDetail(((sender as FrameworkElement).DataContext as VirtualListItem<OverShortDetailsEX>).Data).ConfigureAwait(false);
        }

       
	}
}