﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-DataEntities.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


using EF.Entities;
using FluentNHibernate.Mapping;

namespace NH.Mappings
{
	public class PersonsMap: ClassMap<Persons>
	{
		public PersonsMap()
		{
			
			Id(t => t.Id, "Id");        
			  Table("Persons");
			  Schema("dbo");
				Map(t => t.EntryDateTine).Column("EntryDateTine").Not.LazyLoad();	
		}
	}
}
