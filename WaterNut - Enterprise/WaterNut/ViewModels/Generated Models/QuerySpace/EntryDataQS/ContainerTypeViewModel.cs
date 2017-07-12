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
    
	public partial class ContainerTypeViewModel_AutoGen : ViewModelBase<ContainerTypeViewModel_AutoGen>
	{

       private static readonly ContainerTypeViewModel_AutoGen instance;
       static ContainerTypeViewModel_AutoGen()
        {
            instance = new ContainerTypeViewModel_AutoGen();
        }

       public static ContainerTypeViewModel_AutoGen Instance
        {
            get { return instance; }
        }

       private readonly object lockObject = new object();
  
  

        public ContainerTypeViewModel_AutoGen()
        {
          
            RegisterToReceiveMessages<ContainerType>(MessageToken.CurrentContainerTypeChanged, OnCurrentContainerTypeChanged);
            RegisterToReceiveMessages(MessageToken.ContainerTypesChanged, OnContainerTypesChanged);
			RegisterToReceiveMessages(MessageToken.ContainerTypesFilterExpressionChanged, OnContainerTypesFilterExpressionChanged);


 			// Recieve messages for Core Current Entities Changed
 

			ContainerTypes = new VirtualList<ContainerType>(vloader);
			ContainerTypes.LoadingStateChanged += ContainerTypes_LoadingStateChanged;
            BindingOperations.EnableCollectionSynchronization(ContainerTypes, lockObject);
			
            OnCreated();        
            OnTotals();
        }

        partial void OnCreated();
        partial void OnTotals();

		private VirtualList<ContainerType> _ContainerTypes = null;
        public VirtualList<ContainerType> ContainerTypes
        {
            get
            {
                return _ContainerTypes;
            }
            set
            {
                _ContainerTypes = value;
            }
        }

		 private void OnContainerTypesFilterExpressionChanged(object sender, NotificationEventArgs e)
        {
			ContainerTypes.Refresh();
            SelectedContainerTypes.Clear();
            NotifyPropertyChanged(x => SelectedContainerTypes);
            BeginSendMessage(MessageToken.SelectedContainerTypesChanged, new NotificationEventArgs(MessageToken.SelectedContainerTypesChanged));
        }

		void ContainerTypes_LoadingStateChanged(object sender, EventArgs e)
        {
            switch (ContainerTypes.LoadingState)
            {
                case QueuedBackgroundWorkerState.Processing:
                    StatusModel.Timer("Getting Data...");
                    break;
                case QueuedBackgroundWorkerState.Standby: 
                    StatusModel.StopStatusUpdate();
                    NotifyPropertyChanged(x => ContainerTypes);
                    break;
                case QueuedBackgroundWorkerState.StoppedByError:
                    StatusModel.Error("ContainerTypes | Error occured..." + ContainerTypes.LastLoadingError.Message);
                    NotifyPropertyChanged(x => ContainerTypes);
                    break;
            }
           
        }

		
		public readonly ContainerTypeVirturalListLoader vloader = new ContainerTypeVirturalListLoader();

		private ObservableCollection<ContainerType> _selectedContainerTypes = new ObservableCollection<ContainerType>();
        public ObservableCollection<ContainerType> SelectedContainerTypes
        {
            get
            {
                return _selectedContainerTypes;
            }
            set
            {
                _selectedContainerTypes = value;
				BeginSendMessage(MessageToken.SelectedContainerTypesChanged,
                                    new NotificationEventArgs(MessageToken.SelectedContainerTypesChanged));
				 NotifyPropertyChanged(x => SelectedContainerTypes);
            }
        }

        internal void OnCurrentContainerTypeChanged(object sender, NotificationEventArgs<ContainerType> e)
        {
            if(BaseViewModel.Instance.CurrentContainerType != null) BaseViewModel.Instance.CurrentContainerType.PropertyChanged += CurrentContainerType__propertyChanged;
           // NotifyPropertyChanged(x => this.CurrentContainerType);
        }   

            void CurrentContainerType__propertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
                {
                 } 
        internal void OnContainerTypesChanged(object sender, NotificationEventArgs e)
        {
            _ContainerTypes.Refresh();
			NotifyPropertyChanged(x => this.ContainerTypes);
        }   


 
  			// Core Current Entities Changed
			// theorticall don't need this cuz i am inheriting from core entities baseview model so changes should flow up to here
  
// Filtering Each Field except IDs
		public void ViewAll()
        {
			vloader.FilterExpression = "All";
			vloader.ClearNavigationExpression();
			_ContainerTypes.Refresh();
			NotifyPropertyChanged(x => this.ContainerTypes);
		}

		public async Task SelectAll()
        {
            IEnumerable<ContainerType> lst = null;
            using (var ctx = new ContainerTypeRepository())
            {
                lst = await ctx.GetContainerTypesByExpressionNav(vloader.FilterExpression, vloader.NavigationExpression).ConfigureAwait(continueOnCapturedContext: false);
            }
            SelectedContainerTypes = new ObservableCollection<ContainerType>(lst);
        }

 

		private string _containerCodeFilter;
        public string ContainerCodeFilter
        {
            get
            {
                return _containerCodeFilter;
            }
            set
            {
                _containerCodeFilter = value;
				NotifyPropertyChanged(x => ContainerCodeFilter);
                FilterData();
                
            }
        }	

 

		private string _containerTypeDescriptionFilter;
        public string ContainerTypeDescriptionFilter
        {
            get
            {
                return _containerTypeDescriptionFilter;
            }
            set
            {
                _containerTypeDescriptionFilter = value;
				NotifyPropertyChanged(x => ContainerTypeDescriptionFilter);
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

			ContainerTypes.Refresh();
			NotifyPropertyChanged(x => this.ContainerTypes);
		}		  



		internal virtual StringBuilder GetAutoPropertyFilterString()
		{
		var res = new StringBuilder();
 

									if(string.IsNullOrEmpty(ContainerCodeFilter) == false)
						res.Append(" && " + string.Format("ContainerCode.Contains(\"{0}\")",  ContainerCodeFilter));						
 

									if(string.IsNullOrEmpty(ContainerTypeDescriptionFilter) == false)
						res.Append(" && " + string.Format("ContainerTypeDescription.Contains(\"{0}\")",  ContainerTypeDescriptionFilter));						
			return res.ToString().StartsWith(" &&") || res.Length == 0 ? res:  res.Insert(0," && ");		
		}

// Send to Excel Implementation


        public async Task Send2Excel()
        {
			IEnumerable<ContainerType> lst = null;
            using (var ctx = new ContainerTypeRepository())
            {
                lst = await ctx.GetContainerTypesByExpressionNav(vloader.FilterExpression, vloader.NavigationExpression).ConfigureAwait(continueOnCapturedContext: false);
            }
             if (lst == null || !lst.Any()) 
              {
                   MessageBox.Show("No Data to Send to Excel");
                   return;
               }
            var s = new ExportToExcel<ContainerTypeExcelLine, List<ContainerTypeExcelLine>>
            {
                dataToPrint = lst.Select(x => new ContainerTypeExcelLine
                {
 
                    ContainerCode = x.ContainerCode ,
                    
 
                    ContainerTypeDescription = x.ContainerTypeDescription 
                    
                }).ToList()
            };
            using (var sta = new StaTaskScheduler(numberOfThreads: 1))
            {
                await Task.Factory.StartNew(s.GenerateReport, CancellationToken.None, TaskCreationOptions.None, sta).ConfigureAwait(false);
            }
        }

        public class ContainerTypeExcelLine
        {
		 
                    public string ContainerCode { get; set; } 
                    
 
                    public string ContainerTypeDescription { get; set; } 
                    
        }

		
    }
}
		
