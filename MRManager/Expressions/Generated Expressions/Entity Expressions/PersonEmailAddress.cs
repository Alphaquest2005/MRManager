﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-Expressions.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System;
using System.Collections.Generic;
using System.Linq;
using Common.DataEntites;
using EF.Entities;
using Interfaces;

namespace Entity.Expressions
{
	public static partial class PersonEmailAddressExpressions
	{
		public static IQueryable<PersonEmailAddress> GetPersonEmailAddressById(this IQueryable<PersonEmailAddress> query, int Id) => query.Where(x => x.Id == Id);


// Get Leaf Properties
    
		public static IQueryable<PersonEmailAddress> GetPersonEmailAddress(this IQueryable<PersonEmailAddress> query) => query;

			// Child Properties
			//Parent Properties
				//public static IQueryable<PersonEmailAddress> PersonEmailAddress(this IQueryable<Persons> persons) => persons.SelectMany(x => x.PersonEmailAddress);
				public static IQueryable<Persons> Persons(this IQueryable<PersonEmailAddress> query) => query.Select(x => x.Persons);
	}
}
