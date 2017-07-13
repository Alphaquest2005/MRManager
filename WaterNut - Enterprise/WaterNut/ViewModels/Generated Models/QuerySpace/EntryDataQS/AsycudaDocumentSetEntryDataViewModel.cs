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

using EntryDataQS.Client.Entities;
using EntryDataQS.Client.Repositories;
//using WaterNut.Client.Repositories;
        
using CoreEntities.Client.Entities;


namespace WaterNut.QuerySpace.EntryDataQS.ViewModels
{
    
	public partial class AsycudaDocumentSetEntryDataViewModel_AutoGen : ViewModelBase<AsycudaDocumentSetEntryDataViewModel_AutoGen>
	{

       private static readonly AsycudaDocumentSetEntryDataViewModel_AutoGen instance;
       static AsycudaDocumentSetEntryDataViewModel_AutoGen()
        {
            instance = new AsycudaDocumentSetEntryDataViewModel_AutoGen();
        }

       public static AsycudaDocumentSetEntryDataViewModel_AutoGen Instance
        {
            get { return instance; }
        }

       private readonly object lockObject = new object();
  
  

        public AsycudaDocumentSetEntryDataViewModel_AutoGen()
        {
          
            RegisterToReceiveMessages<AsycudaDocumentSetEntryData>(MessageToken.CurrentAsycudaDocumentSetEntryDataChanged, OnCurrentAsycudaDocumentSetEntryDataChanged);
            RegisterToReceiveMessages(MessageToken.AsycudaDocumentSetEntryDatasChanged, OnAsycudaDocumentSetEntryDatasChanged);
			RegisterToReceiveMessages(MessageToken.AsycudaDocumentSetEntryDatasFilterExpressionChanged, OnAsycudaDocumentSetEntryDatasFilterExpressionChanged);

 
			RegisterToReceiveMessages<EntryDataEx>(MessageToken.CurrentEntryDataExChanged, OnCurrentEntryDataExChanged);

 			// Recieve messages for Core Current Entities Changed
 

			AsycudaDocumentSetEntryDatas = new VirtualList<AsycudaDocumentSetEntryData>(vloader);
			AsycudaDocumentSetEntryDatas.LoadingStateChanged += AsycudaDocumentSetEntryDatas_LoadingStateChanged;
            BindingOperations.EnableCollectionSynchronization(AsycudaDocumentSetEntryDatas, lockObject);
			
            OnCreated();        
            OnTotals();
        }

        partial void OnCreated();
        partial void OnTotals();

		private VirtualList<AsycudaDocumentSetEntryData> _AsycudaDocumentSetEntryDatas = null;
        public VirtualList<AsycudaDocumentSetEntryData> AsycudaDocumentSetEntryDatas
        {
            get
            {
                return _AsycudaDocumentSetEntryDatas;
            }
            set
            {
                _AsycudaDocumentSetEntryDatas = value;
            }
        }

		 private void OnAsycudaDocumentSetEntryDatasFilterExpressionChanged(object sender, NotificationEventArgs e)
        {
			AsycudaDocumentSetEntryDatas.Refresh();
            SelectedAsycudaDocumentSetEntryDatas.Clear();
            NotifyPropertyChanged(x => SelectedAsycudaDocumentSetEntryDatas);
            BeginSendMessage(MessageToken.SelectedAsycudaDocumentSetEntryDatasChanged, new NotificationEventArgs(MessageToken.SelectedAsycudaDocumentSetEntryDatasChanged));
        }

		void AsycudaDocumentSetEntryDatas_LoadingStateChanged(object sender, EventArgs e)
        {
            switch (AsycudaDocumentSetEntryDatas.LoadingState)
            {
                case QueuedBackgroundWorkerState.Processing:
                    StatusModel.Timer("Getting Data...");
                    break;
                case QueuedBackgroundWorkerState.Standby: 
                    StatusModel.StopStatusUpdate();
                    NotifyPropertyChanged(x => AsycudaDocumentSetEntryDatas);
                    break;
                case QueuedBackgroundWorkerState.StoppedByError:
                    StatusModel.Error("AsycudaDocumentSetEntryDatas | Error occured..." + AsycudaDocumentSetEntryDatas.LastLoadingError.Message);
                    NotifyPropertyChanged(x => AsycudaDocumentSetEntryDatas);
                    break;
            }
           
        }

		
		public readonly AsycudaDocumentSetEntryDataVirturalListLoader vloader = new AsycudaDocumentSetEntryDataVirturalListLoader();

		private ObservableCollection<AsycudaDocumentSetEntryData> _selectedAsycudaDocumentSetEntryDatas = new ObservableCollection<AsycudaDocumentSetEntryData>();
        public ObservableCollection<AsycudaDocumentSetEntryData> SelectedAsycudaDocumentSetEntryDatas
        {
            get
            {
                return _selectedAsycudaDocumentSetEntryDatas;
            }
            set
            {
                _selectedAsycudaDocumentSetEntryDatas = value;
				BeginSendMessage(MessageToken.SelectedAsycudaDocumentSetEntryDatasChanged,
                                    new NotificationEventArgs(MessageToken.SelectedAsycudaDocumentSetEntryDatasChanged));
				 NotifyPropertyChanged(x => SelectedAsycudaDocumentSetEntryDatas);
            }
        }

        internal void OnCurrentAsycudaDocumentSetEntryDataChanged(object sender, NotificationEventArgs<AsycudaDocumentSetEntryData> e)
        {
            if(BaseViewModel.Instance.CurrentAsycudaDocumentSetEntryData != null) BaseViewModel.Instance.CurrentAsycudaDocumentSetEntryData.PropertyChanged += CurrentAsycudaDocumentSetEntryData__propertyChanged;
           // NotifyPropertyChanged(x => this.CurrentAsycudaDocumentSetEntryData);
        }   

            void CurrentAsycudaDocumentSetEntryData__propertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
                {
                    //if (e.PropertyName == "AddEntryDataEx")
                   // {
                   //    if(EntryDataEx.Contains(CurrentAsycudaDocumentSetEntryData.EntryDataEx) == false) EntryDataEx.Add(CurrentAsycudaDocumentSetEntryData.EntryDataEx);
                    //}
                 } 
        internal void OnAsycudaDocumentSetEntryDatasChanged(object sender, NotificationEventArgs e)
        {
            _AsycudaDocumentSetEntryDatas.Refresh();
			NotifyPropertyChanged(x => this.AsycudaDocumentSetEntryDatas);
        }   


 	
		 internal void OnCurrentEntryDataExChanged(object sender, SimpleMvvmToolkit.NotificationEventArgs<EntryDataEx> e)
			{
			if(ViewCurrentEntryDataEx == false) return;
			if (e.Data == null || e.Data.InvoiceNo == null)
                {
                    vloader.FilterExpression = "None";
                }
                else
                {
				
				vloader.FilterExpression = string.Format("EntryDataId == \"{0}\"", e.Data.InvoiceNo.ToString());
                }

				AsycudaDocumentSetEntryDatas.Refresh();
				NotifyPropertyChanged(x => this.AsycudaDocumentSetEntryDatas);
                // SendMessage(MessageToken.AsycudaDocumentSetEntryDatasChanged, new NotificationEventArgs(MessageToken.AsycudaDocumentSetEntryDatasChanged));
                			}

  			// Core Current Entities Changed
			// theorticall don't need this cuz i am inheriting from core entities baseview model so changes should flow up to here
  
// Filtering Each Field except IDs
 	
		 bool _viewCurrentEntryDataEx = true;
         public bool ViewCurrentEntryDataEx
         {
             get
             {
                 return _viewCurrentEntryDataEx;
             }
             set
             {
                 _viewCurrentEntryDataEx = value;
                 NotifyPropertyChanged(x => x.ViewCurrentEntryDataEx);
             }
         }
		public void ViewAll()
        {
			vloader.FilterExpression = "All";
			vloader.ClearNavigationExpression();
			_AsycudaDocumentSetEntryDatas.Refresh();
			NotifyPropertyChanged(x => this.AsycudaDocumentSetEntryDatas);
		}

		public async Task SelectAll()
        {
            IEnumerable<AsycudaDocumentSetEntryData> lst = null;
            using (var ctx = new AsycudaDocumentSetEntryDataRepository())
            {
                lst = await ctx.GetAsycudaDocumentSetEntryDatasByExpressionNav(vloader.FilterExpression, vloader.NavigationExpression).ConfigureAwait(continueOnCapturedContext: false);
            }
            SelectedAsycudaDocumentSetEntryDatas = new ObservableCollection<AsycudaDocumentSetEntryData>(lst);
        }

 

		private string _entryDataIdFilter;
        public string EntryDataIdFilter
        {
            get
            {
                return _entryDataIdFilter;
            }
            set
            {
                _entryDataIdFilter = value;
				NotifyPropertyChanged(x => EntryDataIdFilter);
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

			AsycudaDocumentSetEntryDatas.Refresh();
			NotifyPropertyChanged(x => this.AsycudaDocumentSetEntryDatas);
		}		  



		internal virtual StringBuilder GetAutoPropertyFilterString()
		{
		var res = new StringBuilder();
 

									if(string.IsNullOrEmpty(EntryDataIdFilter) == false)
						res.Append(" && " + string.Format("EntryDataId.Contains(\"{0}\")",  EntryDataIdFilter));						
			return res.ToString().StartsWith(" &&") || res.Length == 0 ? res:  res.Insert(0," && ");		
		}

// Send to Excel Implementation


        public async Task Send2Excel()
        {
			IEnumerable<AsycudaDocumentSetEntryData> lst = null;
            using (var ctx = new AsycudaDocumentSetEntryDataRepository())
            {
                lst = await ctx.GetAsycudaDocumentSetEntryDatasByExpressionNav(vloader.FilterExpression, vloader.NavigationExpression).ConfigureAwait(continueOnCapturedContext: false);
            }
             if (lst == null || !lst.Any()) 
              {
                   MessageBox.Show("No Data to Send to Excel");
                   return;
               }
            var s = new ExportToExcel<AsycudaDocumentSetEntryDataExcelLine, List<AsycudaDocumentSetEntryDataExcelLine>>
            {
                dataToPrint = lst.Select(x => new AsycudaDocumentSetEntryDataExcelLine
                {
 
                    EntryDataId = x.EntryDataId 
                    
                }).ToList()
            };
            using (var sta = new StaTaskScheduler(numberOfThreads: 1))
            {
                await Task.Factory.StartNew(s.GenerateReport, CancellationToken.None, TaskCreationOptions.None, sta).ConfigureAwait(false);
            }
        }

        public class AsycudaDocumentSetEntryDataExcelLine
        {
		 
                    public string EntryDataId { get; set; } 
                    
        }

		
    }
}
		