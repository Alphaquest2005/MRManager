﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-DataEntities.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using Common.DataEntites;
using Interfaces;

namespace EF.Entities
{
	public partial class ExamDetailComponents: BaseEntity, IExamDetailComponents
	{
		public virtual int ComponentId { get; set; }
		public virtual int ExamDetailsId { get; set; }

		//-------------------Navigation Properties -------------------------------//
			// ---------Child Relationships
		
			// ---------Parent Relationships
				public virtual Components Components {get; set;}
				public virtual ExamDetails ExamDetails {get; set;}
	

	}
}
