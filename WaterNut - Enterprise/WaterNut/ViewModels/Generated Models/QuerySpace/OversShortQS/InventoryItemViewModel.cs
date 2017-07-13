﻿// <autogenerated>
//   This file was generated by T4 code generator AllQuerySpaceViewModels.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Schedulers;
using SimpleMvvmToolkit;
using System;
using System.Windows;
using System.Windows.Data;
using System.Text;
using Core.Common.UI.DataVirtualization;
using System.Collections.Generic;
using Core.Common.UI;
using Core.Common.Converters;

using OversShortQS.Client.Entities;
using OversShortQS.Client.Repositories;
//using WaterNut.Client.Repositories;


namespace WaterNut.QuerySpace.OversShortQS.ViewModels
{
    
	public partial class InventoryItemViewModel_AutoGen : ViewModelBase<InventoryItemViewModel_AutoGen>
	{

       private static readonly InventoryItemViewModel_AutoGen instance;
       static InventoryItemViewModel_AutoGen()
        {
            instance = new InventoryItemViewModel_AutoGen();
        }

       public static InventoryItemViewModel_AutoGen Instance
        {
            get { return instance; }
        }

       private readonly object lockObject = new object();
  
  

        public InventoryItemViewModel_AutoGen()
        {
          
            RegisterToReceiveMessages<InventoryItem>(MessageToken.CurrentInventoryItemChanged, OnCurrentInventoryItemChanged);
            RegisterToReceiveMessages(MessageToken.InventoryItemsChanged, OnInventoryItemsChanged);
			RegisterToReceiveMessages(MessageToken.InventoryItemsFilterExpressionChanged, OnInventoryItemsFilterExpressionChanged);


 			// Recieve messages for Core Current Entities Changed
 

			InventoryItems = new VirtualList<InventoryItem>(vloader);
			InventoryItems.LoadingStateChanged += InventoryItems_LoadingStateChanged;
            BindingOperations.EnableCollectionSynchronization(InventoryItems, lockObject);
			
            OnCreated();        
            OnTotals();
        }

        partial void OnCreated();
        partial void OnTotals();

		private VirtualList<InventoryItem> _InventoryItems = null;
        public VirtualList<InventoryItem> InventoryItems
        {
            get
            {
                return _InventoryItems;
            }
            set
            {
                _InventoryItems = value;
            }
        }

		 private void OnInventoryItemsFilterExpressionChanged(object sender, NotificationEventArgs e)
        {
			InventoryItems.Refresh();
            SelectedInventoryItems.Clear();
            NotifyPropertyChanged(x => SelectedInventoryItems);
            BeginSendMessage(MessageToken.SelectedInventoryItemsChanged, new NotificationEventArgs(MessageToken.SelectedInventoryItemsChanged));
        }

		void InventoryItems_LoadingStateChanged(object sender, EventArgs e)
        {
            switch (InventoryItems.LoadingState)
            {
                case QueuedBackgroundWorkerState.Processing:
                    StatusModel.Timer("Getting Data...");
                    break;
                case QueuedBackgroundWorkerState.Standby: 
                    StatusModel.StopStatusUpdate();
                    NotifyPropertyChanged(x => InventoryItems);
                    break;
                case QueuedBackgroundWorkerState.StoppedByError:
                    StatusModel.Error("InventoryItems | Error occured..." + InventoryItems.LastLoadingError.Message);
                    NotifyPropertyChanged(x => InventoryItems);
                    break;
            }
           
        }

		
		public readonly InventoryItemVirturalListLoader vloader = new InventoryItemVirturalListLoader();

		private ObservableCollection<InventoryItem> _selectedInventoryItems = new ObservableCollection<InventoryItem>();
        public ObservableCollection<InventoryItem> SelectedInventoryItems
        {
            get
            {
                return _selectedInventoryItems;
            }
            set
            {
                _selectedInventoryItems = value;
				BeginSendMessage(MessageToken.SelectedInventoryItemsChanged,
                                    new NotificationEventArgs(MessageToken.SelectedInventoryItemsChanged));
				 NotifyPropertyChanged(x => SelectedInventoryItems);
            }
        }

        internal void OnCurrentInventoryItemChanged(object sender, NotificationEventArgs<InventoryItem> e)
        {
            if(BaseViewModel.Instance.CurrentInventoryItem != null) BaseViewModel.Instance.CurrentInventoryItem.PropertyChanged += CurrentInventoryItem__propertyChanged;
           // NotifyPropertyChanged(x => this.CurrentInventoryItem);
        }   

            void CurrentInventoryItem__propertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
                {
                 } 
        internal void OnInventoryItemsChanged(object sender, NotificationEventArgs e)
        {
            _InventoryItems.Refresh();
			NotifyPropertyChanged(x => this.InventoryItems);
        }   


 
  			// Core Current Entities Changed
			// theorticall don't need this cuz i am inheriting from core entities baseview model so changes should flow up to here
  
// Filtering Each Field except IDs
		public void ViewAll()
        {
			vloader.FilterExpression = "All";
			vloader.ClearNavigationExpression();
			_InventoryItems.Refresh();
			NotifyPropertyChanged(x => this.InventoryItems);
		}

		public async Task SelectAll()
        {
            IEnumerable<InventoryItem> lst = null;
            using (var ctx = new InventoryItemRepository())
            {
                lst = await ctx.GetInventoryItemsByExpressionNav(vloader.FilterExpression, vloader.NavigationExpression).ConfigureAwait(continueOnCapturedContext: false);
            }
            SelectedInventoryItems = new ObservableCollection<InventoryItem>(lst);
        }

 

		private string _itemNumberFilter;
        public string ItemNumberFilter
        {
            get
            {
                return _itemNumberFilter;
            }
            set
            {
                _itemNumberFilter = value;
				NotifyPropertyChanged(x => ItemNumberFilter);
                FilterData();
                
            }
        }	

 

		private string _descriptionFilter;
        public string DescriptionFilter
        {
            get
            {
                return _descriptionFilter;
            }
            set
            {
                _descriptionFilter = value;
				NotifyPropertyChanged(x => DescriptionFilter);
                FilterData();
                
            }
        }	

 

		private string _categoryFilter;
        public string CategoryFilter
        {
            get
            {
                return _categoryFilter;
            }
            set
            {
                _categoryFilter = value;
				NotifyPropertyChanged(x => CategoryFilter);
                FilterData();
                
            }
        }	

 

		private string _tariffCodeFilter;
        public string TariffCodeFilter
        {
            get
            {
                return _tariffCodeFilter;
            }
            set
            {
                _tariffCodeFilter = value;
				NotifyPropertyChanged(x => TariffCodeFilter);
                FilterData();
                
            }
        }	

 
		private DateTime? _startEntryTimeStampFilter = DateTime.Parse(string.Format("{0}/1/{1}", DateTime.Now.Month ,DateTime.Now.Year));
        public DateTime? StartEntryTimeStampFilter
        {
            get
            {
                return _startEntryTimeStampFilter;
            }
            set
            {
                _startEntryTimeStampFilter = value;
				NotifyPropertyChanged(x => StartEntryTimeStampFilter);
                FilterData();
                
            }
        }	

		private DateTime? _endEntryTimeStampFilter = DateTime.Parse(string.Format("{1}/{0}/{2}", DateTime.DaysInMonth( DateTime.Now.Year,DateTime.Now.Month), DateTime.Now.Month, DateTime.Now.Year));
        public DateTime? EndEntryTimeStampFilter
        {
            get
            {
                return _endEntryTimeStampFilter;
            }
            set
            {
                _endEntryTimeStampFilter = value;
				NotifyPropertyChanged(x => EndEntryTimeStampFilter);
                FilterData();
                
            }
        }
 

		private DateTime? _entryTimeStampFilter;
        public DateTime? EntryTimeStampFilter
        {
            get
            {
                return _entryTimeStampFilter;
            }
            set
            {
                _entryTimeStampFilter = value;
				NotifyPropertyChanged(x => EntryTimeStampFilter);
                FilterData();
                
            }
        }	

 
		internal bool DisableBaseFilterData = false;
        public virtual void FilterData()
	    {
	        FilterData(null);
	    }
		public void FilterData(StringBuilder res = null)
		{
		    if (DisableBaseFilterData) return;
			if(res == null) res = GetAutoPropertyFilterString();
			if (res.Length == 0 && vloader.NavigationExpression.Count != 0) res.Append("&& All");					
			if (res.Length > 0)
            {
                vloader.FilterExpression = res.ToString().Trim().Substring(2).Trim();
            }
            else
            {
                 if (vloader.FilterExpression != "All") vloader.FilterExpression = null;
            }

			InventoryItems.Refresh();
			NotifyPropertyChanged(x => this.InventoryItems);
		}		  



		internal virtual StringBuilder GetAutoPropertyFilterString()
		{
		var res = new StringBuilder();
 

									if(string.IsNullOrEmpty(ItemNumberFilter) == false)
						res.Append(" && " + string.Format("ItemNumber.Contains(\"{0}\")",  ItemNumberFilter));						
 

									if(string.IsNullOrEmpty(DescriptionFilter) == false)
						res.Append(" && " + string.Format("Description.Contains(\"{0}\")",  DescriptionFilter));						
 

									if(string.IsNullOrEmpty(CategoryFilter) == false)
						res.Append(" && " + string.Format("Category.Contains(\"{0}\")",  CategoryFilter));						
 

									if(string.IsNullOrEmpty(TariffCodeFilter) == false)
						res.Append(" && " + string.Format("TariffCode.Contains(\"{0}\")",  TariffCodeFilter));						
 

 

				if (Convert.ToDateTime(StartEntryTimeStampFilter).Date != DateTime.MinValue &&
		        Convert.ToDateTime(EndEntryTimeStampFilter).Date != DateTime.MinValue) res.Append(" && (");

					if (Convert.ToDateTime(StartEntryTimeStampFilter).Date != DateTime.MinValue)
						{
							if(StartEntryTimeStampFilter.HasValue)
								res.Append(
                                            (Convert.ToDateTime(EndEntryTimeStampFilter).Date != DateTime.MinValue?"":" && ") +
                                            string.Format("EntryTimeStamp >= \"{0}\"",  Convert.ToDateTime(StartEntryTimeStampFilter).Date.ToString("MM/dd/yyyy")));
						}

					if (Convert.ToDateTime(EndEntryTimeStampFilter).Date != DateTime.MinValue)
						{
							if(EndEntryTimeStampFilter.HasValue)
								res.Append(" && " + string.Format("EntryTimeStamp <= \"{0}\"",  Convert.ToDateTime(EndEntryTimeStampFilter).Date.AddHours(23).ToString("MM/dd/yyyy")));
						}

				if (Convert.ToDateTime(StartEntryTimeStampFilter).Date != DateTime.MinValue &&
		        Convert.ToDateTime(EndEntryTimeStampFilter).Date != DateTime.MinValue) res.Append(" )");

					if (Convert.ToDateTime(_entryTimeStampFilter).Date != DateTime.MinValue)
						{
							if(EntryTimeStampFilter.HasValue)
								res.Append(" && " + string.Format("EntryTimeStamp == \"{0}\"",  Convert.ToDateTime(EntryTimeStampFilter).Date.ToString("MM/dd/yyyy")));
						}
							return res.ToString().StartsWith(" &&") || res.Length == 0 ? res:  res.Insert(0," && ");		
		}

// Send to Excel Implementation


        public async Task Send2Excel()
        {
			IEnumerable<InventoryItem> lst = null;
            using (var ctx = new InventoryItemRepository())
            {
                lst = await ctx.GetInventoryItemsByExpressionNav(vloader.FilterExpression, vloader.NavigationExpression).ConfigureAwait(continueOnCapturedContext: false);
            }
             if (lst == null || !lst.Any()) 
              {
                   MessageBox.Show("No Data to Send to Excel");
                   return;
               }
            var s = new ExportToExcel<InventoryItemExcelLine, List<InventoryItemExcelLine>>
            {
                dataToPrint = lst.Select(x => new InventoryItemExcelLine
                {
 
                    ItemNumber = x.ItemNumber ,
                    
 
                    Description = x.Description ,
                    
 
                    Category = x.Category ,
                    
 
                    TariffCode = x.TariffCode ,
                    
 
                    EntryTimeStamp = x.EntryTimeStamp 
                    
                }).ToList()
            };
            using (var sta = new StaTaskScheduler(numberOfThreads: 1))
            {
                await Task.Factory.StartNew(s.GenerateReport, CancellationToken.None, TaskCreationOptions.None, sta).ConfigureAwait(false);
            }
        }

        public class InventoryItemExcelLine
        {
		 
                    public string ItemNumber { get; set; } 
                    
 
                    public string Description { get; set; } 
                    
 
                    public string Category { get; set; } 
                    
 
                    public string TariffCode { get; set; } 
                    
 
                    public Nullable<System.DateTime> EntryTimeStamp { get; set; } 
                    
        }

		
    }
}
		