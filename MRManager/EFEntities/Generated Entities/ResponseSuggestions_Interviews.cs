﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-DataEntities.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using Common.DataEntites;
using Interfaces;

namespace EF.Entities
{
	public partial class ResponseSuggestions_Interviews: BaseEntity, IResponseSuggestions_Interviews
	{
		public virtual int InterviewId { get; set; }

		//-------------------Navigation Properties -------------------------------//
			// ---------Child Relationships
		
			// ---------Parent Relationships
				public virtual ResponseSuggestions ResponseSuggestions {get; set;}
	

	}
}
