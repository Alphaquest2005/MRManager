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
	public partial class PersonCountryOfResidence: BaseEntity, IPersonCountryOfResidence
	{
		public virtual int CountryId { get; set; }
		public virtual DateTime Date { get; set; }
		public virtual int PersonId { get; set; }

		//-------------------Navigation Properties -------------------------------//
			// ---------Child Relationships
		
			// ---------Parent Relationships
				public virtual Countries Countries {get; set;}
				public virtual Persons_Patient Persons_Patient {get; set;}
	

	}
}
