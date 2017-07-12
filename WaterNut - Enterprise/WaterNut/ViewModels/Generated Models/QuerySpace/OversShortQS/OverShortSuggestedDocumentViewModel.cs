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
    
	public partial class OverShortSuggestedDocumentViewModel_AutoGen : ViewModelBase<OverShortSuggestedDocumentViewModel_AutoGen>
	{

       private static readonly OverShortSuggestedDocumentViewModel_AutoGen instance;
       static OverShortSuggestedDocumentViewModel_AutoGen()
        {
            instance = new OverShortSuggestedDocumentViewModel_AutoGen();
        }

       public static OverShortSuggestedDocumentViewModel_AutoGen Instance
        {
            get { return instance; }
        }

       private readonly object lockObject = new object();
  
  

        public OverShortSuggestedDocumentViewModel_AutoGen()
        {
          
            RegisterToReceiveMessages<OverShortSuggestedDocument>(MessageToken.CurrentOverShortSuggestedDocumentChanged, OnCurrentOverShortSuggestedDocumentChanged);
            RegisterToReceiveMessages(MessageToken.OverShortSuggestedDocumentsChanged, OnOverShortSuggestedDocumentsChanged);
			RegisterToReceiveMessages(MessageToken.OverShortSuggestedDocumentsFilterExpressionChanged, OnOverShortSuggestedDocumentsFilterExpressionChanged);

 
			RegisterToReceiveMessages<OversShortEX>(MessageToken.CurrentOversShortEXChanged, OnCurrentOversShortEXChanged);

 			// Recieve messages for Core Current Entities Changed
 

			OverShortSuggestedDocuments = new VirtualList<OverShortSuggestedDocument>(vloader);
			OverShortSuggestedDocuments.LoadingStateChanged += OverShortSuggestedDocuments_LoadingStateChanged;
            BindingOperations.EnableCollectionSynchronization(OverShortSuggestedDocuments, lockObject);
			
            OnCreated();        
            OnTotals();
        }

        partial void OnCreated();
        partial void OnTotals();

		private VirtualList<OverShortSuggestedDocument> _OverShortSuggestedDocuments = null;
        public VirtualList<OverShortSuggestedDocument> OverShortSuggestedDocuments
        {
            get
            {
                return _OverShortSuggestedDocuments;
            }
            set
            {
                _OverShortSuggestedDocuments = value;
            }
        }

		 private void OnOverShortSuggestedDocumentsFilterExpressionChanged(object sender, NotificationEventArgs e)
        {
			OverShortSuggestedDocuments.Refresh();
            SelectedOverShortSuggestedDocuments.Clear();
            NotifyPropertyChanged(x => SelectedOverShortSuggestedDocuments);
            BeginSendMessage(MessageToken.SelectedOverShortSuggestedDocumentsChanged, new NotificationEventArgs(MessageToken.SelectedOverShortSuggestedDocumentsChanged));
        }

		void OverShortSuggestedDocuments_LoadingStateChanged(object sender, EventArgs e)
        {
            switch (OverShortSuggestedDocuments.LoadingState)
            {
                case QueuedBackgroundWorkerState.Processing:
                    StatusModel.Timer("Getting Data...");
                    break;
                case QueuedBackgroundWorkerState.Standby: 
                    StatusModel.StopStatusUpdate();
                    NotifyPropertyChanged(x => OverShortSuggestedDocuments);
                    break;
                case QueuedBackgroundWorkerState.StoppedByError:
                    StatusModel.Error("OverShortSuggestedDocuments | Error occured..." + OverShortSuggestedDocuments.LastLoadingError.Message);
                    NotifyPropertyChanged(x => OverShortSuggestedDocuments);
                    break;
            }
           
        }

		
		public readonly OverShortSuggestedDocumentVirturalListLoader vloader = new OverShortSuggestedDocumentVirturalListLoader();

		private ObservableCollection<OverShortSuggestedDocument> _selectedOverShortSuggestedDocuments = new ObservableCollection<OverShortSuggestedDocument>();
        public ObservableCollection<OverShortSuggestedDocument> SelectedOverShortSuggestedDocuments
        {
            get
            {
                return _selectedOverShortSuggestedDocuments;
            }
            set
            {
                _selectedOverShortSuggestedDocuments = value;
				BeginSendMessage(MessageToken.SelectedOverShortSuggestedDocumentsChanged,
                                    new NotificationEventArgs(MessageToken.SelectedOverShortSuggestedDocumentsChanged));
				 NotifyPropertyChanged(x => SelectedOverShortSuggestedDocuments);
            }
        }

        internal void OnCurrentOverShortSuggestedDocumentChanged(object sender, NotificationEventArgs<OverShortSuggestedDocument> e)
        {
            if(BaseViewModel.Instance.CurrentOverShortSuggestedDocument != null) BaseViewModel.Instance.CurrentOverShortSuggestedDocument.PropertyChanged += CurrentOverShortSuggestedDocument__propertyChanged;
           // NotifyPropertyChanged(x => this.CurrentOverShortSuggestedDocument);
        }   

            void CurrentOverShortSuggestedDocument__propertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
                {
                    //if (e.PropertyName == "AddOversShortEX")
                   // {
                   //    if(OversShortEX.Contains(CurrentOverShortSuggestedDocument.OversShortEX) == false) OversShortEX.Add(CurrentOverShortSuggestedDocument.OversShortEX);
                    //}
                 } 
        internal void OnOverShortSuggestedDocumentsChanged(object sender, NotificationEventArgs e)
        {
            _OverShortSuggestedDocuments.Refresh();
			NotifyPropertyChanged(x => this.OverShortSuggestedDocuments);
        }   


 	
		 internal void OnCurrentOversShortEXChanged(object sender, SimpleMvvmToolkit.NotificationEventArgs<OversShortEX> e)
			{
			if(ViewCurrentOversShortEX == false) return;
			if (e.Data == null || e.Data.OversShortsId == null)
                {
                    vloader.FilterExpression = "None";
                }
                else
                {
				vloader.FilterExpression = string.Format("OversShortsId == {0}", e.Data.OversShortsId.ToString());
                 }

				OverShortSuggestedDocuments.Refresh();
				NotifyPropertyChanged(x => this.OverShortSuggestedDocuments);
                // SendMessage(MessageToken.OverShortSuggestedDocumentsChanged, new NotificationEventArgs(MessageToken.OverShortSuggestedDocumentsChanged));
                                          
                BaseViewModel.Instance.CurrentOverShortSuggestedDocument = null;
			}

  			// Core Current Entities Changed
			// theorticall don't need this cuz i am inheriting from core entities baseview model so changes should flow up to here
  
// Filtering Each Field except IDs
 	
		 bool _viewCurrentOversShortEX = false;
         public bool ViewCurrentOversShortEX
         {
             get
             {
                 return _viewCurrentOversShortEX;
             }
             set
             {
                 _viewCurrentOversShortEX = value;
                 NotifyPropertyChanged(x => x.ViewCurrentOversShortEX);
             }
         }
		public void ViewAll()
        {
			vloader.FilterExpression = "All";
			vloader.ClearNavigationExpression();
			_OverShortSuggestedDocuments.Refresh();
			NotifyPropertyChanged(x => this.OverShortSuggestedDocuments);
		}

		public async Task SelectAll()
        {
            IEnumerable<OverShortSuggestedDocument> lst = null;
            using (var ctx = new OverShortSuggestedDocumentRepository())
            {
                lst = await ctx.GetOverShortSuggestedDocumentsByExpressionNav(vloader.FilterExpression, vloader.NavigationExpression).ConfigureAwait(continueOnCapturedContext: false);
            }
            SelectedOverShortSuggestedDocuments = new ObservableCollection<OverShortSuggestedDocument>(lst);
        }

 

		private string _cNumberFilter;
        public string CNumberFilter
        {
            get
            {
                return _cNumberFilter;
            }
            set
            {
                _cNumberFilter = value;
				NotifyPropertyChanged(x => CNumberFilter);
                FilterData();
                
            }
        }	

 

		private string _referenceNumberFilter;
        public string ReferenceNumberFilter
        {
            get
            {
                return _referenceNumberFilter;
            }
            set
            {
                _referenceNumberFilter = value;
				NotifyPropertyChanged(x => ReferenceNumberFilter);
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

			OverShortSuggestedDocuments.Refresh();
			NotifyPropertyChanged(x => this.OverShortSuggestedDocuments);
		}		  



		internal virtual StringBuilder GetAutoPropertyFilterString()
		{
		var res = new StringBuilder();
 

									if(string.IsNullOrEmpty(CNumberFilter) == false)
						res.Append(" && " + string.Format("CNumber.Contains(\"{0}\")",  CNumberFilter));						
 

									if(string.IsNullOrEmpty(ReferenceNumberFilter) == false)
						res.Append(" && " + string.Format("ReferenceNumber.Contains(\"{0}\")",  ReferenceNumberFilter));						
			return res.ToString().StartsWith(" &&") || res.Length == 0 ? res:  res.Insert(0," && ");		
		}

// Send to Excel Implementation


        public async Task Send2Excel()
        {
			IEnumerable<OverShortSuggestedDocument> lst = null;
            using (var ctx = new OverShortSuggestedDocumentRepository())
            {
                lst = await ctx.GetOverShortSuggestedDocumentsByExpressionNav(vloader.FilterExpression, vloader.NavigationExpression).ConfigureAwait(continueOnCapturedContext: false);
            }
             if (lst == null || !lst.Any()) 
              {
                   MessageBox.Show("No Data to Send to Excel");
                   return;
               }
            var s = new ExportToExcel<OverShortSuggestedDocumentExcelLine, List<OverShortSuggestedDocumentExcelLine>>
            {
                dataToPrint = lst.Select(x => new OverShortSuggestedDocumentExcelLine
                {
 
                    CNumber = x.CNumber ,
                    
 
                    ReferenceNumber = x.ReferenceNumber 
                    
                }).ToList()
            };
            using (var sta = new StaTaskScheduler(numberOfThreads: 1))
            {
                await Task.Factory.StartNew(s.GenerateReport, CancellationToken.None, TaskCreationOptions.None, sta).ConfigureAwait(false);
            }
        }

        public class OverShortSuggestedDocumentExcelLine
        {
		 
                    public string CNumber { get; set; } 
                    
 
                    public string ReferenceNumber { get; set; } 
                    
        }

		
    }
}
		
