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
using PreviousDocumentQS.Client.DTO;
using TrackableEntities.Client;
using TrackableEntities;
using Core.Common.Validation;

namespace PreviousDocumentQS.Client.Entities
{
       public partial class PreviousItemsEx: BaseEntity<PreviousItemsEx>
    {
        DTO.PreviousItemsEx previousitemsex;
        public PreviousItemsEx(DTO.PreviousItemsEx dto )
        {
              previousitemsex = dto;
             _changeTracker = new ChangeTrackingCollection<DTO.PreviousItemsEx>(previousitemsex);

        }

        public DTO.PreviousItemsEx DTO
        {
            get
            {
             return previousitemsex;
            }
            set
            {
                previousitemsex = value;
            }
        }
       
       
                
                
public string Packages_number
		{ 
		    get { return this.previousitemsex.Packages_number; }
			set
			{
			    if (value == this.previousitemsex.Packages_number) return;
				this.previousitemsex.Packages_number = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("Packages_number");
			}
		}
     

       
       
                
                
public string Previous_Packages_number
		{ 
		    get { return this.previousitemsex.Previous_Packages_number; }
			set
			{
			    if (value == this.previousitemsex.Previous_Packages_number) return;
				this.previousitemsex.Previous_Packages_number = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("Previous_Packages_number");
			}
		}
     

       
       
                
                
public string Hs_code
		{ 
		    get { return this.previousitemsex.Hs_code; }
			set
			{
			    if (value == this.previousitemsex.Hs_code) return;
				this.previousitemsex.Hs_code = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("Hs_code");
			}
		}
     

       
       
                
                
public string Commodity_code
		{ 
		    get { return this.previousitemsex.Commodity_code; }
			set
			{
			    if (value == this.previousitemsex.Commodity_code) return;
				this.previousitemsex.Commodity_code = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("Commodity_code");
			}
		}
     

       
       
                
                
public string Previous_item_number
		{ 
		    get { return this.previousitemsex.Previous_item_number; }
			set
			{
			    if (value == this.previousitemsex.Previous_item_number) return;
				this.previousitemsex.Previous_item_number = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("Previous_item_number");
			}
		}
     

       
       
                
                
public string Goods_origin
		{ 
		    get { return this.previousitemsex.Goods_origin; }
			set
			{
			    if (value == this.previousitemsex.Goods_origin) return;
				this.previousitemsex.Goods_origin = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("Goods_origin");
			}
		}
     

       [RequiredValidationAttribute(ErrorMessage= "Net_weight is required")]
       [NumberValidationAttribute]
public double Net_weight
		{ 
		    get { return this.previousitemsex.Net_weight; }
			set
			{
			    if (value == this.previousitemsex.Net_weight) return;
				this.previousitemsex.Net_weight = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("Net_weight");
			}
		}
     

       [RequiredValidationAttribute(ErrorMessage= "Prev_net_weight is required")]
       [NumberValidationAttribute]
public double Prev_net_weight
		{ 
		    get { return this.previousitemsex.Prev_net_weight; }
			set
			{
			    if (value == this.previousitemsex.Prev_net_weight) return;
				this.previousitemsex.Prev_net_weight = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("Prev_net_weight");
			}
		}
     

       
       
                
                
public string Prev_reg_ser
		{ 
		    get { return this.previousitemsex.Prev_reg_ser; }
			set
			{
			    if (value == this.previousitemsex.Prev_reg_ser) return;
				this.previousitemsex.Prev_reg_ser = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("Prev_reg_ser");
			}
		}
     

       
       
                
                
public string Prev_reg_nbr
		{ 
		    get { return this.previousitemsex.Prev_reg_nbr; }
			set
			{
			    if (value == this.previousitemsex.Prev_reg_nbr) return;
				this.previousitemsex.Prev_reg_nbr = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("Prev_reg_nbr");
			}
		}
     

       
       
                
                
public string Prev_reg_dat
		{ 
		    get { return this.previousitemsex.Prev_reg_dat; }
			set
			{
			    if (value == this.previousitemsex.Prev_reg_dat) return;
				this.previousitemsex.Prev_reg_dat = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("Prev_reg_dat");
			}
		}
     

       
       
                
                
public string Prev_reg_cuo
		{ 
		    get { return this.previousitemsex.Prev_reg_cuo; }
			set
			{
			    if (value == this.previousitemsex.Prev_reg_cuo) return;
				this.previousitemsex.Prev_reg_cuo = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("Prev_reg_cuo");
			}
		}
     

       [RequiredValidationAttribute(ErrorMessage= "Suplementary_Quantity is required")]
       [NumberValidationAttribute]
public double Suplementary_Quantity
		{ 
		    get { return this.previousitemsex.Suplementary_Quantity; }
			set
			{
			    if (value == this.previousitemsex.Suplementary_Quantity) return;
				this.previousitemsex.Suplementary_Quantity = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("Suplementary_Quantity");
			}
		}
     

       [RequiredValidationAttribute(ErrorMessage= "Preveious_suplementary_quantity is required")]
       [NumberValidationAttribute]
public double Preveious_suplementary_quantity
		{ 
		    get { return this.previousitemsex.Preveious_suplementary_quantity; }
			set
			{
			    if (value == this.previousitemsex.Preveious_suplementary_quantity) return;
				this.previousitemsex.Preveious_suplementary_quantity = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("Preveious_suplementary_quantity");
			}
		}
     

       [RequiredValidationAttribute(ErrorMessage= "Current_value is required")]
       [NumberValidationAttribute]
public double Current_value
		{ 
		    get { return this.previousitemsex.Current_value; }
			set
			{
			    if (value == this.previousitemsex.Current_value) return;
				this.previousitemsex.Current_value = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("Current_value");
			}
		}
     

       [RequiredValidationAttribute(ErrorMessage= "Previous_value is required")]
       [NumberValidationAttribute]
public double Previous_value
		{ 
		    get { return this.previousitemsex.Previous_value; }
			set
			{
			    if (value == this.previousitemsex.Previous_value) return;
				this.previousitemsex.Previous_value = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("Previous_value");
			}
		}
     

       
       
                
                
public string Current_item_number
		{ 
		    get { return this.previousitemsex.Current_item_number; }
			set
			{
			    if (value == this.previousitemsex.Current_item_number) return;
				this.previousitemsex.Current_item_number = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("Current_item_number");
			}
		}
     

       [RequiredValidationAttribute(ErrorMessage= "PreviousItem_ is required")]
       
public int PreviousItem_Id
		{ 
		    get { return this.previousitemsex.PreviousItem_Id; }
			set
			{
			    if (value == this.previousitemsex.PreviousItem_Id) return;
				this.previousitemsex.PreviousItem_Id = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("PreviousItem_Id");
			}
		}
     

       
       
public Nullable<int> ASYCUDA_Id
		{ 
		    get { return this.previousitemsex.ASYCUDA_Id; }
			set
			{
			    if (value == this.previousitemsex.ASYCUDA_Id) return;
				this.previousitemsex.ASYCUDA_Id = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("ASYCUDA_Id");
			}
		}
     

       [RequiredValidationAttribute(ErrorMessage= "QtyAllocated is required")]
       [NumberValidationAttribute]
public double QtyAllocated
		{ 
		    get { return this.previousitemsex.QtyAllocated; }
			set
			{
			    if (value == this.previousitemsex.QtyAllocated) return;
				this.previousitemsex.QtyAllocated = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("QtyAllocated");
			}
		}
     

       [RequiredValidationAttribute(ErrorMessage= "RndCurrent_Value is required")]
       [NumberValidationAttribute]
public double RndCurrent_Value
		{ 
		    get { return this.previousitemsex.RndCurrent_Value; }
			set
			{
			    if (value == this.previousitemsex.RndCurrent_Value) return;
				this.previousitemsex.RndCurrent_Value = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("RndCurrent_Value");
			}
		}
     

       
       
                
                
public string CNumber
		{ 
		    get { return this.previousitemsex.CNumber; }
			set
			{
			    if (value == this.previousitemsex.CNumber) return;
				this.previousitemsex.CNumber = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("CNumber");
			}
		}
     

       
       
public Nullable<System.DateTime> RegistrationDate
		{ 
		    get { return this.previousitemsex.RegistrationDate; }
			set
			{
			    if (value == this.previousitemsex.RegistrationDate) return;
				this.previousitemsex.RegistrationDate = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("RegistrationDate");
			}
		}
     

       
       
public Nullable<int> PreviousDocumentItemId
		{ 
		    get { return this.previousitemsex.PreviousDocumentItemId; }
			set
			{
			    if (value == this.previousitemsex.PreviousDocumentItemId) return;
				this.previousitemsex.PreviousDocumentItemId = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("PreviousDocumentItemId");
			}
		}
     

       
       
public Nullable<int> AsycudaDocumentItemId
		{ 
		    get { return this.previousitemsex.AsycudaDocumentItemId; }
			set
			{
			    if (value == this.previousitemsex.AsycudaDocumentItemId) return;
				this.previousitemsex.AsycudaDocumentItemId = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("AsycudaDocumentItemId");
			}
		}
     

       private PreviousDocumentItem _PreviousDocumentItem;
        public  PreviousDocumentItem PreviousDocumentItem
		{
		    get
               { 
                  if (this.previousitemsex != null)
                   {
                       if (_PreviousDocumentItem != null)
                       {
                           if (this.previousitemsex.PreviousDocumentItem !=
                               _PreviousDocumentItem.DTO)
                           {
                                if (this.previousitemsex.PreviousDocumentItem  != null)
                               _PreviousDocumentItem = new PreviousDocumentItem(this.previousitemsex.PreviousDocumentItem);
                           }
                       }
                       else
                       {
                             if (this.previousitemsex.PreviousDocumentItem  != null)
                           _PreviousDocumentItem = new PreviousDocumentItem(this.previousitemsex.PreviousDocumentItem);
                       }
                   }


             //       if (_PreviousDocumentItem != null) return _PreviousDocumentItem;
                       
             //       var i = new PreviousDocumentItem(){TrackingState = TrackingState.Added};
			//		//if (this.previousitemsex.PreviousDocumentItem == null) Debugger.Break();
			//		if (this.previousitemsex.PreviousDocumentItem != null)
            //        {
            //           i. = this.previousitemsex.PreviousDocumentItem;
            //        }
            //        else
            //        {
            //            this.previousitemsex.PreviousDocumentItem = i.;
             //       }
                           
            //        _PreviousDocumentItem = i;
                     
                    return _PreviousDocumentItem;
               }
			set
			{
			    if (value == _PreviousDocumentItem) return;
                _PreviousDocumentItem = value;
                if(value != null)
                     this.previousitemsex.PreviousDocumentItem = value.DTO;
				if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
                NotifyPropertyChanged("PreviousDocumentItem");
			}
		}
        

       private PreviousDocumentItem _AsycudaDocumentItem;
        public  PreviousDocumentItem AsycudaDocumentItem
		{
		    get
               { 
                  if (this.previousitemsex != null)
                   {
                       if (_AsycudaDocumentItem != null)
                       {
                           if (this.previousitemsex.AsycudaDocumentItem !=
                               _AsycudaDocumentItem.DTO)
                           {
                                if (this.previousitemsex.AsycudaDocumentItem  != null)
                               _AsycudaDocumentItem = new PreviousDocumentItem(this.previousitemsex.AsycudaDocumentItem);
                           }
                       }
                       else
                       {
                             if (this.previousitemsex.AsycudaDocumentItem  != null)
                           _AsycudaDocumentItem = new PreviousDocumentItem(this.previousitemsex.AsycudaDocumentItem);
                       }
                   }


             //       if (_AsycudaDocumentItem != null) return _AsycudaDocumentItem;
                       
             //       var i = new PreviousDocumentItem(){TrackingState = TrackingState.Added};
			//		//if (this.previousitemsex.AsycudaDocumentItem == null) Debugger.Break();
			//		if (this.previousitemsex.AsycudaDocumentItem != null)
            //        {
            //           i. = this.previousitemsex.AsycudaDocumentItem;
            //        }
            //        else
            //        {
            //            this.previousitemsex.AsycudaDocumentItem = i.;
             //       }
                           
            //        _AsycudaDocumentItem = i;
                     
                    return _AsycudaDocumentItem;
               }
			set
			{
			    if (value == _AsycudaDocumentItem) return;
                _AsycudaDocumentItem = value;
                if(value != null)
                     this.previousitemsex.AsycudaDocumentItem = value.DTO;
				if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
                NotifyPropertyChanged("AsycudaDocumentItem");
			}
		}
        


        ChangeTrackingCollection<DTO.PreviousItemsEx> _changeTracker;    
        public ChangeTrackingCollection<DTO.PreviousItemsEx> ChangeTracker
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

