﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-DataEntities.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


using EF.Entities;
using FluentNHibernate.Mapping;

namespace NH.Mappings
{
	public class ForeignPhoneNumbersMap: ClassMap<ForeignPhoneNumbers>
	{
		public ForeignPhoneNumbersMap()
		{
			
			Id(t => t.Id, "Id");        
			  Table("ForeignPhoneNumbers");
			  Schema("dbo");
				Map(t => t.PersonId).Column("PersonId").Not.LazyLoad();	
				Map(t => t.PhoneNumber).Column("PhoneNumber").Not.LazyLoad();	
				Map(t => t.PhoneTypeId).Column("PhoneTypeId").Not.LazyLoad();	
		}
	}
}
