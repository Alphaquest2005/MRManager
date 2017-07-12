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
using InventoryQS.Client.DTO;
using TrackableEntities.Client;
using TrackableEntities;
using Core.Common.Validation;

namespace InventoryQS.Client.Entities
{
       public partial class TariffSupUnitLkps: BaseEntity<TariffSupUnitLkps>
    {
        DTO.TariffSupUnitLkps tariffsupunitlkps;
        public TariffSupUnitLkps(DTO.TariffSupUnitLkps dto )
        {
              tariffsupunitlkps = dto;
             _changeTracker = new ChangeTrackingCollection<DTO.TariffSupUnitLkps>(tariffsupunitlkps);

        }

        public DTO.TariffSupUnitLkps DTO
        {
            get
            {
             return tariffsupunitlkps;
            }
            set
            {
                tariffsupunitlkps = value;
            }
        }
       [RequiredValidationAttribute(ErrorMessage= "TariffCategoryCode is required")]
       
                
                [MaxLength(8, ErrorMessage = "TariffCategoryCode has a max length of 8 letters ")]
public string TariffCategoryCode
		{ 
		    get { return this.tariffsupunitlkps.TariffCategoryCode; }
			set
			{
			    if (value == this.tariffsupunitlkps.TariffCategoryCode) return;
				this.tariffsupunitlkps.TariffCategoryCode = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("TariffCategoryCode");
			}
		}
     

       
       
                
                [MaxLength(50, ErrorMessage = "SuppUnitCode2 has a max length of 50 letters ")]
public string SuppUnitCode2
		{ 
		    get { return this.tariffsupunitlkps.SuppUnitCode2; }
			set
			{
			    if (value == this.tariffsupunitlkps.SuppUnitCode2) return;
				this.tariffsupunitlkps.SuppUnitCode2 = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("SuppUnitCode2");
			}
		}
     

       
       
                
                [MaxLength(50, ErrorMessage = "SuppUnitName2 has a max length of 50 letters ")]
public string SuppUnitName2
		{ 
		    get { return this.tariffsupunitlkps.SuppUnitName2; }
			set
			{
			    if (value == this.tariffsupunitlkps.SuppUnitName2) return;
				this.tariffsupunitlkps.SuppUnitName2 = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("SuppUnitName2");
			}
		}
     

       [RequiredValidationAttribute(ErrorMessage= "SuppQty is required")]
       [NumberValidationAttribute]
public double SuppQty
		{ 
		    get { return this.tariffsupunitlkps.SuppQty; }
			set
			{
			    if (value == this.tariffsupunitlkps.SuppQty) return;
				this.tariffsupunitlkps.SuppQty = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("SuppQty");
			}
		}
     

       [RequiredValidationAttribute(ErrorMessage= " is required")]
       
public int Id
		{ 
		    get { return this.tariffsupunitlkps.Id; }
			set
			{
			    if (value == this.tariffsupunitlkps.Id) return;
				this.tariffsupunitlkps.Id = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("Id");
			}
		}
     

       private TariffCategory _TariffCategory;
        public  TariffCategory TariffCategory
		{
		    get
               { 
                  if (this.tariffsupunitlkps != null)
                   {
                       if (_TariffCategory != null)
                       {
                           if (this.tariffsupunitlkps.TariffCategory !=
                               _TariffCategory.DTO)
                           {
                                if (this.tariffsupunitlkps.TariffCategory  != null)
                               _TariffCategory = new TariffCategory(this.tariffsupunitlkps.TariffCategory);
                           }
                       }
                       else
                       {
                             if (this.tariffsupunitlkps.TariffCategory  != null)
                           _TariffCategory = new TariffCategory(this.tariffsupunitlkps.TariffCategory);
                       }
                   }


             //       if (_TariffCategory != null) return _TariffCategory;
                       
             //       var i = new TariffCategory(){TrackingState = TrackingState.Added};
			//		//if (this.tariffsupunitlkps.TariffCategory == null) Debugger.Break();
			//		if (this.tariffsupunitlkps.TariffCategory != null)
            //        {
            //           i. = this.tariffsupunitlkps.TariffCategory;
            //        }
            //        else
            //        {
            //            this.tariffsupunitlkps.TariffCategory = i.;
             //       }
                           
            //        _TariffCategory = i;
                     
                    return _TariffCategory;
               }
			set
			{
			    if (value == _TariffCategory) return;
                _TariffCategory = value;
                if(value != null)
                     this.tariffsupunitlkps.TariffCategory = value.DTO;
				if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
                NotifyPropertyChanged("TariffCategory");
			}
		}
        


        ChangeTrackingCollection<DTO.TariffSupUnitLkps> _changeTracker;    
        public ChangeTrackingCollection<DTO.TariffSupUnitLkps> ChangeTracker
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


