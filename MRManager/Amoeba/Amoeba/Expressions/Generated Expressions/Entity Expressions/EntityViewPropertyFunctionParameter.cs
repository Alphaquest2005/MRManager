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
	public static partial class EntityViewPropertyFunctionParameterExpressions
	{
		public static IQueryable<EntityViewPropertyFunctionParameter> GetEntityViewPropertyFunctionParameterById(this IQueryable<EntityViewPropertyFunctionParameter> query, int Id) => query.Where(x => x.Id == Id);


// Get Leaf Properties
    
		public static IQueryable<EntityViewProperties> GetEntityViewProperties(this IQueryable<EntityViewPropertyFunctionParameter> query) => query.EntityViewPropertyFunction().EntityViewProperties();
    
		public static IQueryable<Functions> GetFunctions(this IQueryable<EntityViewPropertyFunctionParameter> query) => query.EntityViewPropertyFunction().Functions();
    
		public static IQueryable<EntityViewPropertyFunction> GetEntityViewPropertyFunction(this IQueryable<EntityViewPropertyFunctionParameter> query) => query.EntityViewPropertyFunction();
    
		public static IQueryable<Parameters> GetParameters(this IQueryable<EntityViewPropertyFunctionParameter> query) => query.FunctionParameters().Parameters();
    
		public static IQueryable<FunctionParameters> GetFunctionParameters(this IQueryable<EntityViewPropertyFunctionParameter> query) => query.FunctionParameters();
    
		public static IQueryable<EntityViewPropertyFunctionParameter> GetEntityViewPropertyFunctionParameter(this IQueryable<EntityViewPropertyFunctionParameter> query) => query;

			// Child Properties
			//Parent Properties
				//public static IQueryable<EntityViewPropertyFunctionParameter> EntityViewPropertyFunctionParameter(this IQueryable<EntityViewPropertyFunction> entityviewpropertyfunction) => entityviewpropertyfunction.Select(x => x.EntityViewPropertyFunctionParameter);
				public static IQueryable<EntityViewPropertyFunction> EntityViewPropertyFunction(this IQueryable<EntityViewPropertyFunctionParameter> query) => query.Select(x => x.EntityViewPropertyFunction);
				//public static IQueryable<EntityViewPropertyFunctionParameter> EntityViewPropertyFunctionParameter(this IQueryable<FunctionParameters> functionparameters) => functionparameters.SelectMany(x => x.EntityViewPropertyFunctionParameter);
				public static IQueryable<FunctionParameters> FunctionParameters(this IQueryable<EntityViewPropertyFunctionParameter> query) => query.Select(x => x.FunctionParameters);
	}
}
