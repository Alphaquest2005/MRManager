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
	public static partial class EntityViewEntityPropertiesExpressions
	{
		public static IQueryable<EntityViewEntityProperties> GetEntityViewEntityPropertiesById(this IQueryable<EntityViewEntityProperties> query, int Id) => query.Where(x => x.Id == Id);


// Get Leaf Properties
    
		public static IQueryable<EntityProperties> GetEntityProperties(this IQueryable<EntityViewEntityProperties> query) => query.EntityProperties();
    
		public static IQueryable<EntityViewProperties> GetEntityViewProperties(this IQueryable<EntityViewEntityProperties> query) => query.EntityViewProperties();
    
		public static IQueryable<EntityViewEntityProperties> GetEntityViewEntityProperties(this IQueryable<EntityViewEntityProperties> query) => query;

			// Child Properties
			//Parent Properties
				//public static IQueryable<EntityViewEntityProperties> EntityViewEntityProperties(this IQueryable<EntityProperties> entityproperties) => entityproperties.SelectMany(x => x.EntityViewEntityProperties);
				public static IQueryable<EntityProperties> EntityProperties(this IQueryable<EntityViewEntityProperties> query) => query.Select(x => x.EntityProperties);
				//public static IQueryable<EntityViewEntityProperties> EntityViewEntityProperties(this IQueryable<EntityViewProperties> entityviewproperties) => entityviewproperties.Select(x => x.EntityViewEntityProperties);
				public static IQueryable<EntityViewProperties> EntityViewProperties(this IQueryable<EntityViewEntityProperties> query) => query.Select(x => x.EntityViewProperties);
	}
}
