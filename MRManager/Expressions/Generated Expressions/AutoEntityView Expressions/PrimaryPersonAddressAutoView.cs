﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-Master.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System;
using System.Linq.Expressions;
using EF.Entities;

namespace Entity.Expressions
{
	public static partial class PrimaryPersonAddressExpressions
	{

		public static Expression<Func<PrimaryPersonAddress, PrimaryPersonAddressAutoView>> PrimaryPersonAddressAutoViewExpression { get; } =
		
			x => new PrimaryPersonAddressAutoView 
			{
				Id = x.Id,
			};
	}
}
