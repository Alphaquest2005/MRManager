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

using SalesDataQS.Client.Entities;
using SalesDataQS.Client.Repositories;
//using WaterNut.Client.Repositories;
        
using CoreEntities.Client.Entities;


namespace WaterNut.QuerySpace.SalesDataQS.ViewModels
{
    
	public partial class SalesDataViewModel_AutoGen : ViewModelBase<SalesDataViewModel_AutoGen>
	{

       private static readonly SalesDataViewModel_AutoGen instance;
       static SalesDataViewModel_AutoGen()
        {
            instance = new SalesDataViewModel_AutoGen();
        }

       public static SalesDataViewModel_AutoGen Instance
        {
            get { return instance; }
        }

       private readonly object lockObject = new object();
  
  

        public SalesDataViewModel_AutoGen()
        {
          
            RegisterToReceiveMessages<SalesData>(MessageToken.CurrentSalesDataChanged, OnCurrentSalesDataChanged);
            RegisterToReceiveMessages(MessageToken.SalesDatasChanged, OnSalesDatasChanged);
			RegisterToReceiveMessages(MessageToken.SalesDatasFilterExpressionChanged, OnSalesDatasFilterExpressionChanged);


 			// Recieve messages for Core Current Entities Changed
                        RegisterToReceiveMessages<AsycudaDocument>(CoreEntities.MessageToken.CurrentAsycudaDocumentChanged, OnCurrentAsycudaDocumentChanged);
 

			SalesDatas = new VirtualList<SalesData>(vloader);
			SalesDatas.LoadingStateChanged += SalesDatas_LoadingStateChanged;
            BindingOperations.EnableCollectionSynchronization(SalesDatas, lockObject);
			
            OnCreated();        
            OnTotals();
        }

        partial void OnCreated();
        partial void OnTotals();

		private VirtualList<SalesData> _SalesDatas = null;
        public VirtualList<SalesData> SalesDatas
        {
            get
            {
                return _SalesDatas;
            }
            set
            {
                _SalesDatas = value;
            }
        }

		 private void OnSalesDatasFilterExpressionChanged(object sender, NotificationEventArgs e)
        {
			SalesDatas.Refresh();
            SelectedSalesDatas.Clear();
            NotifyPropertyChanged(x => SelectedSalesDatas);
            BeginSendMessage(MessageToken.SelectedSalesDatasChanged, new NotificationEventArgs(MessageToken.SelectedSalesDatasChanged));
        }

		void SalesDatas_LoadingStateChanged(object sender, EventArgs e)
        {
            switch (SalesDatas.LoadingState)
            {
                case QueuedBackgroundWorkerState.Processing:
                    StatusModel.Timer("Getting Data...");
                    break;
                case QueuedBackgroundWorkerState.Standby: 
                    StatusModel.StopStatusUpdate();
                    NotifyPropertyChanged(x => SalesDatas);
                    break;
                case QueuedBackgroundWorkerState.StoppedByError:
                    StatusModel.Error("SalesDatas | Error occured..." + SalesDatas.LastLoadingError.Message);
                    NotifyPropertyChanged(x => SalesDatas);
                    break;
            }
           
        }

		
		public readonly SalesDataVirturalListLoader vloader = new SalesDataVirturalListLoader();

		private ObservableCollection<SalesData> _selectedSalesDatas = new ObservableCollection<SalesData>();
        public ObservableCollection<SalesData> SelectedSalesDatas
        {
            get
            {
                return _selectedSalesDatas;
            }
            set
            {
                _selectedSalesDatas = value;
				BeginSendMessage(MessageToken.SelectedSalesDatasChanged,
                                    new NotificationEventArgs(MessageToken.SelectedSalesDatasChanged));
				 NotifyPropertyChanged(x => SelectedSalesDatas);
            }
        }

        internal void OnCurrentSalesDataChanged(object sender, NotificationEventArgs<SalesData> e)
        {
            if(BaseViewModel.Instance.CurrentSalesData != null) BaseViewModel.Instance.CurrentSalesData.PropertyChanged += CurrentSalesData__propertyChanged;
           // NotifyPropertyChanged(x => this.CurrentSalesData);
        }   

            void CurrentSalesData__propertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
                {
                    //if (e.PropertyName == "AddAsycudaDocument")
                   // {
                   //    if(AsycudaDocuments.Contains(CurrentSalesData.AsycudaDocument) == false) AsycudaDocuments.Add(CurrentSalesData.AsycudaDocument);
                    //}
                 } 
        internal void OnSalesDatasChanged(object sender, NotificationEventArgs e)
        {
            _SalesDatas.Refresh();
			NotifyPropertyChanged(x => this.SalesDatas);
        }   


 
  			// Core Current Entities Changed
			// theorticall don't need this cuz i am inheriting from core entities baseview model so changes should flow up to here
                internal void OnCurrentAsycudaDocumentChanged(object sender, SimpleMvvmToolkit.NotificationEventArgs<AsycudaDocument> e)
				{
				if (e.Data == null || e.Data.ASYCUDA_Id == null)
                {
                    vloader.FilterExpression = null;
                }
                else
                {
                    vloader.FilterExpression = string.Format("AsycudaDocumentId == {0}", e.Data.ASYCUDA_Id.ToString());
                }
					
                    SalesDatas.Refresh();
					NotifyPropertyChanged(x => this.SalesDatas);
				}
  
// Filtering Each Field except IDs
		public void ViewAll()
        {
			vloader.FilterExpression = "All";
			vloader.ClearNavigationExpression();
			_SalesDatas.Refresh();
			NotifyPropertyChanged(x => this.SalesDatas);
		}

		public async Task SelectAll()
        {
            IEnumerable<SalesData> lst = null;
            using (var ctx = new SalesDataRepository())
            {
                lst = await ctx.GetSalesDatasByExpressionNav(vloader.FilterExpression, vloader.NavigationExpression).ConfigureAwait(continueOnCapturedContext: false);
            }
            SelectedSalesDatas = new ObservableCollection<SalesData>(lst);
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

 
		private DateTime? _startEntryDataDateFilter = DateTime.Parse(string.Format("{0}/1/{1}", DateTime.Now.Month ,DateTime.Now.Year));
        public DateTime? StartEntryDataDateFilter
        {
            get
            {
                return _startEntryDataDateFilter;
            }
            set
            {
                _startEntryDataDateFilter = value;
				NotifyPropertyChanged(x => StartEntryDataDateFilter);
                FilterData();
                
            }
        }	

		private DateTime? _endEntryDataDateFilter = DateTime.Parse(string.Format("{1}/{0}/{2}", DateTime.DaysInMonth( DateTime.Now.Year,DateTime.Now.Month), DateTime.Now.Month, DateTime.Now.Year));
        public DateTime? EndEntryDataDateFilter
        {
            get
            {
                return _endEntryDataDateFilter;
            }
            set
            {
                _endEntryDataDateFilter = value;
				NotifyPropertyChanged(x => EndEntryDataDateFilter);
                FilterData();
                
            }
        }
 

		private DateTime? _entryDataDateFilter;
        public DateTime? EntryDataDateFilter
        {
            get
            {
                return _entryDataDateFilter;
            }
            set
            {
                _entryDataDateFilter = value;
				NotifyPropertyChanged(x => EntryDataDateFilter);
                FilterData();
                
            }
        }	

 

		private string _typeFilter;
        public string TypeFilter
        {
            get
            {
                return _typeFilter;
            }
            set
            {
                _typeFilter = value;
				NotifyPropertyChanged(x => TypeFilter);
                FilterData();
                
            }
        }	

 

		private Double? _taxAmountFilter;
        public Double? TaxAmountFilter
        {
            get
            {
                return _taxAmountFilter;
            }
            set
            {
                _taxAmountFilter = value;
				NotifyPropertyChanged(x => TaxAmountFilter);
                FilterData();
                
            }
        }	

 

		private string _customerNameFilter;
        public string CustomerNameFilter
        {
            get
            {
                return _customerNameFilter;
            }
            set
            {
                _customerNameFilter = value;
				NotifyPropertyChanged(x => CustomerNameFilter);
                FilterData();
                
            }
        }	

 

		private Double? _totalFilter;
        public Double? TotalFilter
        {
            get
            {
                return _totalFilter;
            }
            set
            {
                _totalFilter = value;
				NotifyPropertyChanged(x => TotalFilter);
                FilterData();
                
            }
        }	

 

		private Double? _allocatedTotalFilter;
        public Double? AllocatedTotalFilter
        {
            get
            {
                return _allocatedTotalFilter;
            }
            set
            {
                _allocatedTotalFilter = value;
				NotifyPropertyChanged(x => AllocatedTotalFilter);
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

			SalesDatas.Refresh();
			NotifyPropertyChanged(x => this.SalesDatas);
		}		  



		internal virtual StringBuilder GetAutoPropertyFilterString()
		{
		var res = new StringBuilder();
 

									if(string.IsNullOrEmpty(EntryDataIdFilter) == false)
						res.Append(" && " + string.Format("EntryDataId.Contains(\"{0}\")",  EntryDataIdFilter));						
 

 

				if (Convert.ToDateTime(StartEntryDataDateFilter).Date != DateTime.MinValue &&
		        Convert.ToDateTime(EndEntryDataDateFilter).Date != DateTime.MinValue) res.Append(" && (");

					if (Convert.ToDateTime(StartEntryDataDateFilter).Date != DateTime.MinValue)
						{
							if(StartEntryDataDateFilter.HasValue)
								res.Append(
                                            (Convert.ToDateTime(EndEntryDataDateFilter).Date != DateTime.MinValue?"":" && ") +
                                            string.Format("EntryDataDate >= \"{0}\"",  Convert.ToDateTime(StartEntryDataDateFilter).Date.ToString("MM/dd/yyyy")));
						}

					if (Convert.ToDateTime(EndEntryDataDateFilter).Date != DateTime.MinValue)
						{
							if(EndEntryDataDateFilter.HasValue)
								res.Append(" && " + string.Format("EntryDataDate <= \"{0}\"",  Convert.ToDateTime(EndEntryDataDateFilter).Date.AddHours(23).ToString("MM/dd/yyyy")));
						}

				if (Convert.ToDateTime(StartEntryDataDateFilter).Date != DateTime.MinValue &&
		        Convert.ToDateTime(EndEntryDataDateFilter).Date != DateTime.MinValue) res.Append(" )");

					if (Convert.ToDateTime(_entryDataDateFilter).Date != DateTime.MinValue)
						{
							if(EntryDataDateFilter.HasValue)
								res.Append(" && " + string.Format("EntryDataDate == \"{0}\"",  Convert.ToDateTime(EntryDataDateFilter).Date.ToString("MM/dd/yyyy")));
						}
				 

									if(string.IsNullOrEmpty(TypeFilter) == false)
						res.Append(" && " + string.Format("Type.Contains(\"{0}\")",  TypeFilter));						
 

					if(TaxAmountFilter.HasValue)
						res.Append(" && " + string.Format("TaxAmount == {0}",  TaxAmountFilter.ToString()));				 

									if(string.IsNullOrEmpty(CustomerNameFilter) == false)
						res.Append(" && " + string.Format("CustomerName.Contains(\"{0}\")",  CustomerNameFilter));						
 

					if(TotalFilter.HasValue)
						res.Append(" && " + string.Format("Total == {0}",  TotalFilter.ToString()));				 

					if(AllocatedTotalFilter.HasValue)
						res.Append(" && " + string.Format("AllocatedTotal == {0}",  AllocatedTotalFilter.ToString()));							return res.ToString().StartsWith(" &&") || res.Length == 0 ? res:  res.Insert(0," && ");		
		}

// Send to Excel Implementation


        public async Task Send2Excel()
        {
			IEnumerable<SalesData> lst = null;
            using (var ctx = new SalesDataRepository())
            {
                lst = await ctx.GetSalesDatasByExpressionNav(vloader.FilterExpression, vloader.NavigationExpression).ConfigureAwait(continueOnCapturedContext: false);
            }
             if (lst == null || !lst.Any()) 
              {
                   MessageBox.Show("No Data to Send to Excel");
                   return;
               }
            var s = new ExportToExcel<SalesDataExcelLine, List<SalesDataExcelLine>>
            {
                dataToPrint = lst.Select(x => new SalesDataExcelLine
                {
 
                    EntryDataId = x.EntryDataId ,
                    
 
                    EntryDataDate = x.EntryDataDate ,
                    
 
                    Type = x.Type ,
                    
 
                    TaxAmount = x.TaxAmount ,
                    
 
                    CustomerName = x.CustomerName ,
                    
 
                    Total = x.Total ,
                    
 
                    AllocatedTotal = x.AllocatedTotal 
                    
                }).ToList()
            };
            using (var sta = new StaTaskScheduler(numberOfThreads: 1))
            {
                await Task.Factory.StartNew(s.GenerateReport, CancellationToken.None, TaskCreationOptions.None, sta).ConfigureAwait(false);
            }
        }

        public class SalesDataExcelLine
        {
		 
                    public string EntryDataId { get; set; } 
                    
 
                    public System.DateTime EntryDataDate { get; set; } 
                    
 
                    public string Type { get; set; } 
                    
 
                    public Nullable<double> TaxAmount { get; set; } 
                    
 
                    public string CustomerName { get; set; } 
                    
 
                    public Nullable<double> Total { get; set; } 
                    
 
                    public Nullable<double> AllocatedTotal { get; set; } 
                    
        }

		
    }
}
		
