﻿// <autogenerated>
//   This file was generated by T4 code generator Amoeba-Master.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


using EF.Entities;
using FluentNHibernate.Mapping;

namespace NH.Mappings
{
	public class PartsMap: ClassMap<Parts>
	{
		public PartsMap()
		{
			
			Id(t => t.Id, "Id");        
			  Table("Parts");
			  Schema("dbo");
				Map(t => t.Name).Column("Name").Not.LazyLoad();	
				Map(t => t.Orientation).Column("Orientation").Not.LazyLoad();	
		}
	}
}
