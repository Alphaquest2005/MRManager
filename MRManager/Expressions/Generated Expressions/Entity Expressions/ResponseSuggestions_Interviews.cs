﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-Master.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System.Linq;
using EF.Entities;

namespace Entity.Expressions
{
	public static partial class ResponseSuggestions_InterviewsExpressions
	{
		public static IQueryable<ResponseSuggestions_Interviews> GetResponseSuggestions_InterviewsById(this IQueryable<ResponseSuggestions_Interviews> query, int Id) => query.Where(x => x.Id == Id);


// Get Leaf Properties
    
		public static IQueryable<ResponseSuggestions_Interviews> GetResponseSuggestions_Interviews(this IQueryable<ResponseSuggestions_Interviews> query) => query;

			// Child Properties
			//Parent Properties
				//public static IQueryable<ResponseSuggestions_Interviews> ResponseSuggestions_Interviews(this IQueryable<ResponseSuggestions> responsesuggestions) => responsesuggestions.Select(x => x.ResponseSuggestions_Interviews);
				public static IQueryable<ResponseSuggestions> ResponseSuggestions(this IQueryable<ResponseSuggestions_Interviews> query) => query.Select(x => x.ResponseSuggestions);
	}
}
