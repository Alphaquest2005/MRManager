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
       public partial class InventoryItemsEx: BaseEntity<InventoryItemsEx>
    {
        DTO.InventoryItemsEx inventoryitemsex;
        public InventoryItemsEx(DTO.InventoryItemsEx dto )
        {
              inventoryitemsex = dto;
             _changeTracker = new ChangeTrackingCollection<DTO.InventoryItemsEx>(inventoryitemsex);

        }

        public DTO.InventoryItemsEx DTO
        {
            get
            {
             return inventoryitemsex;
            }
            set
            {
                inventoryitemsex = value;
            }
        }
        


       [RequiredValidationAttribute(ErrorMessage= "ItemNumber is required")]
       
                
                [MaxLength(50, ErrorMessage = "ItemNumber has a max length of 50 letters ")]
public string ItemNumber
		{ 
		    get { return this.inventoryitemsex.ItemNumber; }
			set
			{
			    if (value == this.inventoryitemsex.ItemNumber) return;
				this.inventoryitemsex.ItemNumber = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("ItemNumber");
			}
		}
     

       [RequiredValidationAttribute(ErrorMessage= "Description is required")]
       
                
                
public string Description
		{ 
		    get { return this.inventoryitemsex.Description; }
			set
			{
			    if (value == this.inventoryitemsex.Description) return;
				this.inventoryitemsex.Description = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("Description");
			}
		}
     

       
       
                
                [MaxLength(60, ErrorMessage = "Category has a max length of 60 letters ")]
public string Category
		{ 
		    get { return this.inventoryitemsex.Category; }
			set
			{
			    if (value == this.inventoryitemsex.Category) return;
				this.inventoryitemsex.Category = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("Category");
			}
		}
     

       
       
                
                [MaxLength(8, ErrorMessage = "TariffCode has a max length of 8 letters ")]
public string TariffCode
		{ 
		    get { return this.inventoryitemsex.TariffCode; }
			set
			{
			    if (value == this.inventoryitemsex.TariffCode) return;
				this.inventoryitemsex.TariffCode = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("TariffCode");
			}
		}
     

       
       
public Nullable<System.DateTime> EntryTimeStamp
		{ 
		    get { return this.inventoryitemsex.EntryTimeStamp; }
			set
			{
			    if (value == this.inventoryitemsex.EntryTimeStamp) return;
				this.inventoryitemsex.EntryTimeStamp = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("EntryTimeStamp");
			}
		}
     

        ObservableCollection<EntryDataDetailsEx> _EntryDataDetailsExs = null;
        public  ObservableCollection<EntryDataDetailsEx> EntryDataDetailsExs
		{
            
		    get 
				{ 
					if(_EntryDataDetailsExs != null) return _EntryDataDetailsExs;
					//if (this.inventoryitemsex.EntryDataDetailsExs == null) Debugger.Break();
					if(this.inventoryitemsex.EntryDataDetailsExs != null)
					{
						_EntryDataDetailsExs = new ObservableCollection<EntryDataDetailsEx>(this.inventoryitemsex.EntryDataDetailsExs.Select(x => new EntryDataDetailsEx(x)));
					}
					
						_EntryDataDetailsExs.CollectionChanged += EntryDataDetailsExs_CollectionChanged; 
					
					return _EntryDataDetailsExs; 
				}
			set
			{
			    if (Equals(value, _EntryDataDetailsExs)) return;
				if (value != null)
					this.inventoryitemsex.EntryDataDetailsExs = new ChangeTrackingCollection<DTO.EntryDataDetailsEx>(value.Select(x => x.DTO).ToList());
                _EntryDataDetailsExs = value;
				if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				if (_EntryDataDetailsExs != null)
				_EntryDataDetailsExs.CollectionChanged += EntryDataDetailsExs_CollectionChanged;               
				NotifyPropertyChanged("EntryDataDetailsExs");
			}
		}
        
        void EntryDataDetailsExs_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (EntryDataDetailsEx itm in e.NewItems)
                    {
                        if (itm != null)
                        inventoryitemsex.EntryDataDetailsExs.Add(itm.DTO);
                    }
                    if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (EntryDataDetailsEx itm in e.OldItems)
                    {
                        if (itm != null)
                        inventoryitemsex.EntryDataDetailsExs.Remove(itm.DTO);
                    }
					if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
                    break;
                
            }
        }


        ChangeTrackingCollection<DTO.InventoryItemsEx> _changeTracker;    
        public ChangeTrackingCollection<DTO.InventoryItemsEx> ChangeTracker
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


