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
	public static partial class CarePlanDetailsSuggestedMedicationExpressions
	{
		public static IQueryable<CarePlanDetailsSuggestedMedication> GetCarePlanDetailsSuggestedMedicationById(this IQueryable<CarePlanDetailsSuggestedMedication> query, int Id) => query.Where(x => x.Id == Id);


// Get Leaf Properties
    
		public static IQueryable<CarePlanDetailsSuggestedMedication> GetCarePlanDetailsSuggestedMedication(this IQueryable<CarePlanDetailsSuggestedMedication> query) => query;

			// Child Properties
			//Parent Properties
				//public static IQueryable<CarePlanDetailsSuggestedMedication> CarePlanDetailsSuggestedMedication(this IQueryable<CarePlanDetails> careplandetails) => careplandetails.SelectMany(x => x.CarePlanDetailsSuggestedMedication);
				public static IQueryable<CarePlanDetails> CarePlanDetails(this IQueryable<CarePlanDetailsSuggestedMedication> query) => query.Select(x => x.CarePlanDetails);
	}
}
