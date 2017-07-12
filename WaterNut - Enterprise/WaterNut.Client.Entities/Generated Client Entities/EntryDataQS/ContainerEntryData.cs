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
using EntryDataQS.Client.DTO;
using TrackableEntities.Client;
using TrackableEntities;
using Core.Common.Validation;

namespace EntryDataQS.Client.Entities
{
       public partial class ContainerEntryData: BaseEntity<ContainerEntryData>
    {
        DTO.ContainerEntryData containerentrydata;
        public ContainerEntryData(DTO.ContainerEntryData dto )
        {
              containerentrydata = dto;
             _changeTracker = new ChangeTrackingCollection<DTO.ContainerEntryData>(containerentrydata);

        }

        public DTO.ContainerEntryData DTO
        {
            get
            {
             return containerentrydata;
            }
            set
            {
                containerentrydata = value;
            }
        }
       [RequiredValidationAttribute(ErrorMessage= "Container_ is required")]
       
public int Container_Id
		{ 
		    get { return this.containerentrydata.Container_Id; }
			set
			{
			    if (value == this.containerentrydata.Container_Id) return;
				this.containerentrydata.Container_Id = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("Container_Id");
			}
		}
     

       [RequiredValidationAttribute(ErrorMessage= "EntryData is required")]
       
                
                [MaxLength(50, ErrorMessage = "EntryDataId has a max length of 50 letters ")]
public string EntryDataId
		{ 
		    get { return this.containerentrydata.EntryDataId; }
			set
			{
			    if (value == this.containerentrydata.EntryDataId) return;
				this.containerentrydata.EntryDataId = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("EntryDataId");
			}
		}
     

       [RequiredValidationAttribute(ErrorMessage= "ContainerEntryData1 is required")]
       [NumberValidationAttribute]
public int ContainerEntryData1
		{ 
		    get { return this.containerentrydata.ContainerEntryData1; }
			set
			{
			    if (value == this.containerentrydata.ContainerEntryData1) return;
				this.containerentrydata.ContainerEntryData1 = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("ContainerEntryData1");
			}
		}
     

       private EntryDataEx _EntryDataEx;
        public  EntryDataEx EntryDataEx
		{
		    get
               { 
                  if (this.containerentrydata != null)
                   {
                       if (_EntryDataEx != null)
                       {
                           if (this.containerentrydata.EntryDataEx !=
                               _EntryDataEx.DTO)
                           {
                                if (this.containerentrydata.EntryDataEx  != null)
                               _EntryDataEx = new EntryDataEx(this.containerentrydata.EntryDataEx);
                           }
                       }
                       else
                       {
                             if (this.containerentrydata.EntryDataEx  != null)
                           _EntryDataEx = new EntryDataEx(this.containerentrydata.EntryDataEx);
                       }
                   }


             //       if (_EntryDataEx != null) return _EntryDataEx;
                       
             //       var i = new EntryDataEx(){TrackingState = TrackingState.Added};
			//		//if (this.containerentrydata.EntryDataEx == null) Debugger.Break();
			//		if (this.containerentrydata.EntryDataEx != null)
            //        {
            //           i. = this.containerentrydata.EntryDataEx;
            //        }
            //        else
            //        {
            //            this.containerentrydata.EntryDataEx = i.;
             //       }
                           
            //        _EntryDataEx = i;
                     
                    return _EntryDataEx;
               }
			set
			{
			    if (value == _EntryDataEx) return;
                _EntryDataEx = value;
                if(value != null)
                     this.containerentrydata.EntryDataEx = value.DTO;
				if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
                NotifyPropertyChanged("EntryDataEx");
			}
		}
        

       private ContainerEx _ContainerEx;
        public  ContainerEx ContainerEx
		{
		    get
               { 
                  if (this.containerentrydata != null)
                   {
                       if (_ContainerEx != null)
                       {
                           if (this.containerentrydata.ContainerEx !=
                               _ContainerEx.DTO)
                           {
                                if (this.containerentrydata.ContainerEx  != null)
                               _ContainerEx = new ContainerEx(this.containerentrydata.ContainerEx);
                           }
                       }
                       else
                       {
                             if (this.containerentrydata.ContainerEx  != null)
                           _ContainerEx = new ContainerEx(this.containerentrydata.ContainerEx);
                       }
                   }


             //       if (_ContainerEx != null) return _ContainerEx;
                       
             //       var i = new ContainerEx(){TrackingState = TrackingState.Added};
			//		//if (this.containerentrydata.ContainerEx == null) Debugger.Break();
			//		if (this.containerentrydata.ContainerEx != null)
            //        {
            //           i. = this.containerentrydata.ContainerEx;
            //        }
            //        else
            //        {
            //            this.containerentrydata.ContainerEx = i.;
             //       }
                           
            //        _ContainerEx = i;
                     
                    return _ContainerEx;
               }
			set
			{
			    if (value == _ContainerEx) return;
                _ContainerEx = value;
                if(value != null)
                     this.containerentrydata.ContainerEx = value.DTO;
				if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
                NotifyPropertyChanged("ContainerEx");
			}
		}
        


        ChangeTrackingCollection<DTO.ContainerEntryData> _changeTracker;    
        public ChangeTrackingCollection<DTO.ContainerEntryData> ChangeTracker
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


