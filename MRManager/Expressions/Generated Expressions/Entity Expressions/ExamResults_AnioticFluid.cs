﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-Master.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System.Linq;
using EF.Entities;

namespace Entity.Expressions
{
	public static partial class ExamResults_AnioticFluidExpressions
	{
		public static IQueryable<ExamResults_AnioticFluid> GetExamResults_AnioticFluidById(this IQueryable<ExamResults_AnioticFluid> query, int Id) => query.Where(x => x.Id == Id);


// Get Leaf Properties
    
		public static IQueryable<ExamResults_AnioticFluid> GetExamResults_AnioticFluid(this IQueryable<ExamResults_AnioticFluid> query) => query;

			// Child Properties
			//Parent Properties
				//public static IQueryable<ExamResults_AnioticFluid> ExamResults_AnioticFluid(this IQueryable<ExamResults> examresults) => examresults.Select(x => x.ExamResults_AnioticFluid);
				public static IQueryable<ExamResults> ExamResults(this IQueryable<ExamResults_AnioticFluid> query) => query.Select(x => x.ExamResults);
	}
}
