﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-DataEntities.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using Common.DataEntites;
using Interfaces;

namespace EF.Entities
{
	public partial class Weight: BaseEntity, IWeight
	{
		public virtual double Value { get; set; }
		public virtual int UnitId { get; set; }

		//-------------------Navigation Properties -------------------------------//
			// ---------Child Relationships
		
			// ---------Parent Relationships
				public virtual Units Units {get; set;}
				public virtual VitalSigns VitalSigns {get; set;}
	

	}
}
