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
	public partial class PatientVisit: BaseEntity, IPatientVisit
	{
		public virtual int PatientId { get; set; }
		public virtual DateTime DateOfVisit { get; set; }
		public virtual int DoctorId { get; set; }

		//-------------------Navigation Properties -------------------------------//
			// ---------Child Relationships
				public virtual ICollection<PatientResponses> PatientResponses {get; set;}
				public virtual ICollection<PatientResults> PatientResults {get; set;}
				public virtual ICollection<PatientSyntoms> PatientSyntoms {get; set;}
				public virtual ICollection<PatientVisitVitalSigns> PatientVisitVitalSigns {get; set;}
		
			// ---------Parent Relationships
				public virtual Patients Patients {get; set;}
				public virtual Persons_Doctor Persons_Doctor {get; set;}
	

	}
}
