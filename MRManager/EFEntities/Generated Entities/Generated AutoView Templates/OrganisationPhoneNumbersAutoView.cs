﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-DataEntities.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using Common.DataEntites;
using Interfaces;

namespace EF.Entities
{
	public partial class OrganisationPhoneNumbersAutoView: BaseEntity, IOrganisationPhoneNumbersAutoView
	{
		public string Organisations { get; set; }
		public string PhoneTypes { get; set; }

	}
}
