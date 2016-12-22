﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-Master.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System;
using System.Linq.Expressions;
using EF.Entities;

namespace Entity.Expressions
{
	public static partial class VitalSignsExpressions
	{

		public static Expression<Func<VitalSigns, VitalsHistoryInfo>> VitalSignsToVitalsHistoryInfoExpression { get; } =
		
			x => new VitalsHistoryInfo()
			{
				Id = x.Id,
				Diastolic = x.BloodPressure.Diastolic,
				Systolic = x.BloodPressure.Systolic,
				Heights = x.Height.Value,
				Pulsed = x.Pulse.Value,
				Respirations = x.Respiration.Value,
				Temperatures = x.Temperature.Value,
				DateTimeOfReading = x.DateTimeOfReading,
				Weighted = x.Weight.Value,
			};
	}
}
