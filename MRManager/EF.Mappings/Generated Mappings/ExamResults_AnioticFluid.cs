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
	public class ExamResults_AnioticFluidMap
	{
		public static void Map(EntityTypeBuilder<ExamResults_AnioticFluid> entityBuilder)
		{
			entityBuilder.ToTable("ExamResults_AnioticFluid", "Diagnostics");
			entityBuilder.HasKey(t => t.Id);
			entityBuilder.Property(t => t.Id).HasColumnName("Id").ValueGeneratedNever();	
			entityBuilder.Property(t => t.Id).HasColumnName("Id").IsRequired();
			entityBuilder.Property(t => t.MVP).HasColumnName("MVP").IsRequired();
			entityBuilder.Property(t => t.AFI).HasColumnName("AFI").IsRequired();
			entityBuilder.Property(t => t.Q1).HasColumnName("Q1").IsRequired();
			entityBuilder.Property(t => t.Q2).HasColumnName("Q2").IsRequired();
			entityBuilder.Property(t => t.Q3).HasColumnName("Q3").IsRequired();
			entityBuilder.Property(t => t.Q4).HasColumnName("Q4").IsRequired();
		//-------------------Navigation Properties -------------------------------//
	
				//----------------Parent Properties
				//entityBuilder.HasOne(p => p.ExamResults).WithOne(p => p.ExamResults_AnioticFluid).HasForeignKey<ExamResults>(c => c.Id).OnDelete(DeleteBehavior.Restrict);
	
		}
	}
}
