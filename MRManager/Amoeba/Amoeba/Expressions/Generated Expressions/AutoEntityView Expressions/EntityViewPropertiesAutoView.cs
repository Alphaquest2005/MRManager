﻿// <autogenerated>
//   This file was generated by T4 code generator Amoeba-Master.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Common.DataEntites;
using EF.Entities;
using Interfaces;

namespace Entity.Expressions
{
	public static partial class EntityViewPropertiesExpressions
	{

		public static Expression<Func<EntityViewProperties, EntityViewPropertiesAutoView>> EntityViewPropertiesAutoViewExpression { get; } =
		
			x => new EntityViewPropertiesAutoView 
			{
				Id = x.Id,
 				Entities = x.EntityView.Entities.Name,   
 				EntityView = x.EntityView.Name,   
 				EntityProperties = x.EntityViewEntityProperties.EntityProperties.PropertyName,   
 				Functions = x.EntityViewPropertyFunction.Select(x0 => x0.Functions).Select(z => z.Name).FirstOrDefault(),   
 				EntityViewPropertyFunctionParameter = x.EntityViewPropertyFunction.Select(x0 => x0.EntityViewPropertyFunctionParameter).Select(z => z.Value).FirstOrDefault(),   
 				EntityViewProperties = x.Name,   
			};
	}
}
