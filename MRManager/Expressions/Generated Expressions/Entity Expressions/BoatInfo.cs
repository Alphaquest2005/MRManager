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
	public static partial class BoatInfoExpressions
	{
		public static IQueryable<BoatInfo> GetBoatInfoById(this IQueryable<BoatInfo> query, int Id) => query.Where(x => x.Id == Id);


// Get Leaf Properties
    
		public static IQueryable<BoatInfo> GetBoatInfo(this IQueryable<BoatInfo> query) => query;

			// Child Properties
			//Parent Properties
				//public static IQueryable<BoatInfo> BoatInfo(this IQueryable<Persons_NonResidentPatient> persons_nonresidentpatient) => persons_nonresidentpatient.Select(x => x.BoatInfo);
				public static IQueryable<Persons_NonResidentPatient> Persons_NonResidentPatient(this IQueryable<BoatInfo> query) => query.Select(x => x.Persons_NonResidentPatient);
	}
}
