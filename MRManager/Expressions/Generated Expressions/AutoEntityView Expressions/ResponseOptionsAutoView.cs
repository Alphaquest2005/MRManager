﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-Master.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System;
using System.Linq;
using System.Linq.Expressions;
using EF.Entities;

namespace Entity.Expressions
{
	public static partial class ResponseOptionsExpressions
	{

		public static Expression<Func<ResponseOptions, ResponseOptionsAutoView>> ResponseOptionsAutoViewExpression { get; } =
		
			x => new ResponseOptionsAutoView 
			{
				Id = x.Id,
 				Interviews = x.Questions.Interviews.Name,   
 				Response = x.Response.Select(z => z.Value).FirstOrDefault(),   
			};
	}
}
