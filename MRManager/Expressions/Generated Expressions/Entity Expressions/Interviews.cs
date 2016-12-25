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
	public static partial class InterviewsExpressions
	{
		public static IQueryable<Interviews> GetInterviewsById(this IQueryable<Interviews> query, int Id) => query.Where(x => x.Id == Id);


// Get Leaf Properties
    
		public static IQueryable<Interviews> GetInterviews(this IQueryable<Interviews> query) => query;

			// Child Properties
				public static IQueryable<Questions> Questions(this IQueryable<Interviews> interviews) => interviews.SelectMany(x => x.Questions);
				public static IQueryable<Questions> Questions(this IQueryable<Interviews> interviews, int id) => interviews.Where(x => x.Id == id).SelectMany(x => x.Questions);
			//Parent Properties
				//public static IQueryable<Interviews> Interviews(this IQueryable<Phase> phase) => phase.SelectMany(x => x.Interviews);
				public static IQueryable<Phase> Phase(this IQueryable<Interviews> query) => query.Select(x => x.Phase);
				//public static IQueryable<Interviews> Interviews(this IQueryable<MedicalCategory> medicalcategory) => medicalcategory.SelectMany(x => x.Interviews);
				public static IQueryable<MedicalCategory> MedicalCategory(this IQueryable<Interviews> query) => query.Select(x => x.MedicalCategory);
	}
}
