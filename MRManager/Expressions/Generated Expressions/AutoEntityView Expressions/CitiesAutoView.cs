﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-Expressions.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Common.DataEntites;
using EF.Entities;
using Interfaces;

namespace Entity.Expressions
{
	public static partial class CitiesExpressions
	{

		public static Expression<Func<Cities, CitiesAutoView>> CitiesAutoViewExpression { get; } =
		
			x => new CitiesAutoView 
			{
				Id = x.Id,
 				Cities = x.Name,   
			};
	}
}
