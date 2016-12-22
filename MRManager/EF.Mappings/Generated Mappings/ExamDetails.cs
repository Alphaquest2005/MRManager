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
	public class ExamDetailsMap
	{
		public static void Map(EntityTypeBuilder<ExamDetails> entityBuilder)
		{
			entityBuilder.ToTable("ExamDetails", "Diagnostics");
			entityBuilder.HasKey(t => t.Id);
			entityBuilder.Property(t => t.Id).HasColumnName("Id").UseSqlServerIdentityColumn();	
			entityBuilder.Property(t => t.ExamId).HasColumnName("ExamId").IsRequired();
			entityBuilder.Property(t => t.Section).HasColumnName("Section").IsRequired().HasMaxLength(50);
		//-------------------Navigation Properties -------------------------------//
				entityBuilder.HasMany(x => x.ExamDetailComponents).WithOne(p => p.ExamDetails).HasForeignKey(c => c.ExamDetailsId).OnDelete(DeleteBehavior.Restrict);
				entityBuilder.HasMany(x => x.ExamResults).WithOne(p => p.ExamDetails).HasForeignKey(c => c.ExamDetailsId).OnDelete(DeleteBehavior.Restrict);
	
				//----------------Parent Properties
				//entityBuilder.HasOne(p => p.Exams).WithMany(p => p.ExamDetails).HasForeignKey(c => c.ExamId).OnDelete(DeleteBehavior.Restrict);
	
		}
	}
}
