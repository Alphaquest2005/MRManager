﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-DataEntities.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System;
using System.Collections.Generic;
using Common.DataEntites;
using EF.Entities;
using Interfaces;

namespace EF.Entities
{
	public partial class CarePlan: BaseEntity, ICarePlan
	{
		public virtual string Name { get; set; }
		public virtual string Diagnosis { get; set; }

		//-------------------Navigation Properties -------------------------------//
			// ---------Child Relationships
				public virtual ICollection<CarePlanDetails> CarePlanDetails {get; set;}
				public virtual ICollection<ResponseSuggestions_CarePlans> ResponseSuggestions_CarePlans {get; set;}
		
			// ---------Parent Relationships
	

	}
}
