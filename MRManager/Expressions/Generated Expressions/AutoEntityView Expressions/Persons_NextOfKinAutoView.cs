﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-Master.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System;
using System.Linq.Expressions;
using EF.Entities;

namespace Entity.Expressions
{
	public static partial class Persons_NextOfKinExpressions
	{

		public static Expression<Func<Persons_NextOfKin, Persons_NextOfKinAutoView>> Persons_NextOfKinAutoViewExpression { get; } =
		
			x => new Persons_NextOfKinAutoView 
			{
				Id = x.Id,
 				Sex = x.Persons_Patient.Sex.Name,   
			};
	}
}
