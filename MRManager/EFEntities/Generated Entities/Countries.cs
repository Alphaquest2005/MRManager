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
	public partial class Countries: BaseEntity, ICountries
	{
		public virtual string Name { get; set; }

		//-------------------Navigation Properties -------------------------------//
			// ---------Child Relationships
				public virtual ICollection<AddressCountries> AddressCountries {get; set;}
				public virtual ICollection<PersonCountryOfResidence> PersonCountryOfResidence {get; set;}
		
			// ---------Parent Relationships
	

	}
}
