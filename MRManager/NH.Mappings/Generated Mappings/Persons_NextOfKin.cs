﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-DataEntities.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


using EF.Entities;
using FluentNHibernate.Mapping;

namespace NH.Mappings
{
	public class Persons_NextOfKinMap: ClassMap<Persons_NextOfKin>
	{
		public Persons_NextOfKinMap()
		{
			
			Id(t => t.Id, "Id");        
			  Table("Persons_NextOfKin");
			  Schema("dbo");
				Map(t => t.Id).Column("Id").Not.LazyLoad();	
				Map(t => t.PatientId).Column("PatientId").Not.LazyLoad();	
				Map(t => t.Relationship).Column("Relationship").Not.LazyLoad();	
		}
	}
}
