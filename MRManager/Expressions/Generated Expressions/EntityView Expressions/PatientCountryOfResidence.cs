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
	public static partial class Persons_PatientExpressions
	{

		public static Expression<Func<Persons_Patient, PatientCountryOfResidence>> Persons_PatientToPatientCountryOfResidenceExpression { get; } =
		
			x => new PatientCountryOfResidence()
			{
				Id = x.Id,
				Country = x.PersonCountryOfResidence.Select(x2 => x2.Countries).Select(z => z.Name).StringJoin(","),
			};
	}
}
