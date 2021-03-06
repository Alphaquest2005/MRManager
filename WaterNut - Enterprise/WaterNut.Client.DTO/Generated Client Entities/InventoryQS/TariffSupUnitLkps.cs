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
    public partial class TariffSupUnitLkps : BaseEntity<TariffSupUnitLkps> , ITrackable, IEquatable<TariffSupUnitLkps>
    {
        [DataMember]
        public string TariffCategoryCode
		{ 
		    get { return _TariffCategoryCode; }
			set
			{
			    if (value == _TariffCategoryCode) return;
				_TariffCategoryCode = value;
				NotifyPropertyChanged();//m => this.TariffCategoryCode
			}
		}
        private string _TariffCategoryCode;

        [DataMember]
        public string SuppUnitCode2
		{ 
		    get { return _SuppUnitCode2; }
			set
			{
			    if (value == _SuppUnitCode2) return;
				_SuppUnitCode2 = value;
				NotifyPropertyChanged();//m => this.SuppUnitCode2
			}
		}
        private string _SuppUnitCode2;

        [DataMember]
        public string SuppUnitName2
		{ 
		    get { return _SuppUnitName2; }
			set
			{
			    if (value == _SuppUnitName2) return;
				_SuppUnitName2 = value;
				NotifyPropertyChanged();//m => this.SuppUnitName2
			}
		}
        private string _SuppUnitName2;

        [DataMember]
        public double SuppQty
		{ 
		    get { return _SuppQty; }
			set
			{
			    if (value == _SuppQty) return;
				_SuppQty = value;
				NotifyPropertyChanged();//m => this.SuppQty
			}
		}
        private double _SuppQty;

        [DataMember]
        public int Id
		{ 
		    get { return _Id; }
			set
			{
			    if (value == _Id) return;
				_Id = value;
				NotifyPropertyChanged();//m => this.Id
			}
		}
        private int _Id;

       
        [DataMember]
        public TariffCategory TariffCategory
		{
		    get { return _TariffCategory; }
			set
			{
			    if (value == _TariffCategory) return;
				_TariffCategory = value;
                TariffCategoryChangeTracker = _TariffCategory == null ? null
                    : new ChangeTrackingCollection<TariffCategory> { _TariffCategory };
				NotifyPropertyChanged();//m => this.TariffCategory
			}
		}
        private TariffCategory _TariffCategory;
        private ChangeTrackingCollection<TariffCategory> TariffCategoryChangeTracker { get; set; }

   //     [DataMember]
   //     public TrackingState TrackingState { get; set; }

   //     [DataMember]
   //     public ICollection<string> ModifiedProperties { get; set; }
        
    //  [DataMember]//JsonProperty, 
    //	private Guid EntityIdentifier { get; set; }
    
    //	[DataMember]//JsonProperty, 
    //	private Guid _entityIdentity = default(Guid);
    
    	bool IEquatable<TariffSupUnitLkps>.Equals(TariffSupUnitLkps other)
    	{
    		if (EntityIdentifier != default(Guid))
    			return EntityIdentifier == other.EntityIdentifier;
    		return false;
    	}
    }
}



