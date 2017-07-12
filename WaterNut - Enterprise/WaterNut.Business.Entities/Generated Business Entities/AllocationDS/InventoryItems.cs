﻿// <autogenerated>
//   This file was generated by T4 code generator Business.Entities.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
//using Newtonsoft.Json;
using TrackableEntities;
using Core.Common.Business.Entities;


namespace AllocationDS.Business.Entities
{
    //[JsonObject(IsReference = true)]
    [DataContract(IsReference = true, Namespace="http://www.insight-software.com/WaterNut")]
    public partial class InventoryItems : BaseEntity<InventoryItems> , ITrackable
    {
        partial void AutoGenStartUp() //InventoryItems()
        {
            this.EntryDataDetails = new List<EntryDataDetails>();
            this.InventoryItemAlias = new List<InventoryItemAlias>();
            this.EX9AsycudaSalesAllocations = new List<EX9AsycudaSalesAllocations>();
        }

        [DataMember]
        public string ItemNumber 
        {
            get
            {
                return _itemnumber;
            }
            set
            {
                _itemnumber = value;
                //if(this.TrackingState == TrackingState.Unchanged) this.TrackingState = TrackingState.Modified;  
                NotifyPropertyChanged();
            }
        }
        string _itemnumber;
        [DataMember]
        public string Description 
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                //if(this.TrackingState == TrackingState.Unchanged) this.TrackingState = TrackingState.Modified;  
                NotifyPropertyChanged();
            }
        }
        string _description;
        [DataMember]
        public string Category 
        {
            get
            {
                return _category;
            }
            set
            {
                _category = value;
                //if(this.TrackingState == TrackingState.Unchanged) this.TrackingState = TrackingState.Modified;  
                NotifyPropertyChanged();
            }
        }
        string _category;
        [DataMember]
        public string TariffCode 
        {
            get
            {
                return _tariffcode;
            }
            set
            {
                _tariffcode = value;
                //if(this.TrackingState == TrackingState.Unchanged) this.TrackingState = TrackingState.Modified;  
                NotifyPropertyChanged();
            }
        }
        string _tariffcode;
        [DataMember]
        public Nullable<System.DateTime> EntryTimeStamp 
        {
            get
            {
                return _entrytimestamp;
            }
            set
            {
                _entrytimestamp = value;
                //if(this.TrackingState == TrackingState.Unchanged) this.TrackingState = TrackingState.Modified;  
                NotifyPropertyChanged();
            }
        }
        Nullable<System.DateTime> _entrytimestamp;
        [DataMember]
        public Nullable<int> Quantity 
        {
            get
            {
                return _quantity;
            }
            set
            {
                _quantity = value;
                //if(this.TrackingState == TrackingState.Unchanged) this.TrackingState = TrackingState.Modified;  
                NotifyPropertyChanged();
            }
        }
        Nullable<int> _quantity;
        [DataMember]
        public List<EntryDataDetails> EntryDataDetails { get; set; }
        [DataMember]
        public TariffCodes TariffCodes { get; set; }
        [DataMember]
        public List<InventoryItemAlias> InventoryItemAlias { get; set; }
        [DataMember]
        public List<EX9AsycudaSalesAllocations> EX9AsycudaSalesAllocations { get; set; }

 //       [DataMember]
 //       public TrackingState TrackingState { get; set; }
 //       [DataMember]
 //       public ICollection<string> ModifiedProperties { get; set; }
//        [DataMember]//JsonProperty,
 //       private Guid EntityIdentifier { get; set; }
    }
}


