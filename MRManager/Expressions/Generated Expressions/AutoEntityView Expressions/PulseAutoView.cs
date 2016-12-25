﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-Expressions.tt.
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
	public static partial class PulseExpressions
	{

		public static Expression<Func<Pulse, PulseAutoView>> PulseAutoViewExpression { get; } =
		
			x => new PulseAutoView 
			{
				Id = x.Id,
 				Height = x.Units.Height.Select(z => z.Value).FirstOrDefault(),   
 				Respiration = x.Units.Respiration.Select(z => z.Value).FirstOrDefault(),   
 				Temperature = x.Units.Temperature.Select(z => z.Value).FirstOrDefault(),   
 				Weight = x.Units.Weight.Select(z => z.Value).FirstOrDefault(),   
 				Units = x.Units.Name,   
 				Pulse = x.Value,   
			};
	}
}
