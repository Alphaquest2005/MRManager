﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-Master.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


using EF.Entities;
using FluentNHibernate.Mapping;

namespace NH.Mappings
{
	public class NonResidentHotelInfoMap: ClassMap<NonResidentHotelInfo>
	{
		public NonResidentHotelInfoMap()
		{
			
			Id(t => t.Id, "Id");        
			  Table("NonResidentHotelInfo");
			  Schema("dbo");
				Map(t => t.HotelId).Column("HotelId").Not.LazyLoad();	
				Map(t => t.Id).Column("Id").Not.LazyLoad();	
		}
	}
}
