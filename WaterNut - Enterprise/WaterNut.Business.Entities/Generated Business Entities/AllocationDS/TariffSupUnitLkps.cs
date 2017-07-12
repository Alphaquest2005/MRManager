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
    public partial class TariffSupUnitLkps : BaseEntity<TariffSupUnitLkps> , ITrackable
    {
        [DataMember]
        public string TariffCategoryCode 
        {
            get
            {
                return _tariffcategorycode;
            }
            set
            {
                _tariffcategorycode = value;
                //if(this.TrackingState == TrackingState.Unchanged) this.TrackingState = TrackingState.Modified;  
                NotifyPropertyChanged();
            }
        }
        string _tariffcategorycode;
        [DataMember]
        public string SuppUnitCode2 
        {
            get
            {
                return _suppunitcode2;
            }
            set
            {
                _suppunitcode2 = value;
                //if(this.TrackingState == TrackingState.Unchanged) this.TrackingState = TrackingState.Modified;  
                NotifyPropertyChanged();
            }
        }
        string _suppunitcode2;
        [DataMember]
        public string SuppUnitName2 
        {
            get
            {
                return _suppunitname2;
            }
            set
            {
                _suppunitname2 = value;
                //if(this.TrackingState == TrackingState.Unchanged) this.TrackingState = TrackingState.Modified;  
                NotifyPropertyChanged();
            }
        }
        string _suppunitname2;
        [DataMember]
        public double SuppQty 
        {
            get
            {
                return _suppqty;
            }
            set
            {
                _suppqty = value;
                //if(this.TrackingState == TrackingState.Unchanged) this.TrackingState = TrackingState.Modified;  
                NotifyPropertyChanged();
            }
        }
        double _suppqty;
        [DataMember]
        public int Id 
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                //if(this.TrackingState == TrackingState.Unchanged) this.TrackingState = TrackingState.Modified;  
                NotifyPropertyChanged();
            }
        }
        int _id;
        [DataMember]
        public TariffCategory TariffCategory { get; set; }

 //       [DataMember]
 //       public TrackingState TrackingState { get; set; }
 //       [DataMember]
 //       public ICollection<string> ModifiedProperties { get; set; }
//        [DataMember]//JsonProperty,
 //       private Guid EntityIdentifier { get; set; }
    }
}


