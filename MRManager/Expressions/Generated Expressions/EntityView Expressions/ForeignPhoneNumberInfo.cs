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
	public static partial class Persons_NonResidentPatientExpressions
	{

		public static Expression<Func<Persons_NonResidentPatient, ForeignPhoneNumberInfo>> Persons_NonResidentPatientToForeignPhoneNumberInfoExpression { get; } =
		
			x => new ForeignPhoneNumberInfo()
			{
				Id = x.Id,
				PhoneNumber = x.ForeignPhoneNumbers.Select(z => z.PhoneNumber).StringJoin(","),
				Phonetype = x.ForeignPhoneNumbers.Select(x2 => x2.PhoneTypes).Select(z => z.Name).StringJoin(","),
			};
	}
}
