﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-Master.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System.Linq;
using EF.Entities;

namespace Entity.Expressions
{
	public static partial class ResultFieldNamesExpressions
	{
		public static IQueryable<ResultFieldNames> GetResultFieldNamesById(this IQueryable<ResultFieldNames> query, int Id) => query.Where(x => x.Id == Id);


// Get Leaf Properties
    
		public static IQueryable<ResultFieldNames> GetResultFieldNames(this IQueryable<ResultFieldNames> query) => query;

			// Child Properties
				public static IQueryable<ExamResults_SimpleValues> ExamResults_SimpleValues(this IQueryable<ResultFieldNames> resultfieldnames) => resultfieldnames.SelectMany(x => x.ExamResults_SimpleValues);
				public static IQueryable<ExamResults_SimpleValues> ExamResults_SimpleValues(this IQueryable<ResultFieldNames> resultfieldnames, int id) => resultfieldnames.Where(x => x.Id == id).SelectMany(x => x.ExamResults_SimpleValues);
			//Parent Properties
	}
}
