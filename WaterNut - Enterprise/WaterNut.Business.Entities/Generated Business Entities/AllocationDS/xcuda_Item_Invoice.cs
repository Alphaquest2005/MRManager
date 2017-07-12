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
    public partial class xcuda_Item_Invoice : BaseEntity<xcuda_Item_Invoice> , ITrackable
    {
        [DataMember]
        public double Amount_national_currency 
        {
            get
            {
                return _amount_national_currency;
            }
            set
            {
                _amount_national_currency = value;
                //if(this.TrackingState == TrackingState.Unchanged) this.TrackingState = TrackingState.Modified;  
                NotifyPropertyChanged();
            }
        }
        double _amount_national_currency;
        [DataMember]
        public double Amount_foreign_currency 
        {
            get
            {
                return _amount_foreign_currency;
            }
            set
            {
                _amount_foreign_currency = value;
                //if(this.TrackingState == TrackingState.Unchanged) this.TrackingState = TrackingState.Modified;  
                NotifyPropertyChanged();
            }
        }
        double _amount_foreign_currency;
        [DataMember]
        public string Currency_code 
        {
            get
            {
                return _currency_code;
            }
            set
            {
                _currency_code = value;
                //if(this.TrackingState == TrackingState.Unchanged) this.TrackingState = TrackingState.Modified;  
                NotifyPropertyChanged();
            }
        }
        string _currency_code;
        [DataMember]
        public double Currency_rate 
        {
            get
            {
                return _currency_rate;
            }
            set
            {
                _currency_rate = value;
                //if(this.TrackingState == TrackingState.Unchanged) this.TrackingState = TrackingState.Modified;  
                NotifyPropertyChanged();
            }
        }
        double _currency_rate;
        [DataMember]
        public int Valuation_item_Id 
        {
            get
            {
                return _valuation_item_id;
            }
            set
            {
                _valuation_item_id = value;
                //if(this.TrackingState == TrackingState.Unchanged) this.TrackingState = TrackingState.Modified;  
                NotifyPropertyChanged();
            }
        }
        int _valuation_item_id;
        [DataMember]
        public xcuda_Valuation_item xcuda_Valuation_item { get; set; }

 //       [DataMember]
 //       public TrackingState TrackingState { get; set; }
 //       [DataMember]
 //       public ICollection<string> ModifiedProperties { get; set; }
//        [DataMember]//JsonProperty,
 //       private Guid EntityIdentifier { get; set; }
    }
}


