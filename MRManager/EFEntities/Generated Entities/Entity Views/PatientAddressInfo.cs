﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-DataEntities.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using Common.DataEntites;
using Interfaces;

namespace EF.Entities
{
	public partial class PatientAddressInfo: BaseEntity, IPatientAddressInfo
	{
		public string City { get; set; }
		public string Country { get; set; }
		public string Parish { get; set; }
		public string State { get; set; }
		public string Addresstype { get; set; }

	}
}
