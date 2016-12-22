﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-DataEntities.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using EF.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF.Mappings
{
	public class ResultFieldNamesMap
	{
		public static void Map(EntityTypeBuilder<ResultFieldNames> entityBuilder)
		{
			entityBuilder.ToTable("ResultFieldNames", "Diagnostics");
			entityBuilder.HasKey(t => t.Id);
			entityBuilder.Property(t => t.Id).HasColumnName("Id").UseSqlServerIdentityColumn();	
			entityBuilder.Property(t => t.Name).HasColumnName("Name").IsRequired().HasMaxLength(50);
		//-------------------Navigation Properties -------------------------------//
				entityBuilder.HasMany(x => x.ExamResults_SimpleValues).WithOne(p => p.ResultFieldNames).HasForeignKey(c => c.ResultFieldId).OnDelete(DeleteBehavior.Restrict);
	
				//----------------Parent Properties
	
		}
	}
}
