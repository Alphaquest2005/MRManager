﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-DataEntities.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


using EF.Entities;
using FluentNHibernate.Mapping;

namespace NH.Mappings
{
	public class ResponseOptionsMap: ClassMap<ResponseOptions>
	{
		public ResponseOptionsMap()
		{
			
			Id(t => t.Id, "Id");        
			  Table("ResponseOptions");
			  Schema("Interview");
				Map(t => t.QuestionId).Column("QuestionId").Not.LazyLoad();	
				Map(t => t.Description).Column("Description").Not.LazyLoad();	
				Map(t => t.Type).Column("Type").Not.LazyLoad();	
		}
	}
}
