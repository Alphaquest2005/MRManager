﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-Master.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System;
using System.Linq.Expressions;
using EF.Entities;

namespace Entity.Expressions
{
	public static partial class StatesExpressions
	{

		public static Expression<Func<States, StatesAutoView>> StatesAutoViewExpression { get; } =
		
			x => new StatesAutoView 
			{
				Id = x.Id,
 				States = x.Name,   
			};
	}
}
