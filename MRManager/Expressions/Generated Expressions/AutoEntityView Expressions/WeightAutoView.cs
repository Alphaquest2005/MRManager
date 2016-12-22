﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-Master.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System;
using System.Linq;
using System.Linq.Expressions;
using EF.Entities;

namespace Entity.Expressions
{
	public static partial class WeightExpressions
	{

		public static Expression<Func<Weight, WeightAutoView>> WeightAutoViewExpression { get; } =
		
			x => new WeightAutoView 
			{
				Id = x.Id,
 				Height = x.Units.Height.Select(z => z.Value).FirstOrDefault(),   
 				Pulse = x.Units.Pulse.Select(z => z.Value).FirstOrDefault(),   
 				Respiration = x.Units.Respiration.Select(z => z.Value).FirstOrDefault(),   
 				Temperature = x.Units.Temperature.Select(z => z.Value).FirstOrDefault(),   
 				Units = x.Units.Name,   
 				Weight = x.Value,   
			};
	}
}
