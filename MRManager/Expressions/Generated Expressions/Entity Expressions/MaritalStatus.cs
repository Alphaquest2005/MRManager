﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-Master.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System.Linq;
using EF.Entities;

namespace Entity.Expressions
{
	public static partial class MaritalStatusExpressions
	{
		public static IQueryable<MaritalStatus> GetMaritalStatusById(this IQueryable<MaritalStatus> query, int Id) => query.Where(x => x.Id == Id);


// Get Leaf Properties
    
		public static IQueryable<MaritalStatus> GetMaritalStatus(this IQueryable<MaritalStatus> query) => query;

			// Child Properties
			//Parent Properties
	}
}
