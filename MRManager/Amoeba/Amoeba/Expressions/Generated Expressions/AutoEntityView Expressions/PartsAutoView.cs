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
	public static partial class PartsExpressions
	{

		public static Expression<Func<Parts, PartsAutoView>> PartsAutoViewExpression { get; } =
		
			x => new PartsAutoView 
			{
				Id = x.Id,
 				Screens = x.ScreenParts.Select(x0 => x0.Screens).Select(z => z.Name).FirstOrDefault(),   
 				Parts = x.Name,   
			};
	}
}
