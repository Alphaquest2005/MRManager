﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-Master.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


using EF.Entities;
using FluentNHibernate.Mapping;

namespace NH.Mappings
{
	public class ExamDetailsMap: ClassMap<ExamDetails>
	{
		public ExamDetailsMap()
		{
			
			Id(t => t.Id, "Id");        
			  Table("ExamDetails");
			  Schema("Diagnostics");
				Map(t => t.ExamId).Column("ExamId").Not.LazyLoad();	
				Map(t => t.Section).Column("Section").Not.LazyLoad();	
		}
	}
}
