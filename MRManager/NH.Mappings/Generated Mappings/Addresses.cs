﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-Master.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


using EF.Entities;
using FluentNHibernate.Mapping;

namespace NH.Mappings
{
	public class AddressesMap: ClassMap<Addresses>
	{
		public AddressesMap()
		{
			
			Id(t => t.Id, "Id");        
			  Table("Addresses");
			  Schema("dbo");
				Map(t => t.EntryDateTime).Column("EntryDateTime").Not.LazyLoad();	
		}
	}
}
