﻿// <autogenerated>
//   This file was generated by T4 code generator AllBusinessEntities.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
//using Newtonsoft.Json;
using TrackableEntities;
using Core.Common.Business.Entities;


namespace DocumentItemDS.Business.Entities
{
    //[JsonObject(IsReference = true)]
    [DataContract(IsReference = true, Namespace="http://www.insight-software.com/WaterNut")]
    public partial class xcuda_ItemEntryDataDetails : BaseEntity<xcuda_ItemEntryDataDetails> , ITrackable
    {
        [DataMember]
        public int Item_Id 
        {
            get
            {
                return _item_id;
            }
            set
            {
                _item_id = value;
                //if(this.TrackingState == TrackingState.Unchanged) this.TrackingState = TrackingState.Modified;  
                NotifyPropertyChanged();
            }
        }
        int _item_id;
        [DataMember]
        public int EntryDataDetailsId 
        {
            get
            {
                return _entrydatadetailsid;
            }
            set
            {
                _entrydatadetailsid = value;
                //if(this.TrackingState == TrackingState.Unchanged) this.TrackingState = TrackingState.Modified;  
                NotifyPropertyChanged();
            }
        }
        int _entrydatadetailsid;
        [DataMember]
        public int ItemEntryDataDetailId 
        {
            get
            {
                return _itementrydatadetailid;
            }
            set
            {
                _itementrydatadetailid = value;
                //if(this.TrackingState == TrackingState.Unchanged) this.TrackingState = TrackingState.Modified;  
                NotifyPropertyChanged();
            }
        }
        int _itementrydatadetailid;
        [DataMember]
        public xcuda_Item xcuda_Item { get; set; }

 //       [DataMember]
 //       public TrackingState TrackingState { get; set; }
 //       [DataMember]
 //       public ICollection<string> ModifiedProperties { get; set; }
//        [DataMember]//JsonProperty,
 //       private Guid EntityIdentifier { get; set; }
    }
}


