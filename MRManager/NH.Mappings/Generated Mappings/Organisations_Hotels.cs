﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-DataEntities.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


using EF.Entities;
using FluentNHibernate.Mapping;

namespace NH.Mappings
{
	public class Organisations_HotelsMap: ClassMap<Organisations_Hotels>
	{
		public Organisations_HotelsMap()
		{
			
			Id(t => t.Id, "Id");        
			  Table("Organisations_Hotels");
			  Schema("dbo");
				Map(t => t.Id).Column("Id").Not.LazyLoad();	
		}
	}
}
