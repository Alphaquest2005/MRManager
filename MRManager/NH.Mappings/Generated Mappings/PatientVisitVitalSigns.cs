﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-DataEntities.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


using EF.Entities;
using FluentNHibernate.Mapping;

namespace NH.Mappings
{
	public class PatientVisitVitalSignsMap: ClassMap<PatientVisitVitalSigns>
	{
		public PatientVisitVitalSignsMap()
		{
			
			Id(t => t.Id, "Id");        
			  Table("PatientVisitVitalSigns");
			  Schema("dbo");
				Map(t => t.PatientVisitId).Column("PatientVisitId").Not.LazyLoad();	
				Map(t => t.VitalSignsId).Column("VitalSignsId").Not.LazyLoad();	
		}
	}
}
