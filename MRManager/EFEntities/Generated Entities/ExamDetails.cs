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
	public partial class ExamDetails: BaseEntity, IExamDetails
	{
		public virtual int ExamId { get; set; }
		public virtual string Section { get; set; }

		//-------------------Navigation Properties -------------------------------//
			// ---------Child Relationships
				public virtual ICollection<ExamDetailComponents> ExamDetailComponents {get; set;}
				public virtual ICollection<ExamResults> ExamResults {get; set;}
		
			// ---------Parent Relationships
				public virtual Exams Exams {get; set;}
	

	}
}
