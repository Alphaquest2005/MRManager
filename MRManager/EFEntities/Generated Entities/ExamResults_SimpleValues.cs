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
	public partial class ExamResults_SimpleValues: BaseEntity, IExamResults_SimpleValues
	{
		public virtual int ExamResultsId { get; set; }
		public virtual int PatientResultsId { get; set; }
		public virtual int ResultFieldId { get; set; }
		public virtual string Value { get; set; }

		//-------------------Navigation Properties -------------------------------//
			// ---------Child Relationships
		
			// ---------Parent Relationships
				public virtual ExamResults ExamResults {get; set;}
				public virtual ResultFieldNames ResultFieldNames {get; set;}
	

	}
}
