﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-DataEntities.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


using EF.Entities;
using FluentNHibernate.Mapping;

namespace NH.Mappings
{
	public class AllergiesMap: ClassMap<Allergies>
	{
		public AllergiesMap()
		{
			
			Id(t => t.Id, "Id");        
			  Table("Allergies");
			  Schema("dbo");
				Map(t => t.Name).Column("Name").Not.LazyLoad();	
		}
	}
}
