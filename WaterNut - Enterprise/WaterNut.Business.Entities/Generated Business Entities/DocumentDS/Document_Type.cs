﻿// <autogenerated>
//   This file was generated by T4 code generator AllBusinessEntities.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
//using Newtonsoft.Json;
using TrackableEntities;
using Core.Common.Business.Entities;


namespace DocumentDS.Business.Entities
{
    //[JsonObject(IsReference = true)]
    [DataContract(IsReference = true, Namespace="http://www.insight-software.com/WaterNut")]
    public partial class Document_Type : BaseEntity<Document_Type> , ITrackable
    {
        partial void AutoGenStartUp() //Document_Type()
        {
            this.AsycudaDocumentSets = new List<AsycudaDocumentSet>();
            this.Customs_Procedure = new List<Customs_Procedure>();
            this.xcuda_ASYCUDA_ExtendedProperties = new List<xcuda_ASYCUDA_ExtendedProperties>();
        }

        [DataMember]
        public int Document_TypeId 
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
        int _document_typeid;
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
        public List<AsycudaDocumentSet> AsycudaDocumentSets { get; set; }
        [DataMember]
        public List<Customs_Procedure> Customs_Procedure { get; set; }
        [DataMember]
        public List<xcuda_ASYCUDA_ExtendedProperties> xcuda_ASYCUDA_ExtendedProperties { get; set; }

 //       [DataMember]
 //       public TrackingState TrackingState { get; set; }
 //       [DataMember]
 //       public ICollection<string> ModifiedProperties { get; set; }
//        [DataMember]//JsonProperty,
 //       private Guid EntityIdentifier { get; set; }
    }
}


