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
    public partial class Document_Type : BaseEntity<Document_Type> , ITrackable, IEquatable<Document_Type>
    {
        [DataMember]
        public int Document_TypeId
		{ 
		    get { return _Document_TypeId; }
			set
			{
			    if (value == _Document_TypeId) return;
				_Document_TypeId = value;
				NotifyPropertyChanged();//m => this.Document_TypeId
			}
		}
        private int _Document_TypeId;

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
        public ChangeTrackingCollection<Customs_Procedure> Customs_Procedure
		{
		    get { return _Customs_Procedure; }
			set
			{
			    if (Equals(value, _Customs_Procedure)) return;
				_Customs_Procedure = value;
				NotifyPropertyChanged();//m => this.Customs_Procedure
			}
		}
        private ChangeTrackingCollection<Customs_Procedure> _Customs_Procedure = new ChangeTrackingCollection<Customs_Procedure>();

   //     [DataMember]
   //     public TrackingState TrackingState { get; set; }

   //     [DataMember]
   //     public ICollection<string> ModifiedProperties { get; set; }
        
    //  [DataMember]//JsonProperty, 
    //	private Guid EntityIdentifier { get; set; }
    
    //	[DataMember]//JsonProperty, 
    //	private Guid _entityIdentity = default(Guid);
    
    	bool IEquatable<Document_Type>.Equals(Document_Type other)
    	{
    		if (EntityIdentifier != default(Guid))
    			return EntityIdentifier == other.EntityIdentifier;
    		return false;
    	}
    }
}



