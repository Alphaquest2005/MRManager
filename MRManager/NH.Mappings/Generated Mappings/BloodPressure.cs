﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-DataEntities.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


using EF.Entities;
using FluentNHibernate.Mapping;

namespace NH.Mappings
{
	public class BloodPressureMap: ClassMap<BloodPressure>
	{
		public BloodPressureMap()
		{
			
			Id(t => t.Id, "Id");        
			  Table("BloodPressure");
			  Schema("Vitals");
				Map(t => t.Id).Column("Id").Not.LazyLoad();	
				Map(t => t.Systolic).Column("Systolic").Not.LazyLoad();	
				Map(t => t.Diastolic).Column("Diastolic").Not.LazyLoad();	
				Map(t => t.UnitId).Column("UnitId").Not.LazyLoad();	
		}
	}
}
