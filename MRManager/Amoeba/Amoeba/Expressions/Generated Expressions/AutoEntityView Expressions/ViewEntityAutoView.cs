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
	public static partial class ViewEntityExpressions
	{

		public static Expression<Func<ViewEntity, ViewEntityAutoView>> ViewEntityAutoViewExpression { get; } =
		
			x => new ViewEntityAutoView 
			{
				Id = x.Id,
 				EntityView = x.Entities.EntityView.Select(z => z.Name).FirstOrDefault(),   
 				EntityProperties = x.Entities.EntityProperties.Select(z => z.PropertyName).FirstOrDefault(),   
 				Entities = x.Entities.Name,   
			};
	}
}
