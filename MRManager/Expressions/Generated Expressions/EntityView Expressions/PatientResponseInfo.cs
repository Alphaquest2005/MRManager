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
	public static partial class QuestionsExpressions
	{

		public static Expression<Func<Questions, PatientResponseInfo>> QuestionsToPatientResponseInfoExpression { get; } =
		
			x => new PatientResponseInfo()
			{
				Id = x.Id,
				Interview = x.Interviews.Name,
				Description = x.Description,
			};
	}
}
