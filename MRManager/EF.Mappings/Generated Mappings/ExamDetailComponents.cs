﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-DataEntities.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System;
using EF.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF.Mappings
{
	public class ExamDetailComponentsMap
	{
		public static void Map(EntityTypeBuilder<ExamDetailComponents> entityBuilder)
		{
			entityBuilder.ToTable("ExamDetailComponents", "dbo");
			entityBuilder.HasKey(t => t.Id);
			entityBuilder.Property(t => t.Id).HasColumnName("Id").UseSqlServerIdentityColumn();	
			entityBuilder.Property(t => t.ComponentId).HasColumnName("ComponentId").IsRequired();
			entityBuilder.Property(t => t.ExamDetailsId).HasColumnName("ExamDetailsId").IsRequired();
		//-------------------Navigation Properties -------------------------------//
	
				//----------------Parent Properties
				//entityBuilder.HasOne(p => p.Components).WithMany(p => p.ExamDetailComponents).HasForeignKey(c => c.ComponentId).OnDelete(DeleteBehavior.Restrict);
				//entityBuilder.HasOne(p => p.ExamDetails).WithMany(p => p.ExamDetailComponents).HasForeignKey(c => c.ExamDetailsId).OnDelete(DeleteBehavior.Restrict);
	
		}
	}
}
