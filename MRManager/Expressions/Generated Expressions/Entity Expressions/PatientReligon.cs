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
	public static partial class PatientReligonExpressions
	{
		public static IQueryable<PatientReligon> GetPatientReligonById(this IQueryable<PatientReligon> query, int Id) => query.Where(x => x.Id == Id);


// Get Leaf Properties
    
		public static IQueryable<PatientReligon> GetPatientReligon(this IQueryable<PatientReligon> query) => query;

			// Child Properties
			//Parent Properties
				//public static IQueryable<PatientReligon> PatientReligon(this IQueryable<Persons_Patient> persons_patient) => persons_patient.SelectMany(x => x.PatientReligon);
				public static IQueryable<Persons_Patient> Persons_Patient(this IQueryable<PatientReligon> query) => query.Select(x => x.Persons_Patient);
	}
}
