﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-Master.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System.Linq;
using EF.Entities;

namespace Entity.Expressions
{
	public static partial class PersonNamesExpressions
	{
		public static IQueryable<PersonNames> GetPersonNamesById(this IQueryable<PersonNames> query, int Id) => query.Where(x => x.Id == Id);


// Get Leaf Properties
    
		public static IQueryable<PersonNames> GetPersonNames(this IQueryable<PersonNames> query) => query;

			// Child Properties
			//Parent Properties
				//public static IQueryable<PersonNames> PersonNames(this IQueryable<Persons> persons) => persons.SelectMany(x => x.PersonNames);
				public static IQueryable<Persons> Persons(this IQueryable<PersonNames> query) => query.Select(x => x.Persons);
	}
}
