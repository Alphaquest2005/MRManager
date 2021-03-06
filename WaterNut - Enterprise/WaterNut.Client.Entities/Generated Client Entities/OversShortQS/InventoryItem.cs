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
using OversShortQS.Client.DTO;
using TrackableEntities.Client;
using TrackableEntities;
using Core.Common.Validation;

namespace OversShortQS.Client.Entities
{
       public partial class InventoryItem: BaseEntity<InventoryItem>
    {
        DTO.InventoryItem inventoryitem;
        public InventoryItem(DTO.InventoryItem dto )
        {
              inventoryitem = dto;
             _changeTracker = new ChangeTrackingCollection<DTO.InventoryItem>(inventoryitem);

        }

        public DTO.InventoryItem DTO
        {
            get
            {
             return inventoryitem;
            }
            set
            {
                inventoryitem = value;
            }
        }
        


       [RequiredValidationAttribute(ErrorMessage= "ItemNumber is required")]
       
                
                [MaxLength(50, ErrorMessage = "ItemNumber has a max length of 50 letters ")]
public string ItemNumber
		{ 
		    get { return this.inventoryitem.ItemNumber; }
			set
			{
			    if (value == this.inventoryitem.ItemNumber) return;
				this.inventoryitem.ItemNumber = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("ItemNumber");
			}
		}
     

       [RequiredValidationAttribute(ErrorMessage= "Description is required")]
       
                
                
public string Description
		{ 
		    get { return this.inventoryitem.Description; }
			set
			{
			    if (value == this.inventoryitem.Description) return;
				this.inventoryitem.Description = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("Description");
			}
		}
     

       
       
                
                [MaxLength(60, ErrorMessage = "Category has a max length of 60 letters ")]
public string Category
		{ 
		    get { return this.inventoryitem.Category; }
			set
			{
			    if (value == this.inventoryitem.Category) return;
				this.inventoryitem.Category = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("Category");
			}
		}
     

       
       
                
                [MaxLength(8, ErrorMessage = "TariffCode has a max length of 8 letters ")]
public string TariffCode
		{ 
		    get { return this.inventoryitem.TariffCode; }
			set
			{
			    if (value == this.inventoryitem.TariffCode) return;
				this.inventoryitem.TariffCode = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("TariffCode");
			}
		}
     

       
       
public Nullable<System.DateTime> EntryTimeStamp
		{ 
		    get { return this.inventoryitem.EntryTimeStamp; }
			set
			{
			    if (value == this.inventoryitem.EntryTimeStamp) return;
				this.inventoryitem.EntryTimeStamp = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("EntryTimeStamp");
			}
		}
     

        ObservableCollection<OverShortDetailsEX> _OverShortDetailsEXes = null;
        public  ObservableCollection<OverShortDetailsEX> OverShortDetailsEXes
		{
            
		    get 
				{ 
					if(_OverShortDetailsEXes != null) return _OverShortDetailsEXes;
					//if (this.inventoryitem.OverShortDetailsEXes == null) Debugger.Break();
					if(this.inventoryitem.OverShortDetailsEXes != null)
					{
						_OverShortDetailsEXes = new ObservableCollection<OverShortDetailsEX>(this.inventoryitem.OverShortDetailsEXes.Select(x => new OverShortDetailsEX(x)));
					}
					
						_OverShortDetailsEXes.CollectionChanged += OverShortDetailsEXes_CollectionChanged; 
					
					return _OverShortDetailsEXes; 
				}
			set
			{
			    if (Equals(value, _OverShortDetailsEXes)) return;
				if (value != null)
					this.inventoryitem.OverShortDetailsEXes = new ChangeTrackingCollection<DTO.OverShortDetailsEX>(value.Select(x => x.DTO).ToList());
                _OverShortDetailsEXes = value;
				if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				if (_OverShortDetailsEXes != null)
				_OverShortDetailsEXes.CollectionChanged += OverShortDetailsEXes_CollectionChanged;               
				NotifyPropertyChanged("OverShortDetailsEXes");
			}
		}
        
        void OverShortDetailsEXes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (OverShortDetailsEX itm in e.NewItems)
                    {
                        if (itm != null)
                        inventoryitem.OverShortDetailsEXes.Add(itm.DTO);
                    }
                    if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (OverShortDetailsEX itm in e.OldItems)
                    {
                        if (itm != null)
                        inventoryitem.OverShortDetailsEXes.Remove(itm.DTO);
                    }
					if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
                    break;
                
            }
        }


        ChangeTrackingCollection<DTO.InventoryItem> _changeTracker;    
        public ChangeTrackingCollection<DTO.InventoryItem> ChangeTracker
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


