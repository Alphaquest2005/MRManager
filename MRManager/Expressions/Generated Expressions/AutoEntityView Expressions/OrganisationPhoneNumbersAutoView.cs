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
	public static partial class OrganisationPhoneNumbersExpressions
	{

		public static Expression<Func<OrganisationPhoneNumbers, OrganisationPhoneNumbersAutoView>> OrganisationPhoneNumbersAutoViewExpression { get; } =
		
			x => new OrganisationPhoneNumbersAutoView 
			{
				Id = x.Id,
 				PhoneTypes = x.PhoneTypes.Name,   
 				Organisations = x.Organisations.Name,   
			};
	}
}
