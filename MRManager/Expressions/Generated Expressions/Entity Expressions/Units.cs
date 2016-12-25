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
	public static partial class UnitsExpressions
	{
		public static IQueryable<Units> GetUnitsById(this IQueryable<Units> query, int Id) => query.Where(x => x.Id == Id);


// Get Leaf Properties
    
		public static IQueryable<Units> GetUnits(this IQueryable<Units> query) => query;

			// Child Properties
				public static IQueryable<BloodPressure> BloodPressure(this IQueryable<Units> units) => units.SelectMany(x => x.BloodPressure);
				public static IQueryable<BloodPressure> BloodPressure(this IQueryable<Units> units, int id) => units.Where(x => x.Id == id).SelectMany(x => x.BloodPressure);
				public static IQueryable<Height> Height(this IQueryable<Units> units) => units.SelectMany(x => x.Height);
				public static IQueryable<Height> Height(this IQueryable<Units> units, int id) => units.Where(x => x.Id == id).SelectMany(x => x.Height);
				public static IQueryable<Pulse> Pulse(this IQueryable<Units> units) => units.SelectMany(x => x.Pulse);
				public static IQueryable<Pulse> Pulse(this IQueryable<Units> units, int id) => units.Where(x => x.Id == id).SelectMany(x => x.Pulse);
				public static IQueryable<Respiration> Respiration(this IQueryable<Units> units) => units.SelectMany(x => x.Respiration);
				public static IQueryable<Respiration> Respiration(this IQueryable<Units> units, int id) => units.Where(x => x.Id == id).SelectMany(x => x.Respiration);
				public static IQueryable<Temperature> Temperature(this IQueryable<Units> units) => units.SelectMany(x => x.Temperature);
				public static IQueryable<Temperature> Temperature(this IQueryable<Units> units, int id) => units.Where(x => x.Id == id).SelectMany(x => x.Temperature);
				public static IQueryable<Weight> Weight(this IQueryable<Units> units) => units.SelectMany(x => x.Weight);
				public static IQueryable<Weight> Weight(this IQueryable<Units> units, int id) => units.Where(x => x.Id == id).SelectMany(x => x.Weight);
			//Parent Properties
	}
}
