using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.ComponentModel;
using AllocationQS.Client.Entities;
using PreviousDocumentQS.Client.Repositories;
using PreviousDocumentQS.Client.Entities;
using SimpleMvvmToolkit;
using WaterNut.Views;

namespace WaterNut.QuerySpace.PreviousDocumentQS.ViewModels
{
    public partial class PreviousItemsViewModel : ViewModelBase<PreviousItemsViewModel>
    {
         private static readonly PreviousItemsViewModel instance;
         static PreviousItemsViewModel()
        {
            instance = new PreviousItemsViewModel();
        }

         public static PreviousItemsViewModel Instance
        {
            get { return instance; }
        }
        private PreviousItemsViewModel()
		{
            RegisterToReceiveMessages<AsycudaSalesAllocationsEx>(AllocationQS.MessageToken.CurrentAsycudaSalesAllocationsExChanged,
                                                                    OnCurrentAsycudaSalesAllocationsExChanged);
            RegisterToReceiveMessages<PreviousDocumentItem>(MessageToken.CurrentPreviousDocumentItemChanged, OnCurrentPreviousDocumentItemChanged);
            RegisterToReceiveMessages<global::CoreEntities.Client.Entities.AsycudaDocumentItem>(CoreEntities.MessageToken.CurrentAsycudaDocumentItemChanged, OnCurrentAsycudaDocumentItemChanged);
		}

        private async void OnCurrentAsycudaDocumentItemChanged(object sender, NotificationEventArgs<global::CoreEntities.Client.Entities.AsycudaDocumentItem> e)
        {
            if (e.Data == null)
            {
                PreviousItems = new List<PreviousItemsEx>();
            }
            else
            {
                using (var ctx = new PreviousItemsExRepository())
                {
                    PreviousItems =
                        (await ctx.GetPreviousItemsExesByExpression(string.Format("AsycudaDocumentItemId == {0}", e.Data.Item_Id),//Item_Id
                                                            new List<string>()
                                                            {
                                                                "PreviousDocumentItem.PreviousDocument",//
                                                               // "AsycudaDocumentItem"
                                                            }).ConfigureAwait(false)).ToList();
                }
            }
        }

        private async void OnCurrentPreviousDocumentItemChanged(object sender, NotificationEventArgs<PreviousDocumentItem> e)
        {
            if (e.Data == null)
            {
                PreviousItems = new List<PreviousItemsEx>();
            }
            else
            {
                using (var ctx = new PreviousItemsExRepository())
                {
                    PreviousItems =
                        (await ctx.GetPreviousItemsExesByExpression(string.Format("PreviousDocumentItemId == {0}", e.Data.Item_Id),//Item_Id
                                                            new List<string>()
                                                            {
                                                                "PreviousDocumentItem.PreviousDocument",//
                                                               // "AsycudaDocumentItem"
                                                            }).ConfigureAwait(false)).ToList();
                }
            }

        }

        List<PreviousItemsEx> _previousItems = new List<PreviousItemsEx>();

        public List<PreviousItemsEx> PreviousItems
        {
            get { return _previousItems; }
            set
            {
                _previousItems = value;
                NotifyPropertyChanged(x => x.PreviousItems);
            }
        }

        private async void OnCurrentAsycudaSalesAllocationsExChanged(object sender, NotificationEventArgs<AsycudaSalesAllocationsEx> e)
        {
            if (e.Data.PreviousItem_Id != null)
            {
                using (var ctx = new PreviousItemsExRepository())
                {

                    PreviousItems =
                        (await ctx.GetPreviousItemsExesByExpression(string.Format("PreviousDocumentItemId == {0}", e.Data.PreviousItem_Id)).ConfigureAwait(false))
                            .ToList();
                }
            }
        }

        private string _prev_reg_nbrFilter;
        public string Prev_reg_nbrFilter
        {
            get
            {
                return _prev_reg_nbrFilter;
            }
            set
            {
                _prev_reg_nbrFilter = value;
                NotifyPropertyChanged(x => Prev_reg_nbrFilter);
                FilterData();

            }
        }

        private string _previous_item_numberFilter;
        public string Previous_item_numberFilter
        {
            get
            {
                return _previous_item_numberFilter;
            }
            set
            {
                _previous_item_numberFilter = value;
                NotifyPropertyChanged(x => Previous_item_numberFilter);
                FilterData();

            }
        }	

        private  void FilterData()
        {
            var res = new StringBuilder();
            if (!string.IsNullOrEmpty(Prev_reg_nbrFilter))
            {
                res.Append(string.Format("&& Prev_reg_nbr == \"{0}\"", Prev_reg_nbrFilter));
            }

            if (!string.IsNullOrEmpty(Previous_item_numberFilter))
            {
                res.Append(string.Format("&& Previous_item_number == \"{0}\"", Previous_item_numberFilter));
            }

            if (res.Length > 3)
            {
                using (var ctx = new PreviousItemsExRepository())
                {
                    PreviousItems =
                        (ctx.GetPreviousItemsExesByExpression(res.ToString().Substring(3), //Item_Id
                            new List<string>()
                            {
                                "PreviousDocumentItem.PreviousDocument", //
                                // "AsycudaDocumentItem"
                            })).Result.ToList();
                }
            }
        }

	}
}