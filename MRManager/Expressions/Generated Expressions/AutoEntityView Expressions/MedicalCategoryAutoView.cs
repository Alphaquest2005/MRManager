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
	public static partial class MedicalCategoryExpressions
	{

		public static Expression<Func<MedicalCategory, MedicalCategoryAutoView>> MedicalCategoryAutoViewExpression { get; } =
		
			x => new MedicalCategoryAutoView 
			{
				Id = x.Id,
 				Phase = x.Interviews.Select(x0 => x0.Phase).Select(z => z.Name).FirstOrDefault(),   
 				Interviews = x.Interviews.Select(z => z.Name).FirstOrDefault(),   
			};
	}
}
