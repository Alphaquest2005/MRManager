﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-Master.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


using EF.Entities;
using FluentNHibernate.Mapping;

namespace NH.Mappings
{
	public class ParishCitiesMap: ClassMap<ParishCities>
	{
		public ParishCitiesMap()
		{
			
			Id(t => t.Id, "Id");        
			  Table("ParishCities");
			  Schema("dbo");
				Map(t => t.CityId).Column("CityId").Not.LazyLoad();	
				Map(t => t.Id).Column("Id").Not.LazyLoad();	
		}
	}
}
