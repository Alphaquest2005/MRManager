﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-Master.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System.Linq;
using EF.Entities;

namespace Entity.Expressions
{
	public static partial class ComponentsExpressions
	{
		public static IQueryable<Components> GetComponentsById(this IQueryable<Components> query, int Id) => query.Where(x => x.Id == Id);


// Get Leaf Properties
    
		public static IQueryable<Components> GetComponents(this IQueryable<Components> query) => query;

			// Child Properties
				public static IQueryable<ExamDetailComponents> ExamDetailComponents(this IQueryable<Components> components) => components.SelectMany(x => x.ExamDetailComponents);
				public static IQueryable<ExamDetailComponents> ExamDetailComponents(this IQueryable<Components> components, int id) => components.Where(x => x.Id == id).SelectMany(x => x.ExamDetailComponents);
			//Parent Properties
	}
}
