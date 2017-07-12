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

namespace SalesDataQS.Client.DTO
{

   // [JsonObject(IsReference = true)]
    [DataContract(IsReference = true, Namespace="http://www.insight-software.com/WaterNut")]
    public partial class AsycudaDocumentSetEntryDataDetails : BaseEntity<AsycudaDocumentSetEntryDataDetails> , ITrackable, IEquatable<AsycudaDocumentSetEntryDataDetails>
    {
        [DataMember]
        public int AsycudaDocumentSetId
		{ 
		    get { return _AsycudaDocumentSetId; }
			set
			{
			    if (value == _AsycudaDocumentSetId) return;
				_AsycudaDocumentSetId = value;
				NotifyPropertyChanged();//m => this.AsycudaDocumentSetId
			}
		}
        private int _AsycudaDocumentSetId;

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
        public Nullable<long> Id
		{ 
		    get { return _Id; }
			set
			{
			    if (value == _Id) return;
				_Id = value;
				NotifyPropertyChanged();//m => this.Id
			}
		}
        private Nullable<long> _Id;

       
        [DataMember]
        public SalesDataDetail SalesDataDetail
		{
		    get { return _SalesDataDetail; }
			set
			{
			    if (value == _SalesDataDetail) return;
				_SalesDataDetail = value;
                SalesDataDetailChangeTracker = _SalesDataDetail == null ? null
                    : new ChangeTrackingCollection<SalesDataDetail> { _SalesDataDetail };
				NotifyPropertyChanged();//m => this.SalesDataDetail
			}
		}
        private SalesDataDetail _SalesDataDetail;
        private ChangeTrackingCollection<SalesDataDetail> SalesDataDetailChangeTracker { get; set; }

   //     [DataMember]
   //     public TrackingState TrackingState { get; set; }

   //     [DataMember]
   //     public ICollection<string> ModifiedProperties { get; set; }
        
    //  [DataMember]//JsonProperty, 
    //	private Guid EntityIdentifier { get; set; }
    
    //	[DataMember]//JsonProperty, 
    //	private Guid _entityIdentity = default(Guid);
    
    	bool IEquatable<AsycudaDocumentSetEntryDataDetails>.Equals(AsycudaDocumentSetEntryDataDetails other)
    	{
    		if (EntityIdentifier != default(Guid))
    			return EntityIdentifier == other.EntityIdentifier;
    		return false;
    	}
    }
}



