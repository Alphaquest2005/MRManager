﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-DataEntities.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


using EF.Entities;
using FluentNHibernate.Mapping;

namespace NH.Mappings
{
	public class AddressStatesMap: ClassMap<AddressStates>
	{
		public AddressStatesMap()
		{
			
			Id(t => t.Id, "Id");        
			  Table("AddressStates");
			  Schema("dbo");
				Map(t => t.Id).Column("Id").Not.LazyLoad();	
				Map(t => t.StateId).Column("StateId").Not.LazyLoad();	
		}
	}
}
