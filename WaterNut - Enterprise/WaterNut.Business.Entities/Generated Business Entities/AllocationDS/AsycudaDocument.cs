﻿// <autogenerated>
//   This file was generated by T4 code generator Business.Entities.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
//using Newtonsoft.Json;
using TrackableEntities;
using Core.Common.Business.Entities;


namespace AllocationDS.Business.Entities
{
    //[JsonObject(IsReference = true)]
    [DataContract(IsReference = true, Namespace="http://www.insight-software.com/WaterNut")]
    public partial class AsycudaDocument : BaseEntity<AsycudaDocument> , ITrackable
    {
        partial void AutoGenStartUp() //AsycudaDocument()
        {
            this.xcuda_Item = new List<xcuda_Item>();
        }

        [DataMember]
        public int ASYCUDA_Id 
        {
            get
            {
                return _asycuda_id;
            }
            set
            {
                _asycuda_id = value;
                //if(this.TrackingState == TrackingState.Unchanged) this.TrackingState = TrackingState.Modified;  
                NotifyPropertyChanged();
            }
        }
        int _asycuda_id;
        [DataMember]
        public string id 
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                //if(this.TrackingState == TrackingState.Unchanged) this.TrackingState = TrackingState.Modified;  
                NotifyPropertyChanged();
            }
        }
        string _id;
        [DataMember]
        public string CNumber 
        {
            get
            {
                return _cnumber;
            }
            set
            {
                _cnumber = value;
                //if(this.TrackingState == TrackingState.Unchanged) this.TrackingState = TrackingState.Modified;  
                NotifyPropertyChanged();
            }
        }
        string _cnumber;
        [DataMember]
        public Nullable<System.DateTime> RegistrationDate 
        {
            get
            {
                return _registrationdate;
            }
            set
            {
                _registrationdate = value;
                //if(this.TrackingState == TrackingState.Unchanged) this.TrackingState = TrackingState.Modified;  
                NotifyPropertyChanged();
            }
        }
        Nullable<System.DateTime> _registrationdate;
        [DataMember]
        public Nullable<bool> IsManuallyAssessed 
        {
            get
            {
                return _ismanuallyassessed;
            }
            set
            {
                _ismanuallyassessed = value;
                //if(this.TrackingState == TrackingState.Unchanged) this.TrackingState = TrackingState.Modified;  
                NotifyPropertyChanged();
            }
        }
        Nullable<bool> _ismanuallyassessed;
        [DataMember]
        public string ReferenceNumber 
        {
            get
            {
                return _referencenumber;
            }
            set
            {
                _referencenumber = value;
                //if(this.TrackingState == TrackingState.Unchanged) this.TrackingState = TrackingState.Modified;  
                NotifyPropertyChanged();
            }
        }
        string _referencenumber;
        [DataMember]
        public Nullable<System.DateTime> EffectiveRegistrationDate 
        {
            get
            {
                return _effectiveregistrationdate;
            }
            set
            {
                _effectiveregistrationdate = value;
                //if(this.TrackingState == TrackingState.Unchanged) this.TrackingState = TrackingState.Modified;  
                NotifyPropertyChanged();
            }
        }
        Nullable<System.DateTime> _effectiveregistrationdate;
        [DataMember]
        public Nullable<int> AsycudaDocumentSetId 
        {
            get
            {
                return _asycudadocumentsetid;
            }
            set
            {
                _asycudadocumentsetid = value;
                //if(this.TrackingState == TrackingState.Unchanged) this.TrackingState = TrackingState.Modified;  
                NotifyPropertyChanged();
            }
        }
        Nullable<int> _asycudadocumentsetid;
        [DataMember]
        public Nullable<bool> DoNotAllocate 
        {
            get
            {
                return _donotallocate;
            }
            set
            {
                _donotallocate = value;
                //if(this.TrackingState == TrackingState.Unchanged) this.TrackingState = TrackingState.Modified;  
                NotifyPropertyChanged();
            }
        }
        Nullable<bool> _donotallocate;
        [DataMember]
        public Nullable<bool> AutoUpdate 
        {
            get
            {
                return _autoupdate;
            }
            set
            {
                _autoupdate = value;
                //if(this.TrackingState == TrackingState.Unchanged) this.TrackingState = TrackingState.Modified;  
                NotifyPropertyChanged();
            }
        }
        Nullable<bool> _autoupdate;
        [DataMember]
        public string BLNumber 
        {
            get
            {
                return _blnumber;
            }
            set
            {
                _blnumber = value;
                //if(this.TrackingState == TrackingState.Unchanged) this.TrackingState = TrackingState.Modified;  
                NotifyPropertyChanged();
            }
        }
        string _blnumber;
        [DataMember]
        public string Description 
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                //if(this.TrackingState == TrackingState.Unchanged) this.TrackingState = TrackingState.Modified;  
                NotifyPropertyChanged();
            }
        }
        string _description;
        [DataMember]
        public string Type_of_declaration 
        {
            get
            {
                return _type_of_declaration;
            }
            set
            {
                _type_of_declaration = value;
                //if(this.TrackingState == TrackingState.Unchanged) this.TrackingState = TrackingState.Modified;  
                NotifyPropertyChanged();
            }
        }
        string _type_of_declaration;
        [DataMember]
        public string Declaration_gen_procedure_code 
        {
            get
            {
                return _declaration_gen_procedure_code;
            }
            set
            {
                _declaration_gen_procedure_code = value;
                //if(this.TrackingState == TrackingState.Unchanged) this.TrackingState = TrackingState.Modified;  
                NotifyPropertyChanged();
            }
        }
        string _declaration_gen_procedure_code;
        [DataMember]
        public string Extended_customs_procedure 
        {
            get
            {
                return _extended_customs_procedure;
            }
            set
            {
                _extended_customs_procedure = value;
                //if(this.TrackingState == TrackingState.Unchanged) this.TrackingState = TrackingState.Modified;  
                NotifyPropertyChanged();
            }
        }
        string _extended_customs_procedure;
        [DataMember]
        public Nullable<int> Customs_ProcedureId 
        {
            get
            {
                return _customs_procedureid;
            }
            set
            {
                _customs_procedureid = value;
                //if(this.TrackingState == TrackingState.Unchanged) this.TrackingState = TrackingState.Modified;  
                NotifyPropertyChanged();
            }
        }
        Nullable<int> _customs_procedureid;
        [DataMember]
        public string Country_first_destination 
        {
            get
            {
                return _country_first_destination;
            }
            set
            {
                _country_first_destination = value;
                //if(this.TrackingState == TrackingState.Unchanged) this.TrackingState = TrackingState.Modified;  
                NotifyPropertyChanged();
            }
        }
        string _country_first_destination;
        [DataMember]
        public string Currency_code 
        {
            get
            {
                return _currency_code;
            }
            set
            {
                _currency_code = value;
                //if(this.TrackingState == TrackingState.Unchanged) this.TrackingState = TrackingState.Modified;  
                NotifyPropertyChanged();
            }
        }
        string _currency_code;
        [DataMember]
        public Nullable<double> Currency_rate 
        {
            get
            {
                return _currency_rate;
            }
            set
            {
                _currency_rate = value;
                //if(this.TrackingState == TrackingState.Unchanged) this.TrackingState = TrackingState.Modified;  
                NotifyPropertyChanged();
            }
        }
        Nullable<double> _currency_rate;
        [DataMember]
        public string Manifest_reference_number 
        {
            get
            {
                return _manifest_reference_number;
            }
            set
            {
                _manifest_reference_number = value;
                //if(this.TrackingState == TrackingState.Unchanged) this.TrackingState = TrackingState.Modified;  
                NotifyPropertyChanged();
            }
        }
        string _manifest_reference_number;
        [DataMember]
        public string Customs_clearance_office_code 
        {
            get
            {
                return _customs_clearance_office_code;
            }
            set
            {
                _customs_clearance_office_code = value;
                //if(this.TrackingState == TrackingState.Unchanged) this.TrackingState = TrackingState.Modified;  
                NotifyPropertyChanged();
            }
        }
        string _customs_clearance_office_code;
        [DataMember]
        public string DocumentType 
        {
            get
            {
                return _documenttype;
            }
            set
            {
                _documenttype = value;
                //if(this.TrackingState == TrackingState.Unchanged) this.TrackingState = TrackingState.Modified;  
                NotifyPropertyChanged();
            }
        }
        string _documenttype;
        [DataMember]
        public Nullable<int> Document_TypeId 
        {
            get
            {
                return _document_typeid;
            }
            set
            {
                _document_typeid = value;
                //if(this.TrackingState == TrackingState.Unchanged) this.TrackingState = TrackingState.Modified;  
                NotifyPropertyChanged();
            }
        }
        Nullable<int> _document_typeid;
        [DataMember]
        public Nullable<int> Lines 
        {
            get
            {
                return _lines;
            }
            set
            {
                _lines = value;
                //if(this.TrackingState == TrackingState.Unchanged) this.TrackingState = TrackingState.Modified;  
                NotifyPropertyChanged();
            }
        }
        Nullable<int> _lines;
        [DataMember]
        public Nullable<bool> ImportComplete 
        {
            get
            {
                return _importcomplete;
            }
            set
            {
                _importcomplete = value;
                //if(this.TrackingState == TrackingState.Unchanged) this.TrackingState = TrackingState.Modified;  
                NotifyPropertyChanged();
            }
        }
        Nullable<bool> _importcomplete;
        [DataMember]
        public Nullable<bool> Cancelled 
        {
            get
            {
                return _cancelled;
            }
            set
            {
                _cancelled = value;
                //if(this.TrackingState == TrackingState.Unchanged) this.TrackingState = TrackingState.Modified;  
                NotifyPropertyChanged();
            }
        }
        Nullable<bool> _cancelled;
        [DataMember]
        public Nullable<double> TotalCIF 
        {
            get
            {
                return _totalcif;
            }
            set
            {
                _totalcif = value;
                //if(this.TrackingState == TrackingState.Unchanged) this.TrackingState = TrackingState.Modified;  
                NotifyPropertyChanged();
            }
        }
        Nullable<double> _totalcif;
        [DataMember]
        public Nullable<double> TotalGrossWeight 
        {
            get
            {
                return _totalgrossweight;
            }
            set
            {
                _totalgrossweight = value;
                //if(this.TrackingState == TrackingState.Unchanged) this.TrackingState = TrackingState.Modified;  
                NotifyPropertyChanged();
            }
        }
        Nullable<double> _totalgrossweight;
        [DataMember]
        public System.DateTime AssessmentDate 
        {
            get
            {
                return _assessmentdate;
            }
            set
            {
                _assessmentdate = value;
                //if(this.TrackingState == TrackingState.Unchanged) this.TrackingState = TrackingState.Modified;  
                NotifyPropertyChanged();
            }
        }
        System.DateTime _assessmentdate;
        [DataMember]
        public List<xcuda_Item> xcuda_Item { get; set; }

 //       [DataMember]
 //       public TrackingState TrackingState { get; set; }
 //       [DataMember]
 //       public ICollection<string> ModifiedProperties { get; set; }
//        [DataMember]//JsonProperty,
 //       private Guid EntityIdentifier { get; set; }
    }
}


