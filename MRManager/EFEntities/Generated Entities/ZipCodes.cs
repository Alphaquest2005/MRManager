﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-DataEntities.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System.Collections.Generic;
using Common.DataEntites;
using Interfaces;

namespace EF.Entities
{
	public partial class ZipCodes: BaseEntity, IZipCodes
	{
		public virtual string Name { get; set; }

		//-------------------Navigation Properties -------------------------------//
			// ---------Child Relationships
				public virtual ICollection<AddressZipCodes> AddressZipCodes {get; set;}
		
			// ---------Parent Relationships
	

	}
}
