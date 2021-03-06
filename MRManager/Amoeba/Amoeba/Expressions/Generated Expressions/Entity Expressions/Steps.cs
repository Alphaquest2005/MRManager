﻿// <autogenerated>
//   This file was generated by T4 code generator Amoeba-Master.tt.
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
	public static partial class StepsExpressions
	{
		public static IQueryable<Steps> GetStepsById(this IQueryable<Steps> query, int Id) => query.Where(x => x.Id == Id);


// Get Leaf Properties
    
		public static IQueryable<Steps> GetSteps(this IQueryable<Steps> query) => query;

			// Child Properties
				public static IQueryable<ProcessSteps> ProcessSteps(this IQueryable<Steps> steps) => steps.SelectMany(x => x.ProcessSteps);
				public static IQueryable<ProcessSteps> ProcessSteps(this IQueryable<Steps> steps, int id) => steps.Where(x => x.Id == id).SelectMany(x => x.ProcessSteps);
			//Parent Properties
	}
}
