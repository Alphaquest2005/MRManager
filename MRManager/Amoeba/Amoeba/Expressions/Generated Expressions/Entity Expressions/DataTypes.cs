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
	public static partial class DataTypesExpressions
	{
		public static IQueryable<DataTypes> GetDataTypesById(this IQueryable<DataTypes> query, int Id) => query.Where(x => x.Id == Id);


// Get Leaf Properties
    
		public static IQueryable<DataTypes> GetDataTypes(this IQueryable<DataTypes> query) => query;

			// Child Properties
				public static IQueryable<DataProperties> DataProperties(this IQueryable<DataTypes> datatypes) => datatypes.SelectMany(x => x.DataProperties);
				public static IQueryable<DataProperties> DataProperties(this IQueryable<DataTypes> datatypes, int id) => datatypes.Where(x => x.Id == id).SelectMany(x => x.DataProperties);
				public static IQueryable<Parameters> Parameters(this IQueryable<DataTypes> datatypes) => datatypes.SelectMany(x => x.Parameters);
				public static IQueryable<Parameters> Parameters(this IQueryable<DataTypes> datatypes, int id) => datatypes.Where(x => x.Id == id).SelectMany(x => x.Parameters);
				public static IQueryable<FunctionReturnType> FunctionReturnType(this IQueryable<DataTypes> datatypes) => datatypes.SelectMany(x => x.FunctionReturnType);
				public static IQueryable<FunctionReturnType> FunctionReturnType(this IQueryable<DataTypes> datatypes, int id) => datatypes.Where(x => x.Id == id).SelectMany(x => x.FunctionReturnType);
			//Parent Properties
	}
}
