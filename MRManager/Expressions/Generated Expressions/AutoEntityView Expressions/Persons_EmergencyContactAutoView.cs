﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-Master.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System;
using System.Linq.Expressions;
using EF.Entities;

namespace Entity.Expressions
{
	public static partial class Persons_EmergencyContactExpressions
	{

		public static Expression<Func<Persons_EmergencyContact, Persons_EmergencyContactAutoView>> Persons_EmergencyContactAutoViewExpression { get; } =
		
			x => new Persons_EmergencyContactAutoView 
			{
				Id = x.Id,
 				Sex = x.Persons_Patient.Sex.Name,   
			};
	}
}
