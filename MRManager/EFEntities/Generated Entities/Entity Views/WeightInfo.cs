﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-EntityViews.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System;
using System.Collections.Generic;
using Common.DataEntites;
using EF.Entities;
using Interfaces;

namespace EF.Entities
{
	public partial class WeightInfo: BaseEntity, IWeightInfo
	{
		public string Unit { get; set; }
		public double? Weighted { get; set; }

	}
}
