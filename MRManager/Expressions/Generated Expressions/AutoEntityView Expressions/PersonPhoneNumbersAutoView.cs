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
	public static partial class PersonPhoneNumbersExpressions
	{

		public static Expression<Func<PersonPhoneNumbers, PersonPhoneNumbersAutoView>> PersonPhoneNumbersAutoViewExpression { get; } =
		
			x => new PersonPhoneNumbersAutoView 
			{
				Id = x.Id,
 				PersonNames = x.Persons.PersonNames.Select(z => z.PersonName).FirstOrDefault(),   
 				UserSignIn = x.Persons.UserSignIn.Username,   
 				PhoneTypes = x.PhoneTypes.Name,   
			};
	}
}
