﻿// <autogenerated>
//   This file was generated by T4 code generator AllClientEntities.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
//using Newtonsoft.Json;
using TrackableEntities;
using TrackableEntities.Client;
using Core.Common.Client.DTO;

namespace CoreEntities.Client.DTO
{

   // [JsonObject(IsReference = true)]
    [DataContract(IsReference = true, Namespace="http://www.insight-software.com/WaterNut")]
    public partial class ApplicationSettings : BaseEntity<ApplicationSettings> , ITrackable, IEquatable<ApplicationSettings>
    {
        [DataMember]
        public int ApplicationSettingsId
		{ 
		    get { return _ApplicationSettingsId; }
			set
			{
			    if (value == _ApplicationSettingsId) return;
				_ApplicationSettingsId = value;
				NotifyPropertyChanged();//m => this.ApplicationSettingsId
			}
		}
        private int _ApplicationSettingsId;

        [DataMember]
        public string Description
		{ 
		    get { return _Description; }
			set
			{
			    if (value == _Description) return;
				_Description = value;
				NotifyPropertyChanged();//m => this.Description
			}
		}
        private string _Description;

        [DataMember]
        public Nullable<int> MaxEntryLines
		{ 
		    get { return _MaxEntryLines; }
			set
			{
			    if (value == _MaxEntryLines) return;
				_MaxEntryLines = value;
				NotifyPropertyChanged();//m => this.MaxEntryLines
			}
		}
        private Nullable<int> _MaxEntryLines;

        [DataMember]
        public string SoftwareName
		{ 
		    get { return _SoftwareName; }
			set
			{
			    if (value == _SoftwareName) return;
				_SoftwareName = value;
				NotifyPropertyChanged();//m => this.SoftwareName
			}
		}
        private string _SoftwareName;

        [DataMember]
        public string AllowCounterPoint
		{ 
		    get { return _AllowCounterPoint; }
			set
			{
			    if (value == _AllowCounterPoint) return;
				_AllowCounterPoint = value;
				NotifyPropertyChanged();//m => this.AllowCounterPoint
			}
		}
        private string _AllowCounterPoint;

        [DataMember]
        public Nullable<bool> GroupEX9
		{ 
		    get { return _GroupEX9; }
			set
			{
			    if (value == _GroupEX9) return;
				_GroupEX9 = value;
				NotifyPropertyChanged();//m => this.GroupEX9
			}
		}
        private Nullable<bool> _GroupEX9;

        [DataMember]
        public Nullable<bool> InvoicePerEntry
		{ 
		    get { return _InvoicePerEntry; }
			set
			{
			    if (value == _InvoicePerEntry) return;
				_InvoicePerEntry = value;
				NotifyPropertyChanged();//m => this.InvoicePerEntry
			}
		}
        private Nullable<bool> _InvoicePerEntry;

        [DataMember]
        public string AllowTariffCodes
		{ 
		    get { return _AllowTariffCodes; }
			set
			{
			    if (value == _AllowTariffCodes) return;
				_AllowTariffCodes = value;
				NotifyPropertyChanged();//m => this.AllowTariffCodes
			}
		}
        private string _AllowTariffCodes;

        [DataMember]
        public string AllowWareHouse
		{ 
		    get { return _AllowWareHouse; }
			set
			{
			    if (value == _AllowWareHouse) return;
				_AllowWareHouse = value;
				NotifyPropertyChanged();//m => this.AllowWareHouse
			}
		}
        private string _AllowWareHouse;

        [DataMember]
        public string AllowXBond
		{ 
		    get { return _AllowXBond; }
			set
			{
			    if (value == _AllowXBond) return;
				_AllowXBond = value;
				NotifyPropertyChanged();//m => this.AllowXBond
			}
		}
        private string _AllowXBond;

        [DataMember]
        public string AllowAsycudaManager
		{ 
		    get { return _AllowAsycudaManager; }
			set
			{
			    if (value == _AllowAsycudaManager) return;
				_AllowAsycudaManager = value;
				NotifyPropertyChanged();//m => this.AllowAsycudaManager
			}
		}
        private string _AllowAsycudaManager;

        [DataMember]
        public string AllowQuickBooks
		{ 
		    get { return _AllowQuickBooks; }
			set
			{
			    if (value == _AllowQuickBooks) return;
				_AllowQuickBooks = value;
				NotifyPropertyChanged();//m => this.AllowQuickBooks
			}
		}
        private string _AllowQuickBooks;

        [DataMember]
        public Nullable<bool> ItemDescriptionContainsAsycudaAttribute
		{ 
		    get { return _ItemDescriptionContainsAsycudaAttribute; }
			set
			{
			    if (value == _ItemDescriptionContainsAsycudaAttribute) return;
				_ItemDescriptionContainsAsycudaAttribute = value;
				NotifyPropertyChanged();//m => this.ItemDescriptionContainsAsycudaAttribute
			}
		}
        private Nullable<bool> _ItemDescriptionContainsAsycudaAttribute;

        [DataMember]
        public string AllowExportToExcel
		{ 
		    get { return _AllowExportToExcel; }
			set
			{
			    if (value == _AllowExportToExcel) return;
				_AllowExportToExcel = value;
				NotifyPropertyChanged();//m => this.AllowExportToExcel
			}
		}
        private string _AllowExportToExcel;

        [DataMember]
        public string AllowAutoWeightCalculation
		{ 
		    get { return _AllowAutoWeightCalculation; }
			set
			{
			    if (value == _AllowAutoWeightCalculation) return;
				_AllowAutoWeightCalculation = value;
				NotifyPropertyChanged();//m => this.AllowAutoWeightCalculation
			}
		}
        private string _AllowAutoWeightCalculation;

        [DataMember]
        public string AllowEntryPerIM7
		{ 
		    get { return _AllowEntryPerIM7; }
			set
			{
			    if (value == _AllowEntryPerIM7) return;
				_AllowEntryPerIM7 = value;
				NotifyPropertyChanged();//m => this.AllowEntryPerIM7
			}
		}
        private string _AllowEntryPerIM7;

        [DataMember]
        public string AllowSalesToPI
		{ 
		    get { return _AllowSalesToPI; }
			set
			{
			    if (value == _AllowSalesToPI) return;
				_AllowSalesToPI = value;
				NotifyPropertyChanged();//m => this.AllowSalesToPI
			}
		}
        private string _AllowSalesToPI;

        [DataMember]
        public string AllowEffectiveAssessmentDate
		{ 
		    get { return _AllowEffectiveAssessmentDate; }
			set
			{
			    if (value == _AllowEffectiveAssessmentDate) return;
				_AllowEffectiveAssessmentDate = value;
				NotifyPropertyChanged();//m => this.AllowEffectiveAssessmentDate
			}
		}
        private string _AllowEffectiveAssessmentDate;

        [DataMember]
        public string AllowAutoFreightCalculation
		{ 
		    get { return _AllowAutoFreightCalculation; }
			set
			{
			    if (value == _AllowAutoFreightCalculation) return;
				_AllowAutoFreightCalculation = value;
				NotifyPropertyChanged();//m => this.AllowAutoFreightCalculation
			}
		}
        private string _AllowAutoFreightCalculation;

        [DataMember]
        public string AllowSubItems
		{ 
		    get { return _AllowSubItems; }
			set
			{
			    if (value == _AllowSubItems) return;
				_AllowSubItems = value;
				NotifyPropertyChanged();//m => this.AllowSubItems
			}
		}
        private string _AllowSubItems;

        [DataMember]
        public string AllowEntryDoNotAllocate
		{ 
		    get { return _AllowEntryDoNotAllocate; }
			set
			{
			    if (value == _AllowEntryDoNotAllocate) return;
				_AllowEntryDoNotAllocate = value;
				NotifyPropertyChanged();//m => this.AllowEntryDoNotAllocate
			}
		}
        private string _AllowEntryDoNotAllocate;

        [DataMember]
        public string AllowPreviousItems
		{ 
		    get { return _AllowPreviousItems; }
			set
			{
			    if (value == _AllowPreviousItems) return;
				_AllowPreviousItems = value;
				NotifyPropertyChanged();//m => this.AllowPreviousItems
			}
		}
        private string _AllowPreviousItems;

        [DataMember]
        public string AllowOversShort
		{ 
		    get { return _AllowOversShort; }
			set
			{
			    if (value == _AllowOversShort) return;
				_AllowOversShort = value;
				NotifyPropertyChanged();//m => this.AllowOversShort
			}
		}
        private string _AllowOversShort;

        [DataMember]
        public string AllowContainers
		{ 
		    get { return _AllowContainers; }
			set
			{
			    if (value == _AllowContainers) return;
				_AllowContainers = value;
				NotifyPropertyChanged();//m => this.AllowContainers
			}
		}
        private string _AllowContainers;

        [DataMember]
        public string AllowNonXEntries
		{ 
		    get { return _AllowNonXEntries; }
			set
			{
			    if (value == _AllowNonXEntries) return;
				_AllowNonXEntries = value;
				NotifyPropertyChanged();//m => this.AllowNonXEntries
			}
		}
        private string _AllowNonXEntries;

        [DataMember]
        public string AllowValidateTariffCodes
		{ 
		    get { return _AllowValidateTariffCodes; }
			set
			{
			    if (value == _AllowValidateTariffCodes) return;
				_AllowValidateTariffCodes = value;
				NotifyPropertyChanged();//m => this.AllowValidateTariffCodes
			}
		}
        private string _AllowValidateTariffCodes;

        [DataMember]
        public string AllowCleanBond
		{ 
		    get { return _AllowCleanBond; }
			set
			{
			    if (value == _AllowCleanBond) return;
				_AllowCleanBond = value;
				NotifyPropertyChanged();//m => this.AllowCleanBond
			}
		}
        private string _AllowCleanBond;

        [DataMember]
        public string OrderEntriesBy
		{ 
		    get { return _OrderEntriesBy; }
			set
			{
			    if (value == _OrderEntriesBy) return;
				_OrderEntriesBy = value;
				NotifyPropertyChanged();//m => this.OrderEntriesBy
			}
		}
        private string _OrderEntriesBy;

        [DataMember]
        public Nullable<System.DateTime> OpeningStockDate
		{ 
		    get { return _OpeningStockDate; }
			set
			{
			    if (value == _OpeningStockDate) return;
				_OpeningStockDate = value;
				NotifyPropertyChanged();//m => this.OpeningStockDate
			}
		}
        private Nullable<System.DateTime> _OpeningStockDate;

        [DataMember]
        public string AllowWeightEqualQuantity
		{ 
		    get { return _AllowWeightEqualQuantity; }
			set
			{
			    if (value == _AllowWeightEqualQuantity) return;
				_AllowWeightEqualQuantity = value;
				NotifyPropertyChanged();//m => this.AllowWeightEqualQuantity
			}
		}
        private string _AllowWeightEqualQuantity;

       
   //     [DataMember]
   //     public TrackingState TrackingState { get; set; }

   //     [DataMember]
   //     public ICollection<string> ModifiedProperties { get; set; }
        
    //  [DataMember]//JsonProperty, 
    //	private Guid EntityIdentifier { get; set; }
    
    //	[DataMember]//JsonProperty, 
    //	private Guid _entityIdentity = default(Guid);
    
    	bool IEquatable<ApplicationSettings>.Equals(ApplicationSettings other)
    	{
    		if (EntityIdentifier != default(Guid))
    			return EntityIdentifier == other.EntityIdentifier;
    		return false;
    	}
    }
}



