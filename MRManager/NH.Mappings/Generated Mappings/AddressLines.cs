﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-DataEntities.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


using EF.Entities;
using FluentNHibernate.Mapping;

namespace NH.Mappings
{
	public class AddressLinesMap: ClassMap<AddressLines>
	{
		public AddressLinesMap()
		{
			
			Id(t => t.Id, "Id");        
			  Table("AddressLines");
			  Schema("dbo");
				Map(t => t.Name).Column("Name").Not.LazyLoad();	
				Map(t => t.AddressId).Column("AddressId").Not.LazyLoad();	
		}
	}
}
