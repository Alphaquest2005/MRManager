using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.ComponentModel;
using System.Linq;
using System.Data.Entity;
using System.Collections.ObjectModel;
using PreviousDocumentQS.Client.Entities;
using PreviousDocumentQS.Client.Repositories;
using AllocationQS.Client.Entities;
using SimpleMvvmToolkit;

namespace WaterNut.QuerySpace.PreviousDocumentQS.ViewModels
{
    public partial class PreviousDocumentItemsModel : PreviousDocumentItemViewModel_AutoGen
	{
         private static readonly PreviousDocumentItemsModel instance;
         static PreviousDocumentItemsModel()
        {
            instance = new PreviousDocumentItemsModel()
            {
                EntryTimeStampFilter= DateTime.MinValue,
                StartEntryTimeStampFilter = DateTime.MinValue,
                EndEntryTimeStampFilter= DateTime.MinValue,
                ViewCurrentPreviousDocument = true
                
            };
        }

        public static PreviousDocumentItemsModel Instance
        {
            get { return instance; }
        }
        private PreviousDocumentItemsModel()
		{
            RegisterToReceiveMessages<AsycudaSalesAllocationsEx>(AllocationQS.MessageToken.CurrentAsycudaSalesAllocationsExChanged, OnCurrentAsycudaSalesAllocationsExChanged);
		}

        private void OnCurrentAsycudaSalesAllocationsExChanged(object sender, NotificationEventArgs<AsycudaSalesAllocationsEx> e)
        {
            vloader.FilterExpression = string.Format("Item_Id = {0}", e.Data.PreviousItem_Id.ToString());
            PreviousDocumentItems.Refresh();
        }

        bool _manualMode = false;
        public bool ManualMode
        {
            get
            {
                return _manualMode;
            }
            set
            {
                _manualMode = value;
            }
        }


        internal async Task RemoveItem(int p)
        {
            throw new NotImplementedException();
        }

        internal async Task RemoveSelectedItems(List<PreviousDocumentItem> list)
        {
            throw new NotImplementedException();
        }

        internal async Task SavePreviousDocumentItem(PreviousDocumentItem previousDocumentItem)
        {
            throw new NotImplementedException();
        }
    }
}