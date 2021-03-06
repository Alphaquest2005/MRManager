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

namespace CoreEntities.Client.DTO
{

   // [JsonObject(IsReference = true)]
    [DataContract(IsReference = true, Namespace="http://www.insight-software.com/WaterNut")]
    public partial class AsycudaDocument : BaseEntity<AsycudaDocument> , ITrackable, IEquatable<AsycudaDocument>
    {
        [DataMember]
        public int ASYCUDA_Id
		{ 
		    get { return _ASYCUDA_Id; }
			set
			{
			    if (value == _ASYCUDA_Id) return;
				_ASYCUDA_Id = value;
				NotifyPropertyChanged();//m => this.ASYCUDA_Id
			}
		}
        private int _ASYCUDA_Id;

        [DataMember]
        public string id
		{ 
		    get { return _id; }
			set
			{
			    if (value == _id) return;
				_id = value;
				NotifyPropertyChanged();//m => this.id
			}
		}
        private string _id;

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
        public Nullable<System.DateTime> RegistrationDate
		{ 
		    get { return _RegistrationDate; }
			set
			{
			    if (value == _RegistrationDate) return;
				_RegistrationDate = value;
				NotifyPropertyChanged();//m => this.RegistrationDate
			}
		}
        private Nullable<System.DateTime> _RegistrationDate;

        [DataMember]
        public Nullable<bool> IsManuallyAssessed
		{ 
		    get { return _IsManuallyAssessed; }
			set
			{
			    if (value == _IsManuallyAssessed) return;
				_IsManuallyAssessed = value;
				NotifyPropertyChanged();//m => this.IsManuallyAssessed
			}
		}
        private Nullable<bool> _IsManuallyAssessed;

        [DataMember]
        public string ReferenceNumber
		{ 
		    get { return _ReferenceNumber; }
			set
			{
			    if (value == _ReferenceNumber) return;
				_ReferenceNumber = value;
				NotifyPropertyChanged();//m => this.ReferenceNumber
			}
		}
        private string _ReferenceNumber;

        [DataMember]
        public Nullable<System.DateTime> EffectiveRegistrationDate
		{ 
		    get { return _EffectiveRegistrationDate; }
			set
			{
			    if (value == _EffectiveRegistrationDate) return;
				_EffectiveRegistrationDate = value;
				NotifyPropertyChanged();//m => this.EffectiveRegistrationDate
			}
		}
        private Nullable<System.DateTime> _EffectiveRegistrationDate;

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
        public Nullable<bool> AutoUpdate
		{ 
		    get { return _AutoUpdate; }
			set
			{
			    if (value == _AutoUpdate) return;
				_AutoUpdate = value;
				NotifyPropertyChanged();//m => this.AutoUpdate
			}
		}
        private Nullable<bool> _AutoUpdate;

        [DataMember]
        public string BLNumber
		{ 
		    get { return _BLNumber; }
			set
			{
			    if (value == _BLNumber) return;
				_BLNumber = value;
				NotifyPropertyChanged();//m => this.BLNumber
			}
		}
        private string _BLNumber;

        [DataMember]
        public string Description
		{ 
		    get { return _Description; }
			set
			{
			    if (value == _Description) return;
				_Description = value;
				NotifyPropertyChanged();//m => this.Description
			}
		}
        private string _Description;

        [DataMember]
        public string Type_of_declaration
		{ 
		    get { return _Type_of_declaration; }
			set
			{
			    if (value == _Type_of_declaration) return;
				_Type_of_declaration = value;
				NotifyPropertyChanged();//m => this.Type_of_declaration
			}
		}
        private string _Type_of_declaration;

        [DataMember]
        public string Declaration_gen_procedure_code
		{ 
		    get { return _Declaration_gen_procedure_code; }
			set
			{
			    if (value == _Declaration_gen_procedure_code) return;
				_Declaration_gen_procedure_code = value;
				NotifyPropertyChanged();//m => this.Declaration_gen_procedure_code
			}
		}
        private string _Declaration_gen_procedure_code;

        [DataMember]
        public string Extended_customs_procedure
		{ 
		    get { return _Extended_customs_procedure; }
			set
			{
			    if (value == _Extended_customs_procedure) return;
				_Extended_customs_procedure = value;
				NotifyPropertyChanged();//m => this.Extended_customs_procedure
			}
		}
        private string _Extended_customs_procedure;

        [DataMember]
        public Nullable<int> Customs_ProcedureId
		{ 
		    get { return _Customs_ProcedureId; }
			set
			{
			    if (value == _Customs_ProcedureId) return;
				_Customs_ProcedureId = value;
				NotifyPropertyChanged();//m => this.Customs_ProcedureId
			}
		}
        private Nullable<int> _Customs_ProcedureId;

        [DataMember]
        public string Country_first_destination
		{ 
		    get { return _Country_first_destination; }
			set
			{
			    if (value == _Country_first_destination) return;
				_Country_first_destination = value;
				NotifyPropertyChanged();//m => this.Country_first_destination
			}
		}
        private string _Country_first_destination;

        [DataMember]
        public string Currency_code
		{ 
		    get { return _Currency_code; }
			set
			{
			    if (value == _Currency_code) return;
				_Currency_code = value;
				NotifyPropertyChanged();//m => this.Currency_code
			}
		}
        private string _Currency_code;

        [DataMember]
        public Nullable<double> Currency_rate
		{ 
		    get { return _Currency_rate; }
			set
			{
			    if (value == _Currency_rate) return;
				_Currency_rate = value;
				NotifyPropertyChanged();//m => this.Currency_rate
			}
		}
        private Nullable<double> _Currency_rate;

        [DataMember]
        public string Manifest_reference_number
		{ 
		    get { return _Manifest_reference_number; }
			set
			{
			    if (value == _Manifest_reference_number) return;
				_Manifest_reference_number = value;
				NotifyPropertyChanged();//m => this.Manifest_reference_number
			}
		}
        private string _Manifest_reference_number;

        [DataMember]
        public string Customs_clearance_office_code
		{ 
		    get { return _Customs_clearance_office_code; }
			set
			{
			    if (value == _Customs_clearance_office_code) return;
				_Customs_clearance_office_code = value;
				NotifyPropertyChanged();//m => this.Customs_clearance_office_code
			}
		}
        private string _Customs_clearance_office_code;

        [DataMember]
        public Nullable<int> Lines
		{ 
		    get { return _Lines; }
			set
			{
			    if (value == _Lines) return;
				_Lines = value;
				NotifyPropertyChanged();//m => this.Lines
			}
		}
        private Nullable<int> _Lines;

        [DataMember]
        public string DocumentType
		{ 
		    get { return _DocumentType; }
			set
			{
			    if (value == _DocumentType) return;
				_DocumentType = value;
				NotifyPropertyChanged();//m => this.DocumentType
			}
		}
        private string _DocumentType;

        [DataMember]
        public Nullable<int> Document_TypeId
		{ 
		    get { return _Document_TypeId; }
			set
			{
			    if (value == _Document_TypeId) return;
				_Document_TypeId = value;
				NotifyPropertyChanged();//m => this.Document_TypeId
			}
		}
        private Nullable<int> _Document_TypeId;

        [DataMember]
        public Nullable<bool> ImportComplete
		{ 
		    get { return _ImportComplete; }
			set
			{
			    if (value == _ImportComplete) return;
				_ImportComplete = value;
				NotifyPropertyChanged();//m => this.ImportComplete
			}
		}
        private Nullable<bool> _ImportComplete;

        [DataMember]
        public Nullable<bool> Cancelled
		{ 
		    get { return _Cancelled; }
			set
			{
			    if (value == _Cancelled) return;
				_Cancelled = value;
				NotifyPropertyChanged();//m => this.Cancelled
			}
		}
        private Nullable<bool> _Cancelled;

        [DataMember]
        public Nullable<double> TotalCIF
		{ 
		    get { return _TotalCIF; }
			set
			{
			    if (value == _TotalCIF) return;
				_TotalCIF = value;
				NotifyPropertyChanged();//m => this.TotalCIF
			}
		}
        private Nullable<double> _TotalCIF;

        [DataMember]
        public Nullable<double> TotalGrossWeight
		{ 
		    get { return _TotalGrossWeight; }
			set
			{
			    if (value == _TotalGrossWeight) return;
				_TotalGrossWeight = value;
				NotifyPropertyChanged();//m => this.TotalGrossWeight
			}
		}
        private Nullable<double> _TotalGrossWeight;

       
        [DataMember]
        public ChangeTrackingCollection<AsycudaDocumentItem> AsycudaDocumentItems
		{
		    get { return _AsycudaDocumentItems; }
			set
			{
			    if (Equals(value, _AsycudaDocumentItems)) return;
				_AsycudaDocumentItems = value;
				NotifyPropertyChanged();//m => this.AsycudaDocumentItems
			}
		}
        private ChangeTrackingCollection<AsycudaDocumentItem> _AsycudaDocumentItems = new ChangeTrackingCollection<AsycudaDocumentItem>();

        [DataMember]
        public AsycudaDocumentSetEx AsycudaDocumentSetEx
		{
		    get { return _AsycudaDocumentSetEx; }
			set
			{
			    if (value == _AsycudaDocumentSetEx) return;
				_AsycudaDocumentSetEx = value;
                AsycudaDocumentSetExChangeTracker = _AsycudaDocumentSetEx == null ? null
                    : new ChangeTrackingCollection<AsycudaDocumentSetEx> { _AsycudaDocumentSetEx };
				NotifyPropertyChanged();//m => this.AsycudaDocumentSetEx
			}
		}
        private AsycudaDocumentSetEx _AsycudaDocumentSetEx;
        private ChangeTrackingCollection<AsycudaDocumentSetEx> AsycudaDocumentSetExChangeTracker { get; set; }

   //     [DataMember]
   //     public TrackingState TrackingState { get; set; }

   //     [DataMember]
   //     public ICollection<string> ModifiedProperties { get; set; }
        
    //  [DataMember]//JsonProperty, 
    //	private Guid EntityIdentifier { get; set; }
    
    //	[DataMember]//JsonProperty, 
    //	private Guid _entityIdentity = default(Guid);
    
    	bool IEquatable<AsycudaDocument>.Equals(AsycudaDocument other)
    	{
    		if (EntityIdentifier != default(Guid))
    			return EntityIdentifier == other.EntityIdentifier;
    		return false;
    	}
    }
}



