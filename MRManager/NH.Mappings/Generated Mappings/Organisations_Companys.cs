﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-Master.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


using EF.Entities;
using FluentNHibernate.Mapping;

namespace NH.Mappings
{
	public class Organisations_CompanysMap: ClassMap<Organisations_Companys>
	{
		public Organisations_CompanysMap()
		{
			
			Id(t => t.Id, "Id");        
			  Table("Organisations_Companys");
			  Schema("dbo");
				Map(t => t.Id).Column("Id").Not.LazyLoad();	
		}
	}
}
