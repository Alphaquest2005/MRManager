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
       public partial class PreviousDocumentItem: BaseEntity<PreviousDocumentItem>
    {
        DTO.PreviousDocumentItem previousdocumentitem;
        public PreviousDocumentItem(DTO.PreviousDocumentItem dto )
        {
              previousdocumentitem = dto;
             _changeTracker = new ChangeTrackingCollection<DTO.PreviousDocumentItem>(previousdocumentitem);

        }

        public DTO.PreviousDocumentItem DTO
        {
            get
            {
             return previousdocumentitem;
            }
            set
            {
                previousdocumentitem = value;
            }
        }
        


       
       
                
                
public string Amount_deducted_from_licence
		{ 
		    get { return this.previousdocumentitem.Amount_deducted_from_licence; }
			set
			{
			    if (value == this.previousdocumentitem.Amount_deducted_from_licence) return;
				this.previousdocumentitem.Amount_deducted_from_licence = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("Amount_deducted_from_licence");
			}
		}
     

       [RequiredValidationAttribute(ErrorMessage= "Item_ is required")]
       
public int Item_Id
		{ 
		    get { return this.previousdocumentitem.Item_Id; }
			set
			{
			    if (value == this.previousdocumentitem.Item_Id) return;
				this.previousdocumentitem.Item_Id = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("Item_Id");
			}
		}
     

       [RequiredValidationAttribute(ErrorMessage= "ASYCUDA_ is required")]
       
public int ASYCUDA_Id
		{ 
		    get { return this.previousdocumentitem.ASYCUDA_Id; }
			set
			{
			    if (value == this.previousdocumentitem.ASYCUDA_Id) return;
				this.previousdocumentitem.ASYCUDA_Id = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("ASYCUDA_Id");
			}
		}
     

       
       
                
                
public string Licence_number
		{ 
		    get { return this.previousdocumentitem.Licence_number; }
			set
			{
			    if (value == this.previousdocumentitem.Licence_number) return;
				this.previousdocumentitem.Licence_number = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("Licence_number");
			}
		}
     

       
       
                
                
public string Free_text_1
		{ 
		    get { return this.previousdocumentitem.Free_text_1; }
			set
			{
			    if (value == this.previousdocumentitem.Free_text_1) return;
				this.previousdocumentitem.Free_text_1 = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("Free_text_1");
			}
		}
     

       
       
                
                
public string Free_text_2
		{ 
		    get { return this.previousdocumentitem.Free_text_2; }
			set
			{
			    if (value == this.previousdocumentitem.Free_text_2) return;
				this.previousdocumentitem.Free_text_2 = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("Free_text_2");
			}
		}
     

       
       
public Nullable<int> EntryDataDetailsId
		{ 
		    get { return this.previousdocumentitem.EntryDataDetailsId; }
			set
			{
			    if (value == this.previousdocumentitem.EntryDataDetailsId) return;
				this.previousdocumentitem.EntryDataDetailsId = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("EntryDataDetailsId");
			}
		}
     

       [RequiredValidationAttribute(ErrorMessage= "LineNumber is required")]
       [NumberValidationAttribute]
public int LineNumber
		{ 
		    get { return this.previousdocumentitem.LineNumber; }
			set
			{
			    if (value == this.previousdocumentitem.LineNumber) return;
				this.previousdocumentitem.LineNumber = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("LineNumber");
			}
		}
     

       
       
public Nullable<bool> IsAssessed
		{ 
		    get { return this.previousdocumentitem.IsAssessed; }
			set
			{
			    if (value == this.previousdocumentitem.IsAssessed) return;
				this.previousdocumentitem.IsAssessed = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("IsAssessed");
			}
		}
     

       [RequiredValidationAttribute(ErrorMessage= "DPQtyAllocated is required")]
       [NumberValidationAttribute]
public double DPQtyAllocated
		{ 
		    get { return this.previousdocumentitem.DPQtyAllocated; }
			set
			{
			    if (value == this.previousdocumentitem.DPQtyAllocated) return;
				this.previousdocumentitem.DPQtyAllocated = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("DPQtyAllocated");
			}
		}
     

       [RequiredValidationAttribute(ErrorMessage= "DFQtyAllocated is required")]
       [NumberValidationAttribute]
public double DFQtyAllocated
		{ 
		    get { return this.previousdocumentitem.DFQtyAllocated; }
			set
			{
			    if (value == this.previousdocumentitem.DFQtyAllocated) return;
				this.previousdocumentitem.DFQtyAllocated = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("DFQtyAllocated");
			}
		}
     

       
       
public Nullable<System.DateTime> EntryTimeStamp
		{ 
		    get { return this.previousdocumentitem.EntryTimeStamp; }
			set
			{
			    if (value == this.previousdocumentitem.EntryTimeStamp) return;
				this.previousdocumentitem.EntryTimeStamp = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("EntryTimeStamp");
			}
		}
     

       
       
public Nullable<bool> AttributeOnlyAllocation
		{ 
		    get { return this.previousdocumentitem.AttributeOnlyAllocation; }
			set
			{
			    if (value == this.previousdocumentitem.AttributeOnlyAllocation) return;
				this.previousdocumentitem.AttributeOnlyAllocation = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("AttributeOnlyAllocation");
			}
		}
     

       
       
public Nullable<bool> DoNotAllocate
		{ 
		    get { return this.previousdocumentitem.DoNotAllocate; }
			set
			{
			    if (value == this.previousdocumentitem.DoNotAllocate) return;
				this.previousdocumentitem.DoNotAllocate = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("DoNotAllocate");
			}
		}
     

       
       
public Nullable<bool> DoNotEX
		{ 
		    get { return this.previousdocumentitem.DoNotEX; }
			set
			{
			    if (value == this.previousdocumentitem.DoNotEX) return;
				this.previousdocumentitem.DoNotEX = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("DoNotEX");
			}
		}
     

       
       [NumberValidationAttribute]
public Nullable<double> Item_price
		{ 
		    get { return this.previousdocumentitem.Item_price; }
			set
			{
			    if (value == this.previousdocumentitem.Item_price) return;
				this.previousdocumentitem.Item_price = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("Item_price");
			}
		}
     

       
       
                
                [MaxLength(50, ErrorMessage = "ItemNumber has a max length of 50 letters ")]
public string ItemNumber
		{ 
		    get { return this.previousdocumentitem.ItemNumber; }
			set
			{
			    if (value == this.previousdocumentitem.ItemNumber) return;
				this.previousdocumentitem.ItemNumber = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("ItemNumber");
			}
		}
     

       
       
                
                [MaxLength(8, ErrorMessage = "TariffCode has a max length of 8 letters ")]
public string TariffCode
		{ 
		    get { return this.previousdocumentitem.TariffCode; }
			set
			{
			    if (value == this.previousdocumentitem.TariffCode) return;
				this.previousdocumentitem.TariffCode = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("TariffCode");
			}
		}
     

       
       [NumberValidationAttribute]
public Nullable<double> DutyLiability
		{ 
		    get { return this.previousdocumentitem.DutyLiability; }
			set
			{
			    if (value == this.previousdocumentitem.DutyLiability) return;
				this.previousdocumentitem.DutyLiability = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("DutyLiability");
			}
		}
     

       
       [NumberValidationAttribute]
public Nullable<double> Total_CIF_itm
		{ 
		    get { return this.previousdocumentitem.Total_CIF_itm; }
			set
			{
			    if (value == this.previousdocumentitem.Total_CIF_itm) return;
				this.previousdocumentitem.Total_CIF_itm = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("Total_CIF_itm");
			}
		}
     

       
       [NumberValidationAttribute]
public Nullable<double> Freight
		{ 
		    get { return this.previousdocumentitem.Freight; }
			set
			{
			    if (value == this.previousdocumentitem.Freight) return;
				this.previousdocumentitem.Freight = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("Freight");
			}
		}
     

       
       [NumberValidationAttribute]
public Nullable<double> Statistical_value
		{ 
		    get { return this.previousdocumentitem.Statistical_value; }
			set
			{
			    if (value == this.previousdocumentitem.Statistical_value) return;
				this.previousdocumentitem.Statistical_value = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("Statistical_value");
			}
		}
     

       
       [NumberValidationAttribute]
public Nullable<double> PiQuantity
		{ 
		    get { return this.previousdocumentitem.PiQuantity; }
			set
			{
			    if (value == this.previousdocumentitem.PiQuantity) return;
				this.previousdocumentitem.PiQuantity = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("PiQuantity");
			}
		}
     

       
       
                
                
public string Description_of_goods
		{ 
		    get { return this.previousdocumentitem.Description_of_goods; }
			set
			{
			    if (value == this.previousdocumentitem.Description_of_goods) return;
				this.previousdocumentitem.Description_of_goods = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("Description_of_goods");
			}
		}
     

       
       
                
                
public string Commercial_Description
		{ 
		    get { return this.previousdocumentitem.Commercial_Description; }
			set
			{
			    if (value == this.previousdocumentitem.Commercial_Description) return;
				this.previousdocumentitem.Commercial_Description = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("Commercial_Description");
			}
		}
     

       
       
                
                
public string Suppplementary_unit_code
		{ 
		    get { return this.previousdocumentitem.Suppplementary_unit_code; }
			set
			{
			    if (value == this.previousdocumentitem.Suppplementary_unit_code) return;
				this.previousdocumentitem.Suppplementary_unit_code = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("Suppplementary_unit_code");
			}
		}
     

       
       [NumberValidationAttribute]
public Nullable<double> ItemQuantity
		{ 
		    get { return this.previousdocumentitem.ItemQuantity; }
			set
			{
			    if (value == this.previousdocumentitem.ItemQuantity) return;
				this.previousdocumentitem.ItemQuantity = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("ItemQuantity");
			}
		}
     

       
       
                
                
public string Number
		{ 
		    get { return this.previousdocumentitem.Number; }
			set
			{
			    if (value == this.previousdocumentitem.Number) return;
				this.previousdocumentitem.Number = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("Number");
			}
		}
     

       
       
                
                
public string DocumentType
		{ 
		    get { return this.previousdocumentitem.DocumentType; }
			set
			{
			    if (value == this.previousdocumentitem.DocumentType) return;
				this.previousdocumentitem.DocumentType = value;
                if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				NotifyPropertyChanged("DocumentType");
			}
		}
     

       private PreviousDocument _PreviousDocument;
        public  PreviousDocument PreviousDocument
		{
		    get
               { 
                  if (this.previousdocumentitem != null)
                   {
                       if (_PreviousDocument != null)
                       {
                           if (this.previousdocumentitem.PreviousDocument !=
                               _PreviousDocument.DTO)
                           {
                                if (this.previousdocumentitem.PreviousDocument  != null)
                               _PreviousDocument = new PreviousDocument(this.previousdocumentitem.PreviousDocument);
                           }
                       }
                       else
                       {
                             if (this.previousdocumentitem.PreviousDocument  != null)
                           _PreviousDocument = new PreviousDocument(this.previousdocumentitem.PreviousDocument);
                       }
                   }


             //       if (_PreviousDocument != null) return _PreviousDocument;
                       
             //       var i = new PreviousDocument(){TrackingState = TrackingState.Added};
			//		//if (this.previousdocumentitem.PreviousDocument == null) Debugger.Break();
			//		if (this.previousdocumentitem.PreviousDocument != null)
            //        {
            //           i. = this.previousdocumentitem.PreviousDocument;
            //        }
            //        else
            //        {
            //            this.previousdocumentitem.PreviousDocument = i.;
             //       }
                           
            //        _PreviousDocument = i;
                     
                    return _PreviousDocument;
               }
			set
			{
			    if (value == _PreviousDocument) return;
                _PreviousDocument = value;
                if(value != null)
                     this.previousdocumentitem.PreviousDocument = value.DTO;
				if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
                NotifyPropertyChanged("PreviousDocument");
			}
		}
        

        ObservableCollection<PreviousItemsEx> _PreviousItemsExes = null;
        public  ObservableCollection<PreviousItemsEx> PreviousItemsExes
		{
            
		    get 
				{ 
					if(_PreviousItemsExes != null) return _PreviousItemsExes;
					//if (this.previousdocumentitem.PreviousItemsExes == null) Debugger.Break();
					if(this.previousdocumentitem.PreviousItemsExes != null)
					{
						_PreviousItemsExes = new ObservableCollection<PreviousItemsEx>(this.previousdocumentitem.PreviousItemsExes.Select(x => new PreviousItemsEx(x)));
					}
					
						_PreviousItemsExes.CollectionChanged += PreviousItemsExes_CollectionChanged; 
					
					return _PreviousItemsExes; 
				}
			set
			{
			    if (Equals(value, _PreviousItemsExes)) return;
				if (value != null)
					this.previousdocumentitem.PreviousItemsExes = new ChangeTrackingCollection<DTO.PreviousItemsEx>(value.Select(x => x.DTO).ToList());
                _PreviousItemsExes = value;
				if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				if (_PreviousItemsExes != null)
				_PreviousItemsExes.CollectionChanged += PreviousItemsExes_CollectionChanged;               
				NotifyPropertyChanged("PreviousItemsExes");
			}
		}
        
        void PreviousItemsExes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (PreviousItemsEx itm in e.NewItems)
                    {
                        if (itm != null)
                        previousdocumentitem.PreviousItemsExes.Add(itm.DTO);
                    }
                    if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (PreviousItemsEx itm in e.OldItems)
                    {
                        if (itm != null)
                        previousdocumentitem.PreviousItemsExes.Remove(itm.DTO);
                    }
					if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
                    break;
                
            }
        }

        ObservableCollection<PreviousItemsEx> _PreviousItemEx = null;
        public  ObservableCollection<PreviousItemsEx> PreviousItemEx
		{
            
		    get 
				{ 
					if(_PreviousItemEx != null) return _PreviousItemEx;
					//if (this.previousdocumentitem.PreviousItemEx == null) Debugger.Break();
					if(this.previousdocumentitem.PreviousItemEx != null)
					{
						_PreviousItemEx = new ObservableCollection<PreviousItemsEx>(this.previousdocumentitem.PreviousItemEx.Select(x => new PreviousItemsEx(x)));
					}
					
						_PreviousItemEx.CollectionChanged += PreviousItemEx_CollectionChanged; 
					
					return _PreviousItemEx; 
				}
			set
			{
			    if (Equals(value, _PreviousItemEx)) return;
				if (value != null)
					this.previousdocumentitem.PreviousItemEx = new ChangeTrackingCollection<DTO.PreviousItemsEx>(value.Select(x => x.DTO).ToList());
                _PreviousItemEx = value;
				if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
				if (_PreviousItemEx != null)
				_PreviousItemEx.CollectionChanged += PreviousItemEx_CollectionChanged;               
				NotifyPropertyChanged("PreviousItemEx");
			}
		}
        
        void PreviousItemEx_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (PreviousItemsEx itm in e.NewItems)
                    {
                        if (itm != null)
                        previousdocumentitem.PreviousItemEx.Add(itm.DTO);
                    }
                    if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (PreviousItemsEx itm in e.OldItems)
                    {
                        if (itm != null)
                        previousdocumentitem.PreviousItemEx.Remove(itm.DTO);
                    }
					if(this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged)this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
                    break;
                
            }
        }


        ChangeTrackingCollection<DTO.PreviousDocumentItem> _changeTracker;    
        public ChangeTrackingCollection<DTO.PreviousDocumentItem> ChangeTracker
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


