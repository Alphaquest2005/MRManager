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
	public partial class ForeignAddresses: BaseEntity, IForeignAddresses
	{
		public virtual int AddressId { get; set; }
		public virtual int PersonId { get; set; }
		public virtual int AddressTypeId { get; set; }

		//-------------------Navigation Properties -------------------------------//
			// ---------Child Relationships
		
			// ---------Parent Relationships
				public virtual Addresses Addresses {get; set;}
				public virtual Persons_NonResidentPatient Persons_NonResidentPatient {get; set;}
				public virtual AddressTypes AddressTypes {get; set;}
	

	}
}
