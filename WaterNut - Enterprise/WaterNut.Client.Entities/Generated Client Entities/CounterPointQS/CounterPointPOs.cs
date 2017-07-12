﻿// <autogenerated>
//   This file was generated by T4 code generator AllClientEntities.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using WaterNut.Interfaces;
using Core.Common.Client.Entities;
using CounterPointQS.Client.DTO;
using TrackableEntities.Client;
using TrackableEntities;
using Core.Common.Validation;

namespace CounterPointQS.Client.Entities
{
       public partial class CounterPointPOs: BaseEntity<CounterPointPOs>
    {
        DTO.CounterPointPOs counterpointpos;
        public CounterPointPOs(DTO.CounterPointPOs dto )
        {
              counterpointpos = dto;
             _changeTracker = new ChangeTrackingCollection<DTO.CounterPointPOs>(counterpointpos);

        }

        public DTO.CounterPointPOs DTO
        {
            get
            {
             return counterpointpos;
            }
            set
            {
                counterpointpos = value;
            }
        }
       [RequiredValidationAttribute(ErrorMessage= "PurchaseOrderNo is required")]
       
                
                [MaxLength(20, ErrorMessage = "PurchaseOrderNo has a max length of 20 letters ")]
public string PurchaseOrderNo
		{ 
		    get { return this.counterpointpos.PurchaseOrderNo; }
			set
			{
			    if (value == this.counterpointpos.PurchaseOrderNo) return;
				this.counterpointpos.PurchaseOrderNo = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("PurchaseOrderNo");
			}
		}
     

       [RequiredValidationAttribute(ErrorMessage= "Date is required")]
       
public System.DateTime Date
		{ 
		    get { return this.counterpointpos.Date; }
			set
			{
			    if (value == this.counterpointpos.Date) return;
				this.counterpointpos.Date = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("Date");
			}
		}
     

       [RequiredValidationAttribute(ErrorMessage= "LineNumber is required")]
       [NumberValidationAttribute]
public int LineNumber
		{ 
		    get { return this.counterpointpos.LineNumber; }
			set
			{
			    if (value == this.counterpointpos.LineNumber) return;
				this.counterpointpos.LineNumber = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("LineNumber");
			}
		}
     

       
       
public Nullable<bool> Downloaded
		{ 
		    get { return this.counterpointpos.Downloaded; }
			set
			{
			    if (value == this.counterpointpos.Downloaded) return;
				this.counterpointpos.Downloaded = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("Downloaded");
			}
		}
     


        ChangeTrackingCollection<DTO.CounterPointPOs> _changeTracker;    
        public ChangeTrackingCollection<DTO.CounterPointPOs> ChangeTracker
        {
            get
            {
                return _changeTracker;
            }
        }

        public new TrackableEntities.TrackingState TrackingState
        {
            get
            {
                return this.DTO.TrackingState;
            }
            set
            {
                this.DTO.TrackingState = value;
                NotifyPropertyChanged("TrackingState");
            }
        }

    }
}


