﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-DataEntities.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using Common.DataEntites;
using Interfaces;

namespace EF.Entities
{
	public partial class HeightAutoView: BaseEntity, IHeightAutoView
	{
		public int? Pulse { get; set; }
		public int? Respiration { get; set; }
		public double? Temperature { get; set; }
		public double? Weight { get; set; }
		public string Units { get; set; }
		public double? Height { get; set; }

	}
}
