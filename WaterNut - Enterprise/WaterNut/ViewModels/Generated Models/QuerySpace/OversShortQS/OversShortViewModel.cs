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
    
	public partial class OversShortViewModel_AutoGen : ViewModelBase<OversShortViewModel_AutoGen>
	{

       private static readonly OversShortViewModel_AutoGen instance;
       static OversShortViewModel_AutoGen()
        {
            instance = new OversShortViewModel_AutoGen();
        }

       public static OversShortViewModel_AutoGen Instance
        {
            get { return instance; }
        }

       private readonly object lockObject = new object();
  
  

        public OversShortViewModel_AutoGen()
        {
          
            RegisterToReceiveMessages<OversShort>(MessageToken.CurrentOversShortChanged, OnCurrentOversShortChanged);
            RegisterToReceiveMessages(MessageToken.OversShortsChanged, OnOversShortsChanged);
			RegisterToReceiveMessages(MessageToken.OversShortsFilterExpressionChanged, OnOversShortsFilterExpressionChanged);


 			// Recieve messages for Core Current Entities Changed
 

			OversShorts = new VirtualList<OversShort>(vloader);
			OversShorts.LoadingStateChanged += OversShorts_LoadingStateChanged;
            BindingOperations.EnableCollectionSynchronization(OversShorts, lockObject);
			
			vloader.FilterExpression = "All";
             OnCreated();        
            OnTotals();
        }

        partial void OnCreated();
        partial void OnTotals();

		private VirtualList<OversShort> _OversShorts = null;
        public VirtualList<OversShort> OversShorts
        {
            get
            {
                return _OversShorts;
            }
            set
            {
                _OversShorts = value;
            }
        }

		 private void OnOversShortsFilterExpressionChanged(object sender, NotificationEventArgs e)
        {
			OversShorts.Refresh();
            SelectedOversShorts.Clear();
            NotifyPropertyChanged(x => SelectedOversShorts);
            BeginSendMessage(MessageToken.SelectedOversShortsChanged, new NotificationEventArgs(MessageToken.SelectedOversShortsChanged));
        }

		void OversShorts_LoadingStateChanged(object sender, EventArgs e)
        {
            switch (OversShorts.LoadingState)
            {
                case QueuedBackgroundWorkerState.Processing:
                    StatusModel.Timer("Getting Data...");
                    break;
                case QueuedBackgroundWorkerState.Standby: 
                    StatusModel.StopStatusUpdate();
                    NotifyPropertyChanged(x => OversShorts);
                    break;
                case QueuedBackgroundWorkerState.StoppedByError:
                    StatusModel.Error("OversShorts | Error occured..." + OversShorts.LastLoadingError.Message);
                    NotifyPropertyChanged(x => OversShorts);
                    break;
            }
           
        }

		
		public readonly OversShortVirturalListLoader vloader = new OversShortVirturalListLoader();

		private ObservableCollection<OversShort> _selectedOversShorts = new ObservableCollection<OversShort>();
        public ObservableCollection<OversShort> SelectedOversShorts
        {
            get
            {
                return _selectedOversShorts;
            }
            set
            {
                _selectedOversShorts = value;
				BeginSendMessage(MessageToken.SelectedOversShortsChanged,
                                    new NotificationEventArgs(MessageToken.SelectedOversShortsChanged));
				 NotifyPropertyChanged(x => SelectedOversShorts);
            }
        }

        internal void OnCurrentOversShortChanged(object sender, NotificationEventArgs<OversShort> e)
        {
            if(BaseViewModel.Instance.CurrentOversShort != null) BaseViewModel.Instance.CurrentOversShort.PropertyChanged += CurrentOversShort__propertyChanged;
           // NotifyPropertyChanged(x => this.CurrentOversShort);
        }   

            void CurrentOversShort__propertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
                {
                 } 
        internal void OnOversShortsChanged(object sender, NotificationEventArgs e)
        {
            _OversShorts.Refresh();
			NotifyPropertyChanged(x => this.OversShorts);
        }   


 
  			// Core Current Entities Changed
			// theorticall don't need this cuz i am inheriting from core entities baseview model so changes should flow up to here
  
// Filtering Each Field except IDs
		public void ViewAll()
        {
			vloader.FilterExpression = "All";
			vloader.ClearNavigationExpression();
			_OversShorts.Refresh();
			NotifyPropertyChanged(x => this.OversShorts);
		}

		public async Task SelectAll()
        {
            IEnumerable<OversShort> lst = null;
            using (var ctx = new OversShortRepository())
            {
                lst = await ctx.GetOversShortsByExpressionNav(vloader.FilterExpression, vloader.NavigationExpression).ConfigureAwait(continueOnCapturedContext: false);
            }
            SelectedOversShorts = new ObservableCollection<OversShort>(lst);
        }

 

		private string _invoiceNoFilter;
        public string InvoiceNoFilter
        {
            get
            {
                return _invoiceNoFilter;
            }
            set
            {
                _invoiceNoFilter = value;
				NotifyPropertyChanged(x => InvoiceNoFilter);
                FilterData();
                
            }
        }	

 
		private DateTime? _startInvoiceDateFilter = DateTime.Parse(string.Format("{0}/1/{1}", DateTime.Now.Month ,DateTime.Now.Year));
        public DateTime? StartInvoiceDateFilter
        {
            get
            {
                return _startInvoiceDateFilter;
            }
            set
            {
                _startInvoiceDateFilter = value;
				NotifyPropertyChanged(x => StartInvoiceDateFilter);
                FilterData();
                
            }
        }	

		private DateTime? _endInvoiceDateFilter = DateTime.Parse(string.Format("{1}/{0}/{2}", DateTime.DaysInMonth( DateTime.Now.Year,DateTime.Now.Month), DateTime.Now.Month, DateTime.Now.Year));
        public DateTime? EndInvoiceDateFilter
        {
            get
            {
                return _endInvoiceDateFilter;
            }
            set
            {
                _endInvoiceDateFilter = value;
				NotifyPropertyChanged(x => EndInvoiceDateFilter);
                FilterData();
                
            }
        }
 

		private DateTime? _invoiceDateFilter;
        public DateTime? InvoiceDateFilter
        {
            get
            {
                return _invoiceDateFilter;
            }
            set
            {
                _invoiceDateFilter = value;
				NotifyPropertyChanged(x => InvoiceDateFilter);
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

			OversShorts.Refresh();
			NotifyPropertyChanged(x => this.OversShorts);
		}		  



		internal virtual StringBuilder GetAutoPropertyFilterString()
		{
		var res = new StringBuilder();
 

									if(string.IsNullOrEmpty(InvoiceNoFilter) == false)
						res.Append(" && " + string.Format("InvoiceNo.Contains(\"{0}\")",  InvoiceNoFilter));						
 

 

				if (Convert.ToDateTime(StartInvoiceDateFilter).Date != DateTime.MinValue &&
		        Convert.ToDateTime(EndInvoiceDateFilter).Date != DateTime.MinValue) res.Append(" && (");

					if (Convert.ToDateTime(StartInvoiceDateFilter).Date != DateTime.MinValue)
						{
							if(StartInvoiceDateFilter.HasValue)
								res.Append(
                                            (Convert.ToDateTime(EndInvoiceDateFilter).Date != DateTime.MinValue?"":" && ") +
                                            string.Format("InvoiceDate >= \"{0}\"",  Convert.ToDateTime(StartInvoiceDateFilter).Date.ToString("MM/dd/yyyy")));
						}

					if (Convert.ToDateTime(EndInvoiceDateFilter).Date != DateTime.MinValue)
						{
							if(EndInvoiceDateFilter.HasValue)
								res.Append(" && " + string.Format("InvoiceDate <= \"{0}\"",  Convert.ToDateTime(EndInvoiceDateFilter).Date.AddHours(23).ToString("MM/dd/yyyy")));
						}

				if (Convert.ToDateTime(StartInvoiceDateFilter).Date != DateTime.MinValue &&
		        Convert.ToDateTime(EndInvoiceDateFilter).Date != DateTime.MinValue) res.Append(" )");

					if (Convert.ToDateTime(_invoiceDateFilter).Date != DateTime.MinValue)
						{
							if(InvoiceDateFilter.HasValue)
								res.Append(" && " + string.Format("InvoiceDate == \"{0}\"",  Convert.ToDateTime(InvoiceDateFilter).Date.ToString("MM/dd/yyyy")));
						}
							return res.ToString().StartsWith(" &&") || res.Length == 0 ? res:  res.Insert(0," && ");		
		}

// Send to Excel Implementation


        public async Task Send2Excel()
        {
			IEnumerable<OversShort> lst = null;
            using (var ctx = new OversShortRepository())
            {
                lst = await ctx.GetOversShortsByExpressionNav(vloader.FilterExpression, vloader.NavigationExpression).ConfigureAwait(continueOnCapturedContext: false);
            }
             if (lst == null || !lst.Any()) 
              {
                   MessageBox.Show("No Data to Send to Excel");
                   return;
               }
            var s = new ExportToExcel<OversShortExcelLine, List<OversShortExcelLine>>
            {
                dataToPrint = lst.Select(x => new OversShortExcelLine
                {
 
                    InvoiceNo = x.InvoiceNo ,
                    
 
                    InvoiceDate = x.InvoiceDate 
                    
                }).ToList()
            };
            using (var sta = new StaTaskScheduler(numberOfThreads: 1))
            {
                await Task.Factory.StartNew(s.GenerateReport, CancellationToken.None, TaskCreationOptions.None, sta).ConfigureAwait(false);
            }
        }

        public class OversShortExcelLine
        {
		 
                    public string InvoiceNo { get; set; } 
                    
 
                    public System.DateTime InvoiceDate { get; set; } 
                    
        }

		
    }
}
		