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
	public static partial class UltraSoundGeneralEvaluationExpressions
	{
		public static IQueryable<UltraSoundGeneralEvaluation> GetUltraSoundGeneralEvaluationById(this IQueryable<UltraSoundGeneralEvaluation> query, int Id) => query.Where(x => x.Id == Id);


// Get Leaf Properties
    
		public static IQueryable<UltraSoundGeneralEvaluation> GetUltraSoundGeneralEvaluation(this IQueryable<UltraSoundGeneralEvaluation> query) => query;

			// Child Properties
			//Parent Properties
	}
}
