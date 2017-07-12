using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AllocationQS.Client.Entities;
using CoreEntities.Client.Repositories;
using OversShortQS.Client.Entities;
using OversShortQS.Client.Repositories;
using SimpleMvvmToolkit;
using WaterNut.QuerySpace.OversShortQS.ViewModels;
using AsycudaDocumentItem = OversShortQS.Client.Entities.AsycudaDocumentItem;
using AsycudaDocumentItemRepository = OversShortQS.Client.Repositories.AsycudaDocumentItemRepository;

namespace WaterNut.QuerySpace.OversShortQS.ViewModels
{
    public class OverShortDetailsModel : OverShortDetailsEXViewModel_AutoGen
    {
        private static readonly OverShortDetailsModel instance;
        static OverShortDetailsModel()
        {
            instance = new OverShortDetailsModel(){ViewCurrentOversShortEX = true};
        }
        public static  OverShortDetailsModel Instance
        {
            get { return instance; }
        }

        bool _viewErrors = false;
        public bool ViewErrors
        {
            get
            {
                return _viewErrors;
            }
            set
            {
                _viewErrors = value;
                FilterData();
                NotifyPropertyChanged(x => this.ViewErrors);
            }
        }



        bool _viewMatches = false;
        public bool ViewMatches
        {
            get
            {
                return _viewMatches;
            }
            set
            {
                _viewMatches = value;
                FilterData();
                NotifyPropertyChanged(x => this.ViewMatches);
            }
        }



        bool _viewSelected = false;
        public bool ViewSelected
        {
            get
            {
                return _viewSelected;
            }
            set
            {
                _viewSelected = value;
                FilterData();
                NotifyPropertyChanged(x => this.ViewSelected);
            }
        }



        bool _viewOvers = false;
        public bool ViewOvers
        {
            get
            {
                return _viewOvers;
            }
            set
            {
                _viewOvers = value;
                FilterData();
                NotifyPropertyChanged(x => this.ViewOvers);
            }
        }



        bool _viewShorts = false;
        public bool ViewShorts
        {
            get
            {
                return _viewShorts;
            }
            set
            {
                _viewShorts = value;
                FilterData();
                NotifyPropertyChanged( x => this.ViewShorts);
            }
        }


        bool _viewBadMatches = false;
        public bool ViewBadMatches
        {
            get
            {
                return _viewBadMatches;
            }
            set
            {
                _viewBadMatches = value;
                FilterData();
                NotifyPropertyChanged(x => this.ViewBadMatches);
            }
        }



        private async Task<IEnumerable<OverShortDetailsEX>> GetBadMatchLst()
        {
            using (var ctx = new OverShortDetailsEXRepository())
            {
                var lst = await ctx.GetOverShortDetailsByExpressionNav("All",
                    new Dictionary<string, string>()
                    {
                        {
                            "EX", "(Duration > 15) || (AsycudaMonth != InvoiceMonth)"
                        }
                    }).ConfigureAwait(false);

                return lst;
            }
        }

    

        internal void ViewSuggestions(OverShortDetailsEX osd)
        {
            QuerySpace.CoreEntities.ViewModels.AsycudaDocumentItemsModel.Instance.vloader.FilterExpression =
                string.Format("ItemNumber == \"{0}\" && DocumentType == \"IM7\"", osd.ItemNumber);

            QuerySpace.CoreEntities.ViewModels.AsycudaDocumentItemsModel.Instance.AsycudaDocumentItems.OrderBy(
                x => x.Data.AsycudaDocument.RegistrationDate - osd.OversShortEX.InvoiceDate);

           // OnStaticPropertyChanged("OSSuggestedAsycudaItemEntry");
        }

        internal async Task RemoveOsMatch(OverShortDetailsEX osd)
        {
           osd.OverShortDetailAllocations.Clear();
           osd.Status = "";
            using (var ctx = new OverShortDetailRepository())
            {
                await ctx.UpdateOverShortDetail(osd).ConfigureAwait(false);
            }
            OverShortDetails.Refresh();
        }



        public override void FilterData()
        {
            var res = GetAutoPropertyFilterString();
            if (_viewShorts)
            {
               res.Append(@" && InvoiceQty > ReceivedQty");
            }
             if (_viewOvers)
            {
                res.Append(@" && InvoiceQty < ReceivedQty");
            }
            if (_viewErrors)
            {
                res.Append(@" && Status != null");
            }
            if (_viewMatches)
            {
                res.Append(@" && OverShortDetailAllocations.Any()");
            }

            if (_viewSelected)
            {
                var lst = OversShortsModel.Instance.SelectedOversShorts.ToList();
                if (lst.Any())
                {
                    var slst = BuildOSLst(lst);
                    //remove comma
                    
                    res.Append(slst);
                }
            }

            if (_viewBadMatches)
            {
                //vloader.SetNavigationExpression("EX");
                res.Append(@" && EX.Duration > 15 && EX.InvoiceMonth != EX.AsycudaMonth");

            }

            FilterData(res);

        }

        private async Task BuildOSLst(List<OversShortEX> lst)
        {
            await OversShortEXRepository.Instance.BuildOSLst(lst.Select(x => x.OversShortsId).ToList()).ConfigureAwait(false);
        }




        internal async Task MatchToCurrentItem(OverShortDetailsEX overShortDetailsEX)
        {
            if (CoreEntities.ViewModels.BaseViewModel.Instance.CurrentAsycudaDocumentItem == null)
            {
                MessageBox.Show("Please Select Asycuda Item");
                return;
            }

            await OverShortDetailsEXRepository.Instance.MatchToCurrentItem(
                CoreEntities.ViewModels.BaseViewModel.Instance.CurrentAsycudaDocumentItem.Item_Id, overShortDetailsEX)
                .ConfigureAwait(false);

            MessageBus.Default.BeginNotify(MessageToken.CurrentOverShortDetailsEXChanged, this,
              new NotificationEventArgs<OverShortDetailsEX>(
                  QuerySpace.OversShortQS.MessageToken.CurrentOverShortDetailsEXChanged, overShortDetailsEX));
        }

        internal async Task RemoveOverShortDetail(OverShortDetailsEX overShortDetailsEX)
        {
             MessageBoxResult res = MessageBox.Show("Are you sure you want to delete this OverShort Detail?",
                "Delete selected Items", MessageBoxButton.YesNo);
            if (res == MessageBoxResult.Yes)
            {
                await OverShortDetailsEXRepository.Instance.RemoveOverShortDetail(overShortDetailsEX.OverShortDetailId).ConfigureAwait(false);

                MessageBus.Default.BeginNotify(MessageToken.OverShortDetailsChanged, this,
                    new NotificationEventArgs(MessageToken.OverShortDetailsChanged));
                BaseViewModel.Instance.CurrentOverShortDetail = null;

            }
        }
    }
}