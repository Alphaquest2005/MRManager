﻿// <autogenerated>
//   This file was generated by T4 code generator AllClientEntities.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
//using Newtonsoft.Json;
using TrackableEntities;
using TrackableEntities.Client;
using Core.Common.Client.DTO;

namespace InventoryQS.Client.DTO
{

   // [JsonObject(IsReference = true)]
    [DataContract(IsReference = true, Namespace="http://www.insight-software.com/WaterNut")]
    public partial class EntryDataDetailsEx : BaseEntity<EntryDataDetailsEx> , ITrackable, IEquatable<EntryDataDetailsEx>
    {
        [DataMember]
        public int EntryDataDetailsId
		{ 
		    get { return _EntryDataDetailsId; }
			set
			{
			    if (value == _EntryDataDetailsId) return;
				_EntryDataDetailsId = value;
				NotifyPropertyChanged();//m => this.EntryDataDetailsId
			}
		}
        private int _EntryDataDetailsId;

        [DataMember]
        public string EntryDataId
		{ 
		    get { return _EntryDataId; }
			set
			{
			    if (value == _EntryDataId) return;
				_EntryDataId = value;
				NotifyPropertyChanged();//m => this.EntryDataId
			}
		}
        private string _EntryDataId;

        [DataMember]
        public Nullable<int> LineNumber
		{ 
		    get { return _LineNumber; }
			set
			{
			    if (value == _LineNumber) return;
				_LineNumber = value;
				NotifyPropertyChanged();//m => this.LineNumber
			}
		}
        private Nullable<int> _LineNumber;

        [DataMember]
        public string ItemNumber
		{ 
		    get { return _ItemNumber; }
			set
			{
			    if (value == _ItemNumber) return;
				_ItemNumber = value;
				NotifyPropertyChanged();//m => this.ItemNumber
			}
		}
        private string _ItemNumber;

        [DataMember]
        public float Quantity
		{ 
		    get { return _Quantity; }
			set
			{
			    if (value == _Quantity) return;
				_Quantity = value;
				NotifyPropertyChanged();//m => this.Quantity
			}
		}
        private float _Quantity;

        [DataMember]
        public string Units
		{ 
		    get { return _Units; }
			set
			{
			    if (value == _Units) return;
				_Units = value;
				NotifyPropertyChanged();//m => this.Units
			}
		}
        private string _Units;

        [DataMember]
        public string ItemDescription
		{ 
		    get { return _ItemDescription; }
			set
			{
			    if (value == _ItemDescription) return;
				_ItemDescription = value;
				NotifyPropertyChanged();//m => this.ItemDescription
			}
		}
        private string _ItemDescription;

        [DataMember]
        public float Cost
		{ 
		    get { return _Cost; }
			set
			{
			    if (value == _Cost) return;
				_Cost = value;
				NotifyPropertyChanged();//m => this.Cost
			}
		}
        private float _Cost;

        [DataMember]
        public double QtyAllocated
		{ 
		    get { return _QtyAllocated; }
			set
			{
			    if (value == _QtyAllocated) return;
				_QtyAllocated = value;
				NotifyPropertyChanged();//m => this.QtyAllocated
			}
		}
        private double _QtyAllocated;

        [DataMember]
        public float UnitWeight
		{ 
		    get { return _UnitWeight; }
			set
			{
			    if (value == _UnitWeight) return;
				_UnitWeight = value;
				NotifyPropertyChanged();//m => this.UnitWeight
			}
		}
        private float _UnitWeight;

        [DataMember]
        public Nullable<bool> DoNotAllocate
		{ 
		    get { return _DoNotAllocate; }
			set
			{
			    if (value == _DoNotAllocate) return;
				_DoNotAllocate = value;
				NotifyPropertyChanged();//m => this.DoNotAllocate
			}
		}
        private Nullable<bool> _DoNotAllocate;

        [DataMember]
        public string TariffCode
		{ 
		    get { return _TariffCode; }
			set
			{
			    if (value == _TariffCode) return;
				_TariffCode = value;
				NotifyPropertyChanged();//m => this.TariffCode
			}
		}
        private string _TariffCode;

        [DataMember]
        public string CNumber
		{ 
		    get { return _CNumber; }
			set
			{
			    if (value == _CNumber) return;
				_CNumber = value;
				NotifyPropertyChanged();//m => this.CNumber
			}
		}
        private string _CNumber;

        [DataMember]
        public Nullable<int> CLineNumber
		{ 
		    get { return _CLineNumber; }
			set
			{
			    if (value == _CLineNumber) return;
				_CLineNumber = value;
				NotifyPropertyChanged();//m => this.CLineNumber
			}
		}
        private Nullable<int> _CLineNumber;

        [DataMember]
        public Nullable<bool> Downloaded
		{ 
		    get { return _Downloaded; }
			set
			{
			    if (value == _Downloaded) return;
				_Downloaded = value;
				NotifyPropertyChanged();//m => this.Downloaded
			}
		}
        private Nullable<bool> _Downloaded;

        [DataMember]
        public string DutyFreePaid
		{ 
		    get { return _DutyFreePaid; }
			set
			{
			    if (value == _DutyFreePaid) return;
				_DutyFreePaid = value;
				NotifyPropertyChanged();//m => this.DutyFreePaid
			}
		}
        private string _DutyFreePaid;

        [DataMember]
        public Nullable<float> Total
		{ 
		    get { return _Total; }
			set
			{
			    if (value == _Total) return;
				_Total = value;
				NotifyPropertyChanged();//m => this.Total
			}
		}
        private Nullable<float> _Total;

        [DataMember]
        public Nullable<int> AsycudaDocumentSetId
		{ 
		    get { return _AsycudaDocumentSetId; }
			set
			{
			    if (value == _AsycudaDocumentSetId) return;
				_AsycudaDocumentSetId = value;
				NotifyPropertyChanged();//m => this.AsycudaDocumentSetId
			}
		}
        private Nullable<int> _AsycudaDocumentSetId;

       
        [DataMember]
        public InventoryItemsEx InventoryItemsEx
		{
		    get { return _InventoryItemsEx; }
			set
			{
			    if (value == _InventoryItemsEx) return;
				_InventoryItemsEx = value;
                InventoryItemsExChangeTracker = _InventoryItemsEx == null ? null
                    : new ChangeTrackingCollection<InventoryItemsEx> { _InventoryItemsEx };
				NotifyPropertyChanged();//m => this.InventoryItemsEx
			}
		}
        private InventoryItemsEx _InventoryItemsEx;
        private ChangeTrackingCollection<InventoryItemsEx> InventoryItemsExChangeTracker { get; set; }

   //     [DataMember]
   //     public TrackingState TrackingState { get; set; }

   //     [DataMember]
   //     public ICollection<string> ModifiedProperties { get; set; }
        
    //  [DataMember]//JsonProperty, 
    //	private Guid EntityIdentifier { get; set; }
    
    //	[DataMember]//JsonProperty, 
    //	private Guid _entityIdentity = default(Guid);
    
    	bool IEquatable<EntryDataDetailsEx>.Equals(EntryDataDetailsEx other)
    	{
    		if (EntityIdentifier != default(Guid))
    			return EntityIdentifier == other.EntityIdentifier;
    		return false;
    	}
    }
}



