﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-DataEntities.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System.Collections.Generic;
using Common.DataEntites;
using Interfaces;

namespace EF.Entities
{
	public partial class PhoneTypes: BaseEntity, IPhoneTypes
	{
		public virtual string Name { get; set; }

		//-------------------Navigation Properties -------------------------------//
			// ---------Child Relationships
				public virtual ICollection<ForeignPhoneNumbers> ForeignPhoneNumbers {get; set;}
				public virtual ICollection<OrganisationPhoneNumbers> OrganisationPhoneNumbers {get; set;}
				public virtual ICollection<PersonPhoneNumbers> PersonPhoneNumbers {get; set;}
		
			// ---------Parent Relationships
	

	}
}
