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
	public partial class ParishesAutoView: BaseEntity, IParishesAutoView
	{
		public string Cities { get; set; }
		public string Parishes { get; set; }

	}
}
