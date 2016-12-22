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
	public static partial class PersonAddressesExpressions
	{

		public static Expression<Func<PersonAddresses, PersonAddressesAutoView>> PersonAddressesAutoViewExpression { get; } =
		
			x => new PersonAddressesAutoView 
			{
				Id = x.Id,
 				AddressLines = x.Addresses.AddressLines.Select(z => z.Name).FirstOrDefault(),   
 				AddressTypes = x.AddressTypes.Name,   
 				PersonNames = x.Persons.PersonNames.Select(z => z.PersonName).FirstOrDefault(),   
			};
	}
}
