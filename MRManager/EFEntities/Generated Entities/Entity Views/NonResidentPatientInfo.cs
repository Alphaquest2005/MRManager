﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-DataEntities.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System;
using System.Collections.Generic;
using Common.DataEntites;
using EF.Entities;
using Interfaces;

namespace EF.Entities
{
	public partial class NonResidentPatientInfo: BaseEntity, INonResidentPatientInfo
	{
		public string Boatinfo { get; set; }
		public string MarinaList { get; set; }
		public string Organisation { get; set; }
		public DateTime? ArrivalDate { get; set; }
		public DateTime? DepartureDate { get; set; }
		public string School { get; set; }

	}
}
