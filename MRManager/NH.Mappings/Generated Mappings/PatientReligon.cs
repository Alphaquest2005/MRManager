﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-Master.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


using EF.Entities;
using FluentNHibernate.Mapping;

namespace NH.Mappings
{
	public class PatientReligonMap: ClassMap<PatientReligon>
	{
		public PatientReligonMap()
		{
			
			Id(t => t.Id, "Id");        
			  Table("PatientReligon");
			  Schema("dbo");
				Map(t => t.PersonId).Column("PersonId").Not.LazyLoad();	
				Map(t => t.ReligionId).Column("ReligionId").Not.LazyLoad();	
		}
	}
}
