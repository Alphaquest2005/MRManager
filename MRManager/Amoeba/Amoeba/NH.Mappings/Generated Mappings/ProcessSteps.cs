﻿// <autogenerated>
//   This file was generated by T4 code generator Amoeba-Master.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


using EF.Entities;
using FluentNHibernate.Mapping;

namespace NH.Mappings
{
	public class ProcessStepsMap: ClassMap<ProcessSteps>
	{
		public ProcessStepsMap()
		{
			
			Id(t => t.Id, "Id");        
			  Table("ProcessSteps");
			  Schema("dbo");
				Map(t => t.ProcessId).Column("ProcessId").Not.LazyLoad();	
				Map(t => t.StepId).Column("StepId").Not.LazyLoad();	
		}
	}
}
